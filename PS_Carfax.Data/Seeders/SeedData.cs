using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PS_Carfax.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PS_Carfax.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new CarfaxDbContext();

            // Check if data already exists
            if (context.Histories.Any())
            {
                return;   // DB has been seeded
            }

            // Seed Owners
            var owners = new List<Owner>
            {
                new Owner { Name = "John Doe", Address = "123 Main St", ContactInfo = "123-456-7890" },
                new Owner { Name = "Jane Doe", Address = "456 Elm St", ContactInfo = "098-765-4321" },
                new Owner { Name = "Alice Smith", Address = "789 Pine St", ContactInfo = "111-222-3333" },
                new Owner { Name = "Bob Johnson", Address = "101 Oak St", ContactInfo = "444-555-6666" },
                new Owner { Name = "Eve Williams", Address = "202 Maple St", ContactInfo = "777-888-9999" },
                new Owner { Name = "Mike Brown", Address = "303 Cedar St", ContactInfo = "000-111-2222" },
                new Owner { Name = "Sarah Wilson", Address = "404 Birch St", ContactInfo = "333-444-5555" },
                new Owner { Name = "David Lee", Address = "505 Pine St", ContactInfo = "666-777-8888" },
                new Owner { Name = "Emily Davis", Address = "606 Elm St", ContactInfo = "999-000-1111" },
                new Owner { Name = "Paul Clark", Address = "707 Oak St", ContactInfo = "222-333-4444" }
            };

            context.Owners.AddRange(owners);
            context.SaveChanges();

            // Seed Vehicles
            var vehicles = new List<Vehicle>
            {
                new Vehicle { VIN = "1HGCM82633A004352", Make = "Honda", Model = "Accord", Year = 2003, Color = "Silver", Mileage = 100000, EngineType = EngineType.Petrol, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "2FMDK3GCXABB34567", Make = "Ford", Model = "Escape", Year = 2010, Color = "Black", Mileage = 80000, EngineType = EngineType.Diesel, TransmissionType = TransmissionType.Manual },
                new Vehicle { VIN = "3JHDG5ACJOO122567", Make = "Toyota", Model = "Camry", Year = 2015, Color = "Red", Mileage = 60000, EngineType = EngineType.Hybrid, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "4LKM09HFCYX239840", Make = "Chevrolet", Model = "Malibu", Year = 2018, Color = "White", Mileage = 50000, EngineType = EngineType.Petrol, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "5BHDJ93DFOI984760", Make = "BMW", Model = "X5", Year = 2020, Color = "Blue", Mileage = 40000, EngineType = EngineType.Diesel, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "6OIU4893NDJ32H88", Make = "Audi", Model = "A4", Year = 2017, Color = "Gray", Mileage = 70000, EngineType = EngineType.Petrol, TransmissionType = TransmissionType.Manual },
                new Vehicle { VIN = "7PNE34JDX00293JD", Make = "Mercedes-Benz", Model = "C-Class", Year = 2019, Color = "Black", Mileage = 45000, EngineType = EngineType.Hybrid, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "8XZC01NFO23DII09", Make = "Lexus", Model = "RX 350", Year = 2016, Color = "Silver", Mileage = 55000, EngineType = EngineType.Petrol, TransmissionType = TransmissionType.Automatic },
                new Vehicle { VIN = "9IDNLSK2938D89DK", Make = "Hyundai", Model = "Elantra", Year = 2014, Color = "White", Mileage = 65000, EngineType = EngineType.Diesel, TransmissionType = TransmissionType.Manual },
                new Vehicle { VIN = "10DKJCNM3829KQ32", Make = "Kia", Model = "Optima", Year = 2012, Color = "Black", Mileage = 75000, EngineType = EngineType.Petrol, TransmissionType = TransmissionType.Automatic }
            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

            // Seed ServiceRecords
            var serviceRecords = new List<ServiceRecord>
            {
                new ServiceRecord { Date = DateTime.Now.AddDays(-30), Description = "Oil Change", MileageAtService = 99000, Cost = 50.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-60), Description = "Brake Replacement", MileageAtService = 75000, Cost = 200.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-45), Description = "Tire Rotation", MileageAtService = 85000, Cost = 100.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-75), Description = "Air Filter Replacement", MileageAtService = 80000, Cost = 30.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-55), Description = "Spark Plug Change", MileageAtService = 72000, Cost = 60.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-40), Description = "Battery Replacement", MileageAtService = 70000, Cost = 150.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-35), Description = "Wheel Alignment", MileageAtService = 95000, Cost = 80.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-70), Description = "Transmission Fluid Change", MileageAtService = 50000, Cost = 220.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-50), Description = "Coolant Flush", MileageAtService = 65000, Cost = 90.00 },
                new ServiceRecord { Date = DateTime.Now.AddDays(-65), Description = "Brake Pad Replacement", MileageAtService = 60000, Cost = 180.00 }
            };

            context.ServiceRecords.AddRange(serviceRecords);
            context.SaveChanges();

            // Seed Accidents
            var accidents = new List<Accident>
            {
                new Accident { Date = DateTime.Now.AddDays(-90), Description = "Minor collision", Severity = "Low" },
                new Accident { Date = DateTime.Now.AddDays(-120), Description = "Major collision", Severity = "High" },
                new Accident { Date = DateTime.Now.AddDays(-100), Description = "Fender bender", Severity = "Medium" },
                new Accident { Date = DateTime.Now.AddDays(-110), Description = "Rear-end collision", Severity = "Low" },
                new Accident { Date = DateTime.Now.AddDays(-80), Description = "Side-swipe", Severity = "Medium" },
                new Accident { Date = DateTime.Now.AddDays(-105), Description = "Hit and run", Severity = "High" },
                new Accident { Date = DateTime.Now.AddDays(-95), Description = "Parking lot collision", Severity = "Low" },
                new Accident { Date = DateTime.Now.AddDays(-115), Description = "T-bone collision", Severity = "High" },
                new Accident { Date = DateTime.Now.AddDays(-85), Description = "Intersection accident", Severity = "Medium" },
                new Accident { Date = DateTime.Now.AddDays(-125), Description = "Head-on collision", Severity = "High" }
            };

            context.Accidents.AddRange(accidents);
            context.SaveChanges();

            // Seed History
            var history = new History
            {
                VIN = "1HGCM82633A004352",
                YearGenerated = 2024,
                Vehicle = vehicles[0],
                Owners = owners,
                ServiceRecords = serviceRecords,
                Accidents = accidents
            };

            context.Histories.Add(history);

            var histories = new List<History>
            {
                new History
                {
                    VIN = "1HGCM82633A004352",
                    YearGenerated = 2019,
                    Vehicle = context.Vehicles.FirstOrDefault(v => v.VIN == "1HGCM82633A004352"),
                    Owners = context.Owners.Take(3).ToList(),
                    ServiceRecords = context.ServiceRecords.Take(5).ToList(),
                    Accidents = context.Accidents.Take(3).ToList()
                },
                new History
                {
                    VIN = "2FMDK3GCXABB34567",
                    YearGenerated = 2017,
                    Vehicle = context.Vehicles.FirstOrDefault(v => v.VIN == "2FMDK3GCXABB34567"),
                    Owners = context.Owners.Skip(3).Take(4).ToList(),
                    ServiceRecords = context.ServiceRecords.Skip(5).Take(4).ToList(),
                    Accidents = context.Accidents.Skip(3).Take(2).ToList()
                },
                new History
                {
                    VIN = "3JHDG5ACJOO122567",
                    YearGenerated = 2015,
                    Vehicle = context.Vehicles.FirstOrDefault(v => v.VIN == "3JHDG5ACJOO122567"),
                    Owners = context.Owners.Skip(1).Take(3).ToList(),
                    ServiceRecords = context.ServiceRecords.Skip(3).Take(3).ToList(),
                    Accidents = context.Accidents.Skip(1).Take(3).ToList()
                },
                new History
                {
                    VIN = "4LKM09HFCYX239840",
                    YearGenerated = 2004,
                    Vehicle = context.Vehicles.FirstOrDefault(v => v.VIN == "4LKM09HFCYX239840"),
                    Owners = context.Owners.Skip(4).Take(2).ToList(),
                    ServiceRecords = context.ServiceRecords.Skip(7).Take(2).ToList(),
                    Accidents = context.Accidents.Skip(5).Take(2).ToList()
                }
            };

            context.Histories.AddRange(histories);
            context.SaveChanges();
        }
    }
}