using SqlSugar;
using WebAPI.Entity;
using WebAPI.Model;

namespace WebAPI.DAO
{
    public class UserDAO
    {
        private SqlSugarClient db;
        public UserDAO()
        {
            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = SysConfigModel.
                                   Configuration.
                                   GetConnectionString("DefaultConnection"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(SysUser));
        }

        public int Insert(SysUser user)
        {
            int id = db.Insertable(user).ExecuteCommand();
            return id;
        }

        public List<SysUser> QueryUserByEmail(String Email)
        {
            return db.Queryable<SysUser>().Where(it => it.Email == Email).ToList();
        }
    }
}
