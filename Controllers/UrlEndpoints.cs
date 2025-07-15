using EncurtadorUrl.Dtos;
using EncurtadorUrl.Error;
using EncurtadorUrl.Services;

namespace EncurtadorUrl.Controllers;

internal static class UrlEndpoints
{

    public static void AddUrlRoutes(this WebApplication app)
    {
        var routes = app.MapGroup("");

        routes.MapGet("{code}", async (
            string code,
            CancellationToken ct,
            IUrlService service) =>
        {
            var result = await service.GetShortUrl(code, ct);

            if (result == null)
                return Results.NotFound();

            return Results.Redirect(result);
        })
        .WithName("GetShortUrl")
        .WithDescription("Get url from short code");

        routes.MapPost("register", async (
            CreateShortUrlRequest body,
            CancellationToken ct,
            IUrlService service) =>
        {
            var result = await service.CreateShortUrl(body, ct);

            if (result.IsT0)
            {
                var response = result.AsT0;
                return Results.Created($"{response.ShortUrl}", response);
            }

            var error = result.AsT1;

            if (error.TypeError == TypeError.Validation)
            {
                return Results.BadRequest(error);
            }

            return Results.InternalServerError();
        })
        .WithName("CreateShortUrl")
        .WithDescription("Create a new short url");
    }
}
