using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTOs.Adverts;

public sealed class NewAdvertDTORequest
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Area { get; set; }
    public string Price { get; set; }
    public EAdvertType Type { get; set; }
    public string? Description { get; set; }
    public string? PersonalNotes { get; set; }
    public DateTime MeetingTime { get; set; }
    public bool IncludesBills { get; set; }
    public int OwnerId { get; set; }
    //public string Pictures { get; set; }
    public double Score { get; set; }
}
