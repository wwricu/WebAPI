/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 25/09/2022
******************************************/

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
