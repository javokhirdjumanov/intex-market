using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Application.Services;
public interface IFileServices
{
    Task<FileDto> UploadFile(IFormFile file);
    Task<(FileStream, FileModel)> DownloadFile(Guid fileId);
}
