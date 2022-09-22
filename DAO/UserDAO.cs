using SqlSugar;
using System.Diagnostics;
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
        }

        public int Insert(SysUser user)
        {
            if (db.Queryable<SysUser>()
                 .Where(it => it.UserNumber == user.UserNumber)
                 .Count() != 0)
            {
                throw new Exception("The user existed!");
            }
            int id = db.Insertable(user).ExecuteCommand();
            return id;
        }

        public List<SysUser> QueryUserByNumber(string UserNumber)
        {
            return db.Queryable<SysUser>()
                     .Where(it => it.UserNumber == UserNumber)
                     .ToList();
        }

        public List<SysUser> QueryUsers(PrivateInfoModel info,
                                        int PageNumber,
                                        int PageSize)
        {
            var res = db.Queryable<SysUser>().
                         Where(it => it.Permission == info.Permission);
            if (PageNumber > 0 && PageSize > 0)
            {
                int TotalCount = 0;
                res.ToPageList(PageNumber, PageSize, ref TotalCount);
            }
            if (info.Email != null)
            {
                res = res.Where(it => it.Email == info.Email);
            }
            if (info.UserNumber != null)
            {
                res = res.Where(it => it.UserNumber == info.UserNumber);
            }
            if (info.UserName != null)
            {
                res = res.Where(it => it.UserName.Contains(info.UserName));
            }
            if (info.Academic != null)
            {
                res = res.Where(it => it.Academic == info.Academic);
            }

            return res.IgnoreColumns(it => it.PasswordHash).
                       // IgnoreColumns(it => it.SysUserID).
                       IgnoreColumns(it => it.CourseOfferingList).
                       IgnoreColumns(it => it.Salt).ToList();
        }
        public List<SysUser> QueryUsers(PrivateInfoModel info,
                                        CourseOffering course)
        {
            var res = db.Queryable<SysUser>().
                         Where(it => it.Permission == info.Permission);

            if (info.Email != null)
            {
                res = res.Where(it => it.Email == info.Email);
            }
            if (info.UserNumber != null)
            {
                res = res.Where(it => it.UserNumber == info.UserNumber);
            }
            if (info.UserName != null)
            {
                res = res.Where(it => it.UserName.Contains(info.UserName));
            }
            if (info.Academic != null)
            {

                res = res.Where(it => it.Academic == info.Academic);
            }

            if (course != null)
            {
                if (info.Permission == 1)
                {
                    res = res.Includes(x => x.CourseOfferingList)
                             .Where(y => y.CourseOfferingList
                             .Any(z => z.CourseOfferingID
                                  == course.CourseOfferingID));
                } else if (info.Permission > 1)
                {
                    res = res.Includes(x => x.StaffOfferingList)
                             .Where(y => y.StaffOfferingList
                             .Any(z => z.CourseOfferingID
                                  == course.CourseOfferingID));
                }

            }

            Debug.WriteLine(res.ToList().Count);

            return res.IgnoreColumns(it => it.PasswordHash).
                       IgnoreColumns(it => it.SysUserID).
                       IgnoreColumns(it => it.Salt).ToList();
        }

        public bool UpdateUser(SysUser UserInfo)
        {
            UserInfo.SysUserID = db.Queryable<SysUser>()
                       .Where(it => it.UserNumber == UserInfo.UserNumber)
                       .Select(it => it.SysUserID)
                       .First();
            var res = db.Updateable(UserInfo);
            if (UserInfo.Addresses[0] == null
             && UserInfo.Addresses[1] == null
             && UserInfo.Addresses[2] == null)
            {
                res = res.IgnoreColumns(it => new {it.Addresses});
            }
            if (UserInfo.UserName == null)
            {
                res = res.IgnoreColumns(it => new { it.UserName });
            }

            /*Debug.WriteLine(Convert.ToString(UserInfo.SysUserID));
            Debug.WriteLine(UserInfo.UserNumber);
            Debug.WriteLine(UserInfo.Permission);*/

            return res.IgnoreColumns(ignoreAllNullColumns: true)
                      .ExecuteCommandHasChange();
        }

        public void DeleteUser(String userNumber)
        {

            db.DeleteNav<SysUser>(x => x.UserNumber
                                      == userNumber)
                            .Include(x => x.CourseOfferingList,
                                        new DeleteNavOptions()
                                        {
                                            ManyToManyIsDeleteA = true
                                        })
                            .Include(x => x.StaffOfferingList,
                                        new DeleteNavOptions()
                                        {
                                            ManyToManyIsDeleteA = true
                                        })
                            .ExecuteCommand();
        }
    }
}
