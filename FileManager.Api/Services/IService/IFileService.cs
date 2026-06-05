namespace FileManager.Api.Services.IService;

public interface IFileService
{
    public Task<Guid> Upload(IFormFile file, CancellationToken cancellationToken = default);
}
