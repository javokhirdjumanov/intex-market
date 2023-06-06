using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace IndexMarket.Application.Services;
public class FileServices : IFileServices
{
    private readonly IFileRepository fileRepository;
    public FileServices(IFileRepository fileRepository)
    {
        this.fileRepository = fileRepository;
    }

    public async Task<FileDto> UploadFile(IFormFile file)
    {
        var exension = Path.GetExtension(file.FileName);

        var newFile = await this.fileRepository.CreateAsync(new FileModel
        {
            Type = exension,
            FileName = file.FileName.Replace(exension, string.Empty),
        });

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "AdditionalInformation",
            "UploadsFiles");

        if(!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        filePath = Path.Combine(filePath, newFile.Id.ToString() + newFile.Type);
        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new FileDto(newFile.Id, newFile.Type, newFile.FileName);
    }

    public async Task<(FileStream, FileModel)> DownloadFile(Guid fileId)
    {
        fileId.IsDefault();

        var storageFile = await this.fileRepository
            .GetFileByIdAsync(fileId);

        ValidationStorageObject
            .ValidationGeneric<FileModel>(storageFile, fileId);

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "AdditionalInformation",
            "UploadsFiles",
            fileId.ToString() + storageFile.Type);

        if (!System.IO.File.Exists(filePath))
            throw new NotFoundException($"Couldn't file the given id: {filePath}");

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        return (fileStream, storageFile);
    }
}