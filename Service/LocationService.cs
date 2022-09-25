using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class LocationService
    {
        public static List<Location> GetLocations(Location location)
        {
            if (location.LocationID != 0)
            {
                return new LocationDAO().Query(location);
            }
            if (location.Campus == null)
            {
                return new LocationDAO().QueryCampus();
            }
            if (location.Building == null)
            {
                return new LocationDAO().QueryBuilding(location);
            }
            return new LocationDAO().Query(location);
        }
    }
}
