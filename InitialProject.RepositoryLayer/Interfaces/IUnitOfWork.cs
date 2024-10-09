using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using Microsoft.EntityFrameworkCore;

namespace Kawkaba.RepositoryLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<ApplicationUser> UserRepository { get; }
    public IBaseRepository<Paths> PathsRepository { get; }
    public IBaseRepository<Images> ImagesRepository { get; }
    //-----------------------------------------------------------------------------------
    int SaveChanges();

    Task<int> SaveChangesAsync();
}