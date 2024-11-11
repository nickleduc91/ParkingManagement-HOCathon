using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationCore.Entities;

namespace Infrastructure.Data
{
    public class ParkingManagementDbContextSeed
    {
        public static async Task SeedAsync(ParkingManagementDbContext parkingManagementContext,
        ILogger logger,
        int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (parkingManagementContext.Database.IsSqlServer())
                {
                    parkingManagementContext.Database.Migrate();
                }

                if (!await parkingManagementContext.ParkingGarages.AnyAsync())
                {
                    await parkingManagementContext.ParkingGarages.AddRangeAsync(
                        GetPreconfiguredParkingGarages());

                    await parkingManagementContext.SaveChangesAsync();
                }

                if (!await parkingManagementContext.ParkingSpots.AnyAsync())
                {
                    List<ParkingGarage> allGarages = await parkingManagementContext.Set<ParkingGarage>().ToListAsync();
                    foreach (var garage in allGarages)
                    {
                        for (int i = 0; i < garage.NumParkingSpots; i++)
                        {
                            garage.AddParkingSpot("Default Directions", i + 1);
                        }

                    }

                    await parkingManagementContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(parkingManagementContext, logger, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<ParkingGarage> GetPreconfiguredParkingGarages()
        {
            return new List<ParkingGarage>
            {
                new("RIDEAU CTRE - GARAGE", -75.69067629999999, 45.4261102,  "12 Nicholas Street, Ottawa, ON, Canada", 10, 10, 2, false, "", "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=12 Nicholas Street, Ottawa, ON, Canada&key=KEY"),
                new("Ottawa Court House", -75.6902999, 45.42080000000001,  "114 Laurier Ave W, Ottawa, ON, Canada", 10, 10, 2, false, "", "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=114 Laurier Ave W, Ottawa, ON, Canada&key=KEY"),
                new("Standard Life Centre II", -75.69996660000001, 45.41835280000001, "333 Laurier Avenue West, Ottawa, ON, Canada", 10, 10, 2, true, "", "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=333%20Laurier%20Avenue%20West,%20Ottawa,%20ON,%20Canada&key=KEY"),
                new("City of Ottawa Parking Glebe", -75.6890736, 45.4029633, "170 Second Avenue, Ottawa, ON, Canada", 10, 10, 2, true, "", "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=170 Second Avenue, Ottawa, ON, Canada&key=KEY")
            };
        }
    }
}
