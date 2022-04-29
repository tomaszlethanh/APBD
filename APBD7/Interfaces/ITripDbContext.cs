using APBD7.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APBD7.Interfaces
{
    public interface ITripDbContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<ClientTrip> ClientTrips { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<CountryTrip> CountryTrips { get; set; }
        DbSet<Trip> Trips { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        

    }
}
