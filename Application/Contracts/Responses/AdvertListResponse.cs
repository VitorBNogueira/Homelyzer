using Application.DTOs.Advert;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Responses
{
    public class AdvertListResponse : ISuccess
    {
        public IEnumerable<AdvertDTO> Adverts { get; set; }

        public AdvertListResponse(IEnumerable<AdvertDTO> adverts)
        {
            Adverts = adverts;
        }
    }
}
