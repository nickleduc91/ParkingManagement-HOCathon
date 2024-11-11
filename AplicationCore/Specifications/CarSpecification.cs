using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class CarSpecification : Specification<Car>
    {
        public CarSpecification(string ownerId)
        {
            Query
            .Where(c => c.OwnerId == ownerId);
            
        }
    }
}
