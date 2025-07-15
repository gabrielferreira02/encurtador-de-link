using System;

namespace EncurtadorUrl.Handlers;

internal class GenerateUrlCodeHandler : IGenerateUrlCodeHandler
{
    public string Execute()
    {
        string code = string.Empty;
        Random rand = new();

        string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 8; i++)
        {
            int n = rand.Next(chars.Length);
            code += chars[n];
        }

        return code;
    }
}
