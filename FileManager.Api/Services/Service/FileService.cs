namespace FileManager.Api.Services.Service;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
    private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";
    private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/images";
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var uploadedFile = await SaveFile(file, cancellationToken);

        await _context.AddAsync(uploadedFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return  uploadedFile.Id;
    }

    public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        List<UploadedFile> uploadedFiles = [];

        foreach (var file in files)
        {
            var uploadedFile = await SaveFile(file, cancellationToken);
            uploadedFiles.Add(uploadedFile);
        }

        await _context.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFiles.Select(x => x.Id).ToList();
    }

    public async Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_imagesPath, image.FileName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
    }

    private async Task<UploadedFile> SaveFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        var randomFileName = Path.GetRandomFileName();

        var uploadedFile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileExtension = Path.GetExtension(file.FileName),
            StoredFileName = randomFileName
        };

        var path = Path.Combine(_filesPath, randomFileName);

        using var stream = File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
    }
}
