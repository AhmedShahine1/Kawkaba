using Microsoft.EntityFrameworkCore;

namespace InitialProject.RepositoryLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    //-----------------------------------------------------------------------------------
    int SaveChanges();

    Task<int> SaveChangesAsync();
}