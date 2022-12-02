using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Persistence.Repositories
{
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        private HomelyzerDBContext _context { get; set; }
        public AdvertRepository(HomelyzerDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Advert>> GetAll_Complete_Async()
        {
            return await _context.Adverts.AsNoTracking()
                .Include("Pictures")
                .Include("Owner")
                .ToListAsync();
        }
        public async Task<IEnumerable<Advert>> GetAdvertsByOwnerIdAsync(int ownerId)
        {
            return await _context.Adverts.Where(a => a.OwnerId == ownerId).ToListAsync();
        }

        //public async Task<IEnumerable<Advert>> GetTop5AdvertsAsync()
        //{
        //    return await _context.Adverts.OrderByDescending(ad => ad.Score).ToListAsync();
        //}

        public async Task ClearAllAdverts()
        {
            var conn = new SqlConnection("Server=tcp:homelyzerdbdbserver.database.windows.net,1433;Initial Catalog=HomelyzerDB;Persist Security Info=False;User ID=pixodungbomb;Password=qweQWE123!\"#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();

            try
            {
                var cmd = new SqlCommand($"DELETE FROM [dbo].[Adverts]\r\nDBCC CHECKIDENT ('HomelyzerDB.dbo.Adverts', RESEED, 0)");
                cmd.Connection = conn;
                cmd.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }

            //_context.Adverts.RemoveRange(_context.Adverts);
        }

        public async Task<Advert> GetById_Complete_Async(int id)
        {
            return await _context.Adverts
                .Include("Pictures")
                .Include("Owner")
                .AsNoTracking()
                .FirstAsync(a => a.AdvertId == id);
        }
    }
}
