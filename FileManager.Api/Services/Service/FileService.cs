namespace FileManager.Api.Services.Service;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/uploads";
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> Upload(IFormFile file, CancellationToken cancellationToken = default)
    {
        var randomFileName = Path.GetRandomFileName();

        var uploadedFile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileExtension = Path.GetExtension(file.FileName),
            StoredFileName = randomFileName
        };

        var path = Path.Combine(_filePath, randomFileName);

        using var stream = File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        await _context.AddAsync(uploadedFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFile.Id;
    }
}
