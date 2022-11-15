/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 16/09/2022
******************************************/

using SqlSugar;
using System.Text;
using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class UtilService : IHostedService
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            InitDAO initDAO = new();
            initDAO.InitDatabase();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //Cleanup logic here
        }
    }
}
