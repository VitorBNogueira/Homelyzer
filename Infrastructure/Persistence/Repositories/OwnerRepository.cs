using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        private HomelyzerDBContext _context { get; set; }
        public OwnerRepository(HomelyzerDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
