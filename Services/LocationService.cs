using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDP_Stefan.Services
{
    public class LocationService : ILocationService
    {
        private Context.ContextDb _context;

        public LocationService(ContextDb context)
        {
            _context = context;
        }

        public async Task<Location> AddLocation(Location location)
        {
            _context.Location.Add(location);
            _context.SaveChanges();
            return location;
        }

        public async Task DeleteLocation(Location location)
        {
            _context.Location.Remove(location);
            _context.SaveChanges();
        }
        public async Task<Location> EditLocation(Location location)
        {
            var existingLocation = _context.Location.Find(location.Id);
            if (existingLocation != null)
            {
                existingLocation.Name = location.Name;
                existingLocation.Adress = location.Adress;
                existingLocation.TelNumber = location.TelNumber;

                _context.Location.Update(existingLocation);
                _context.SaveChanges();
            }
            return location;
        }
        public async Task<Location> GetLocationById(int id)
        {
            return _context.Location.SingleOrDefault(x => x.Id == id);
        }
        public async Task<Location> GetLocationByName(string name)
        {
            return _context.Location.SingleOrDefault(x => x.Name == name);
        }
        public async Task<List<Location>> GetAllLocations()
        {
            return _context.Location.ToList();
        }
    }
}
