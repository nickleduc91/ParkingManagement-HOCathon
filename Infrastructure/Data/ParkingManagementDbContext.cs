using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationCore.Entities;



namespace Infrastructure.Data
{
    public class ParkingManagementDbContext : DbContext
    {
        public ParkingManagementDbContext(DbContextOptions<ParkingManagementDbContext> options) : base(options) { }

        /*Add db enitites below:*/
        public DbSet<ParkingGarage> ParkingGarages { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Car> Cars { get; set; }
    }


}
