using System;
using EncurtadorUrl.Dtos;
using EncurtadorUrl.Error;
using OneOf;

namespace EncurtadorUrl.Services;

public interface IUrlService
{
    public Task<OneOf<ShortUrlCreatedResponse, AppError>> CreateShortUrl(CreateShortUrlRequest req, CancellationToken ct);
    public Task<string?> GetShortUrl(string code, CancellationToken ct);
}
