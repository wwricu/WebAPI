/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 25/09/2022
******************************************/

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
        public List<Location> QueryCampus()
        {
            return db.Queryable<Location>()
                     .Select(it => new Location { Campus=it.Campus })
                     .GroupBy(x => new { x.Campus })
                     .ToList();
        }
        public List<Location> QueryBuilding(Location location)
        {
            return db.Queryable<Location>()
                     .Select(it => new Location
                            {
                                Campus = it.Campus,
                                Building = it.Building,
                            })
                     .Where(it => it.Campus == location.Campus)
                     .GroupBy(x => new { x.Campus, x.Building})
                     .ToList();
        }
    }
}
