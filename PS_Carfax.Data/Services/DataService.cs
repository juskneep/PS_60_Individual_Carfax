using System.Collections.ObjectModel;
using PS_Carfax.Data;
using PS_Carfax.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PS_Carfax.UI.Services
{
    public class DataService
    {
        private readonly CarfaxDbContext _dbContext;

        public DataService()
        {
            _dbContext = new CarfaxDbContext();
        }

        public ObservableCollection<Vehicle> GetVehicles()
        {
            return new ObservableCollection<Vehicle>(_dbContext.Vehicles.ToList());
        }

        public ObservableCollection<ServiceRecord> GetServiceRecords()
        {
            return new ObservableCollection<ServiceRecord>(_dbContext.ServiceRecords.ToList());
        }

        public ObservableCollection<Owner> GetOwners()
        {
            return new ObservableCollection<Owner>(_dbContext.Owners.ToList());
        }

        public ObservableCollection<Accident> GetAccidents()
        {
            return new ObservableCollection<Accident>(_dbContext.Accidents.ToList());
        }

        public ObservableCollection<History> GetHistories()
        {
            return new ObservableCollection<History>(_dbContext.Histories.ToList());
        }

        public List<History> GetAllHistories()
        {
            return _dbContext.Histories.ToList();
        }

        public List<History> SearchHistories(int? yearGenerated, string? ownerName, string? model, string? make)
        {
            var query = _dbContext.Histories.Include(h => h.Owners)
                .Include(h => h.Accidents)
                .Include(h => h.ServiceRecords).Include(h => h.Vehicle).AsQueryable();

            if (yearGenerated.HasValue)
            {
                // Try to find histories for the specified year
                var historiesForYear = query.Where(h => h.YearGenerated == yearGenerated).ToList();

                // If no histories found for the specified year, find the nearest year
                if (!historiesForYear.Any())
                {
                    var nearestYear = GetNearestYear(yearGenerated.Value);
                    if (nearestYear.HasValue)
                    {
                        query = query.Where(h => h.YearGenerated == nearestYear);
                    }
                }
                else
                {
                    query = historiesForYear.AsQueryable();
                }
            }

            if (!string.IsNullOrEmpty(ownerName))
            {
                query = query.Where(h => h.Owners.Any(owner => owner.Name == ownerName));
            }

            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(h => h.Vehicle.Model.Contains(model));
            }

            if (!string.IsNullOrEmpty(make))
            {
                query = query.Where(h => h.Vehicle.Make.Contains(make));
            }

            return query.ToList();
        }
         
        private int? GetNearestYear(int targetYear)
        {
            var nearestYear = _dbContext.Histories.Where(h => h.YearGenerated >= targetYear)
                                                   .OrderBy(h => h.YearGenerated)
                                                   .Select(h => h.YearGenerated)
                                                   .FirstOrDefault();

            if (nearestYear == 0)
            {
                nearestYear = _dbContext.Histories.Where(h => h.YearGenerated < targetYear)
                                                   .OrderByDescending(h => h.YearGenerated)
                                                   .Select(h => h.YearGenerated)
                                                   .FirstOrDefault();
            }

            return nearestYear;
        }


        public void CreateHistory(History history)
        {
            // Add Vehicle
            if (history.Vehicle != null && !_dbContext.Vehicles.Any(v => v.VIN == history.Vehicle.VIN))
            {
                _dbContext.Vehicles.Add(history.Vehicle);
            }

            // Add Owners
            foreach (var owner in history.Owners)
            {
                if (!_dbContext.Owners.Any(o => o.Id == owner.Id))
                {
                    _dbContext.Owners.Add(owner);
                }
            }

            // Add Accidents
            foreach (var accident in history.Accidents)
            {
                if (!_dbContext.Accidents.Any(a => a.Id == accident.Id))
                {
                    _dbContext.Accidents.Add(accident);
                }
            }

            // Add Service Records
            foreach (var serviceRecord in history.ServiceRecords)
            {
                if (!_dbContext.ServiceRecords.Any(sr => sr.Id == serviceRecord.Id))
                {
                    _dbContext.ServiceRecords.Add(serviceRecord);
                }
            }

            // Add History
            _dbContext.Histories.Add(history);

            // Save changes
            _dbContext.SaveChanges();
        }

    }
}
