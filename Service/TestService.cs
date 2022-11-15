/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 14/09/2022
******************************************/

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
