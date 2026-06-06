namespace FileManager.Api.Contracts.UploadManyFiles;

public record UploadManyFilesRequest(
    IFormFileCollection Files    
);
