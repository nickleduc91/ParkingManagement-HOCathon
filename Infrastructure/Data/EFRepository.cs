using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly ParkingManagementDbContext _context;
    public EfRepository(ParkingManagementDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();   
    }
}
