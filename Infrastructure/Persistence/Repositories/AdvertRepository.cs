using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
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

        // Compiled Queries

        // It's not worth it to compile a query that returns a List
        //private static readonly Func<HomelyzerDBContext, Task<List<Advert>>> GetAll_Complete_Query =
        //    EF.CompileAsyncQuery((HomelyzerDBContext context) =>
        //        context.Adverts.AsNoTracking()
        //            .Include("Pictures")
        //            .Include("Owner")
        //            .ToList()
        //    );

        private static readonly Func<HomelyzerDBContext, int, Task<Advert?>> GetById_Complete_Query =
            EF.CompileAsyncQuery((HomelyzerDBContext context, int id) =>
                context.Adverts
                .Include("Pictures")
                .Include("Owner")
                .AsNoTracking()
                .FirstOrDefault(a => a.AdvertId == id)
            );

        public async Task<IEnumerable<Advert>> GetAll_Complete_Async()
        {
            return await _context.Adverts.AsNoTracking()
                    .Include("Pictures")
                    .Include("Owner")
                    .ToListAsync();
        }

        public async Task<IEnumerable<Advert>> GetAllActive_Complete_Async()
        {
            return (await GetAll_Complete_Async()).Where(x=> x.IsActive);
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
            return await GetById_Complete_Query(_context, id);
        }
    }
}
