using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class LocationDAO : BaseDAO
    {
        public void Insert(List<Location> locations)
        {
            db.Insertable(locations);
        }
        public void Delete(List<Location> locations)
        {
            db.Deleteable(locations);
        }
        public void Update(List<Location> locations)
        {
            db.Updateable(locations);
        }
        public List<Location> Query(Location location)
        {
            var res = db.Queryable<Location>();
            if (location.Campus != null)
            {
                res = res.Where(it => it.Campus == location.Campus);
            }
            if (location.Building != null)
            {
                res = res.Where(it => it.Building == location.Building);
            }
            if (location.Room != null)
            {
                res = res.Where(it => it.Room == location.Room);
            }
            return res.ToList();
        }
    }
}
