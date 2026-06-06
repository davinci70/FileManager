using FileManager.Api.Settings;

namespace FileManager.Api.Contracts.UploadFile;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        RuleFor(x => x.File)
            .Must((request, context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
            .WithMessage($"Max file size is {FileSettings.MaxFileSizeInMB} MB")
            .When(x => x.File is not null);
    }
}
