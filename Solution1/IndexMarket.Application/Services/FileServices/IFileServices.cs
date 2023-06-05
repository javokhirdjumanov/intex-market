using IndexMarket.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Application.Services;
public interface IFileServices
{
    Task<string> UploadFile(IFormFile file);
    Task<(FileStream, FileModel)> DownloadFile(Guid fileId);
}
