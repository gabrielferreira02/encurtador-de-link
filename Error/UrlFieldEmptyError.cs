namespace EncurtadorUrl.Error;

internal record UrlFieldEmptyError() : AppError("Campo [URL] não deve ser vazio", TypeError.Validation);
