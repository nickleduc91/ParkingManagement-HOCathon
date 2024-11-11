using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Car: BaseEntity, IAggregateRoot
    {
        
        public string LicensePlateNumber { get; set; } = null!;
        public string OwnerId { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Make { get; set; } = null!;
        public string Colour { get; set; } = null!;

        public Car(string licensePlateNumber, string ownerId, string model, string make, string colour)
        {
            LicensePlateNumber = licensePlateNumber;
            OwnerId = ownerId;
            Model = model;
            Make = make;
            Colour = colour;

        }
        
        public void Update(string licensePlateNumber, string model, string make, string colour)
        {
            LicensePlateNumber = licensePlateNumber;
            Model = model;
            Make = make;
            Colour = colour;

        }
    }
}
