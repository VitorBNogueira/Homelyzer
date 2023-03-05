using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public sealed class HomelyzerDBContext : DbContext
{
    public HomelyzerDBContext(DbContextOptions<HomelyzerDBContext> options) : base(options)
    {
    }

    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Picture> Pictures { get; set; }
}
