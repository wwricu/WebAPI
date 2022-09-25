using SqlSugar;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class BaseDAO
    {
        protected SqlSugarClient db;
        public BaseDAO()
        {
            db = UtilService.GetDBClient();
        }
    }
}
