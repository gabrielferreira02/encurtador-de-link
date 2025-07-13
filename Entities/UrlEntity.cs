using System;

namespace EncurtadorUrl.Entities;

public class UrlEntity
{
    public Guid Id { get; init; }
    public string Code { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;

    public UrlEntity() { }

    public UrlEntity(string code, string url)
    {
        Code = code;
        Url = url;
    }
}
