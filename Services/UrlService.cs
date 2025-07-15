using System;
using EncurtadorUrl.Data;
using EncurtadorUrl.Dtos;
using EncurtadorUrl.Entities;
using EncurtadorUrl.Error;
using EncurtadorUrl.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using OneOf;

namespace EncurtadorUrl.Services;

internal class UrlService : IUrlService
{
    private readonly AppDbContext _context;
    private readonly ILogger<Program> _logger;
    private readonly IDistributedCache _cache;
    private readonly IGenerateUrlCodeHandler _generateUrlCodeHandler;

    public UrlService(
        AppDbContext context,
        ILogger<Program> logger,
        IDistributedCache cache,
        IGenerateUrlCodeHandler generateUrlCodeHandler)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
        _generateUrlCodeHandler = generateUrlCodeHandler;
    }

    public async Task<OneOf<ShortUrlCreatedResponse, AppError>> CreateShortUrl(CreateShortUrlRequest req, CancellationToken ct)
    {
        if (req.Url.Length == 0)
        {
            _logger.LogError("Campo [URL] é vazio");
            return new UrlFieldEmptyError();
        }

        var verifyUrlExists = await _context.Urls.Where(u => u.Url == req.Url).FirstOrDefaultAsync(ct);

        if (verifyUrlExists != null)
        {
            _logger.LogInformation("Url já cadastrada, retornado codigo referente: {code}", verifyUrlExists.Code);
            return new ShortUrlCreatedResponse($"http://localhost:5140/{verifyUrlExists.Code}", req.Url);
        }

        var code = _generateUrlCodeHandler.Execute();
        

        var newShortUrl = new UrlEntity(code, req.Url);
        await _context.Urls.AddAsync(newShortUrl, ct);
        await _context.SaveChangesAsync(ct);

        _logger.LogInformation("Nova url encurtada criada para: {url}", req.Url);
        return new ShortUrlCreatedResponse($"http://localhost:5140/{code}", req.Url);
    }

    public async Task<string?> GetShortUrl(string code, CancellationToken ct)
    {
        var linkCache = await _cache.GetStringAsync(code, ct);

        if (linkCache != null)
        {
            _logger.LogInformation("link encontrado no cache: {link}", linkCache);
            return linkCache;
        }

        var link = await _context.Urls.Where(u => u.Code == code).FirstOrDefaultAsync(ct);

        if (link == null)
        {
        _logger.LogError("Codigo não encontrado: {code}", code);
            return null;
        }

        await _cache.SetStringAsync(code, link.Url, ct);

        _logger.LogInformation("Codigo encontrado: {code}", code);
        return link.Url;
    }
}
