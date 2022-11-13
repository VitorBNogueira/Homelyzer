using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTOs.Adverts;

public sealed class AdvertListDTOResponse
{
    public List<Advert> AdvertList { get; private set; }

	public AdvertListDTOResponse(List<Advert> advertList)
	{
		AdvertList = advertList;
	}
}
