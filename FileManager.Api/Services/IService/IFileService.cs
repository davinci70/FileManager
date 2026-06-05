namespace FileManager.Api.Services.IService;

public interface IFileService
{
    public Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default);
}
