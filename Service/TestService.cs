using SqlSugar;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class TestService
    {
        public static void SugarTest()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=.;uid=wwr;pwd=153226;database=INFT6900",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = false,
                InitKeyType = InitKeyType.Attribute
            });

            db.DbMaintenance.CreateDatabase();
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(TestEntity));

            db.Insertable<TestEntity>(new TestEntity()).ExecuteCommand();
        }
    }
}
