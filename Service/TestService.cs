using SqlSugar;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class TestService
    {
        public static void SugarTest()
        {
            SqlSugarClient db = UtilService.GetDBClient();

            db.DbMaintenance.CreateDatabase();
        }
    }
}
