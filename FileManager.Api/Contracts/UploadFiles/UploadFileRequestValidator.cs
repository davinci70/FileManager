namespace FileManager.Api.Contracts.UploadFiles;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        RuleFor(x => x.File)
            .SetValidator(new FileSizeValidator());

        RuleFor(x => x.File)
            .SetValidator(new BlockedSignaturesValidator());
    }
}
