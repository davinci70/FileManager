using FileManager.Api.Settings;
using System.Security.Cryptography.Xml;

namespace FileManager.Api.Contracts.UploadFile;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        RuleFor(x => x.File)
            .Must((request, context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
            .WithMessage($"Max file size is {FileSettings.MaxFileSizeInMB} MB")
            .When(x => x.File is not null);

        RuleFor(x => x.File)
            .Must((request, context) =>
            {
                BinaryReader binary = new(request.File.OpenReadStream());
                var bytes = binary.ReadBytes(2);

                var fileSequenceHex = BitConverter.ToString(bytes);

                foreach (var signature in FileSettings.BlockedSignatures)
                    if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                        return false;

                return true;
            })
            .WithMessage("File content is not allowed")
            .When(x => x.File is not null);
    }
}
