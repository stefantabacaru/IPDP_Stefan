using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPDP_Stefan.Interfaces
{
    public interface ILocationService
    {
        public Task<Location> AddLocation(Location location);
        public Task DeleteLocation(Location location);
        public Task<Location> EditLocation(Location location);
        public Task<Location> GetLocationById(int id);
        public Task<Location> GetLocationByName(string name);
        public Task<List<Location>> GetAllLocations();
    }
}
