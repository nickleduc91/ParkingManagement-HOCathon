using AplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public sealed class ParkingGarageSpecification : Specification<ParkingGarage>
    {
        public ParkingGarageSpecification(int parkingGarageId)
        {
            Query
            .Where(b => b.Id == parkingGarageId)
            .Include(b => b.ParkingSpots);
        }

        public ParkingGarageSpecification(string userId)
        {
            Query
            .Where(b => b.OwnerId == userId);
        }
    }
}
