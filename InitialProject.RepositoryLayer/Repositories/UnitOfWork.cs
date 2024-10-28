using Kawkaba.Core;
using Kawkaba.RepositoryLayer.Interfaces;
using Kawkaba.Core.Entity.ApplicationData;
using Kawkaba.Core.Entity.Files;
using Kawkaba.Core.Entity.RequestEmployee;
using Kawkaba.Core.Entity.Posts;
using Microsoft.AspNetCore.Identity;

namespace Kawkaba.RepositoryLayer.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBaseRepository<ApplicationUser> UserRepository { get; set; }
    public IBaseRepository<ApplicationRole> RoleRepository { get; set; }
    public IBaseRepository<IdentityUserRole<string>> UserRoleRepository { get; set; }


    public IBaseRepository<Paths> PathsRepository { get; set; }
    public IBaseRepository<Images> ImagesRepository { get; set; }
    public IBaseRepository<RequestEmployee> RequestEmployeeRepository { get; set; }
    public IBaseRepository<Post> PostRepository { get; set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new BaseRepository<ApplicationUser>(context);
        RoleRepository = new BaseRepository<ApplicationRole>(context);
        UserRoleRepository = new BaseRepository<IdentityUserRole<string>>(context);
        PathsRepository = new BaseRepository<Paths>(context);
        ImagesRepository = new BaseRepository<Images>(context);
        RequestEmployeeRepository = new BaseRepository<RequestEmployee>(context);
        PostRepository = new BaseRepository<Post>(context);

    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}