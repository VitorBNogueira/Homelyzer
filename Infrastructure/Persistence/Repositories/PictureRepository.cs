using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        private HomelyzerDBContext _context { get; set; }
        public PictureRepository(HomelyzerDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Picture>> GetPicturesByAdvertIdAsync(int advertId)
        {
            return await _context.Pictures.Where(a => a.AdvertId == advertId).ToListAsync();
        }

        public async Task ClearAllPictures()
        {
            _context.Pictures.RemoveRange(_context.Pictures);
        }
    }
}
