using Application.Contracts.DTOs.Advert;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.DTOs.Owner;

public sealed class OwnerDTO
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string PhoneContact { get; set; }
    public string EmailContact { get; set; }
    public List<AdvertDTO> Adverts { get; set; }

}
