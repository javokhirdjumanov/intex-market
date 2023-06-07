using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Infrastructure.Repository;
public class FileRepository : IFileRepository
{
    private readonly AppDbContext appDbContext;
    public FileRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<FileModel> CreateAsync(FileModel file)
    {
        var entityEntry = await this.appDbContext
            .AddAsync<FileModel>(file);

        await this.appDbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async ValueTask<FileModel> GetFileByIdAsync(Guid fileId)
    {
        return await this.appDbContext
                    .Set<FileModel>()
                    .FirstOrDefaultAsync(f => f.Id == fileId);
    }
}
