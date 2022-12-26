using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Advert
{
    [Key]
    public int AdvertId { get; set; }
    public string Name { get; set; }
    public string? Address{ get; set; }
    public string? Area { get; set; }
    public string? Price { get; set; }
    public EAdvertType? Type { get; set; }
    public string? Description { get; set; }
    public string? PersonalNotes { get; set; }
    public DateTime? MeetingTime { get; set; }
    public bool? IncludesBills { get; set; }
    public int? OwnerId { get; set; }
    public double? Score { get; set; }
    public string? Url { get; set; }

    public bool IsActive { get; set; }

    public List<Picture>? Pictures { get; set; }
    public Owner? Owner { get; set; }
}
