using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public class FileServices : IFileServices
{
    private readonly IFileRepository fileRepository;
    public FileServices(IFileRepository fileRepository)
    {
        this.fileRepository = fileRepository;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        var exension = Path.GetExtension(file.FileName);

        var newFile = await this.fileRepository.CreateAsync(new FileModel
        {
            Type = exension,
            FileName = file.FileName.Replace(exension, string.Empty),
        });

        var additionalInformationDirectory = "AdditionalInformation";
        var uploadsDirectory = "UploadsFiles";

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), additionalInformationDirectory, uploadsDirectory);

        if(!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        filePath = Path.Combine(filePath, newFile.Id.ToString() + newFile.Type);
        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return JsonSerializer.Serialize($"File Id: {newFile.Id}");
    }

    public async Task<(FileStream, FileModel)> DownloadFile(Guid fileId)
    {
        var newFile = await this.fileRepository.GetFileByIdAsync(fileId);

        if(newFile == null)
            throw new NotFoundException($"Couldn't file the given id: {fileId}");

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "UploadsFiles",
            fileId.ToString() + newFile.Type);

        if (!System.IO.File.Exists(filePath))
            throw new NotFoundException($"Couldn't file the given id: {filePath}");

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        return (fileStream, newFile);
    }
}