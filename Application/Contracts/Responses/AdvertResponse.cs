using Application.Contracts.DTOs.Advert;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Responses
{
    public class AdvertResponse : ISuccess
    {
        public AdvertDTO Advert { get; set; }

        public AdvertResponse(AdvertDTO advert)
        {
            Advert = advert;
        }
    }
}
