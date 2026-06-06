# File Manager

A robust File Management API built with ASP.NET Core Web API that provides secure file upload, download, image handling, and media streaming capabilities.

## Features

### File Upload

* Upload a single file.
* Upload multiple files in a single request.
* Upload image files.

### File Validation

* Extension validation.
* File signature validation.
* Maximum file size validation.
* Protection against invalid or spoofed file uploads.

### File Management

* Download stored files.
* Stream media files efficiently.
* Generate unique names for stored files.
* Store file metadata in SQL Server.

### Metadata Storage

The API stores file information in the database including:

* File Id
* Original File Name
* Generated Stored File Name
* Content Type
* File Extension

Example:

| Property       | Value                             |
| -------------- | --------------------------------- |
| FileName       | image.jpg                         |
| StoredFileName | rvy0t3ys.rvy                      |
| ContentType    | image/jpeg                        |
| FileExtension  | .jpg                              |

## Technologies Used

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* RESTful APIs

## API Endpoints

### Upload Single File

```http id="jcb0ll"
POST /api/files/upload
```

Uploads a single file after validation.

### Upload Multiple Files

```http id="y6up3j"
POST /api/files/upload-many
```

Uploads multiple files in one request.

### Upload Image

```http id="iz2kv5"
POST /api/files/upload-image
```

Uploads an image after validating:

* Extension
* Signature
* File Size

### Download File

```http id="n4hvk4"
GET /api/files/download/{id}
```

Downloads a file using its identifier.

### Stream File

```http id="83n5wk"
GET /api/files/stream/{id}
```

Streams media files without loading the entire file into memory.

## Database Design

The API stores file metadata separately from the physical file.

### File Entity

```csharp
public class UploadedFile
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = default!;

    public string StoredFileName { get; set; } = default!;

    public string ContentType { get; set; } = default!;

    public string FileExtension { get; set; } = default!;
}
```

## Security Measures

* File extension validation.
* File signature verification.
* Maximum file size restrictions.
* Randomized storage file names.
* Metadata separation from physical storage.

## Learning Outcomes

This project demonstrates practical experience with:

* File Handling in ASP.NET Core
* IFormFile Processing
* Entity Framework Core
* SQL Server Integration
* Secure File Upload Techniques
* Media Streaming
* RESTful API Design
* Validation and Security Best Practices

## Author

Mohammed Ahmed Ibrahim
.NET Developer
