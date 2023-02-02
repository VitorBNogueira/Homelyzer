using Application.DTOs.Advert;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Responses
{
    public class OwnerListResponse : ISuccess
    {
        public IEnumerable<OwnerDTO> Owners { get; set; }

        public OwnerListResponse(IEnumerable<OwnerDTO> owners)
        {
            Owners = owners;
        }
    }
}
