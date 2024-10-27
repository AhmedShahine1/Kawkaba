using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using Kawkaba.Core.Entity.Posts;
using Kawkaba.Core.Entity.RequestEmployee;
using Microsoft.EntityFrameworkCore;

namespace Kawkaba.RepositoryLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<ApplicationUser> UserRepository { get; }
    public IBaseRepository<Paths> PathsRepository { get; }
    public IBaseRepository<Images> ImagesRepository { get; }
    public IBaseRepository<RequestEmployee> RequestEmployeeRepository { get; }
    public IBaseRepository<Post> PostRepository { get; }
    //-----------------------------------------------------------------------------------
    int SaveChanges();

    Task<int> SaveChangesAsync();
}