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

        public async Task<Advert> GetById_Complete_Async(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Owner>> GetAll_Complete_Async()
        {
            return await _context.Owners.AsNoTracking()
                .Include("Adverts")
                .ToListAsync();
        }
    }
}
