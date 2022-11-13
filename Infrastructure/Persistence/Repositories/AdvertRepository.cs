using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        private HomelyzerDBContext _context { get; set; }
        public AdvertRepository(HomelyzerDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Advert>> GetAdvertsByOwnerIdAsync(int ownerId)
        {
            return await _context.Adverts.Where(a => a.OwnerId == ownerId).ToListAsync();
        }

        public async Task<IEnumerable<Advert>> GetTop5AdvertsAsync()
        {
            return await _context.Adverts.OrderByDescending(ad => ad.Score).ToListAsync();
        }

        public async Task ClearAllAdverts()
        {
            _context.Adverts.RemoveRange(_context.Adverts);
        }
    }
}
