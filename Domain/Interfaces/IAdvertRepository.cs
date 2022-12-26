using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<IEnumerable<Advert>> GetAdvertsByOwnerIdAsync(int ownerId);
        //Task<IEnumerable<Advert>> GetTop5AdvertsAsync();
        Task<IEnumerable<Advert>> GetAll_Complete_Async();
        Task<IEnumerable<Advert>> GetAllActive_Complete_Async();

        Task<Advert> GetById_Complete_Async(int id);
        Task ClearAllAdverts();
    }
}
