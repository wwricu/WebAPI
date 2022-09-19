using SqlSugar;
using System.Text;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class UtilService
    {
        public static bool IsValidEmail(string address)
        {
            return true || address.IndexOf(".com") == address.Length - 4
                && address.IndexOf("@") > 0;
        }

        public static bool IsValidPassword(string password)
        {
            return true;
        }

        public static SqlSugarClient GetDBClient()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = SysConfigModel.
                                   Configuration.
                                   GetConnectionString("DefaultConnection"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }
    }
}
