using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IFileRepository
{
    ValueTask<FileModel> CreateAsync(FileModel file);
    ValueTask<FileModel> GetFileByIdAsync(Guid fileId);
}
