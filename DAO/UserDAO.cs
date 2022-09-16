using SqlSugar;
using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class UserDAO
    {
        private SqlSugarClient db;
        public UserDAO()
        {
            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = SysConfig.
                                   Configuration.
                                   GetConnectionString("DefaultConnection"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Student));
            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Staff));
        }

        public int Insert(Student user)
        {
            int id = db.Insertable(user).ExecuteCommand();
            return id;
        }
        public int Insert(Staff user)
        {
            int id = db.Insertable(user).ExecuteCommand();
            return id;
        }
        public List<Staff> QueryStaffByEmail(String Email)
        {
            return db.Queryable<Staff>().Where(it => it.Email == Email).ToList();
        }
        public List<Student> QueryStudentByEmail(String Email)
        {
            return db.Queryable<Student>().Where(it => it.Email == Email).ToList();
        }
    }
}
