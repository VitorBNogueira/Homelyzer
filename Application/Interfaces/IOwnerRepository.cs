using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<Advert> GetById_Complete_Async(int id);
        Task<IEnumerable<Owner>> GetAll_Complete_Async();
    }
}
