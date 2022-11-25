﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Advert;

public sealed class PictureDTO
{
    public int? PictureId { get; set; }
    public int? AdvertId { get; set; }
    public string Url { get; set; }

}
