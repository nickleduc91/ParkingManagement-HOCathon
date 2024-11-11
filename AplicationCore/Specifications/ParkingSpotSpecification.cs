using AplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public sealed class ParkingSpotSpecification : Specification<ParkingSpot>
    {
        public ParkingSpotSpecification(int spotId)
        {
            Query
            .Where(s => s.Id == spotId);
        }
        public ParkingSpotSpecification(string userId)
        {
            Query
            .Where(s => (s.UserId == userId && s.IsAvailable == false));
        }
    }
}
