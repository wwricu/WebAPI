using SqlSugar;
using WebAPI.Service;
using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class ApplicationDAO
    {
        private SqlSugarClient db;
        public ApplicationDAO()
        {
            db = UtilService.GetDBClient();
        }
        public long Insert(Application application)
        {
            return db.Insertable(application).ExecuteReturnSnowflakeId();
        }
        public long Update(Application application)
        {
            return db.Updateable(application).ExecuteCommand();
        }
        public void Delete(Application application)
        {
            db.Deleteable(application).ExecuteCommand();
        }
        public void Delete(List<Application> applications)
        {
            db.Deleteable(applications).ExecuteCommand();
        }
        public List<Application> Query(Application? application,
                                       SysUser? user,
                                       AssessmentInstance? instance)
        {
            var res = db.Queryable<Application>();

            if (application != null)
            {
                if (application.ApplicationID != 0)
                {
                    res = res.Where(it => it.ApplicationID == application.ApplicationID);
                }
                if (application.Reason != null)
                {
                    res = res.Where(it => it.Reason == application.Reason);
                }
                if (application.SubmitDate != null)
                {
                    res = res.Where(it => it.SubmitDate == application.SubmitDate);
                }
                if (application.Status != null)
                {
                    res = res.Where(it => it.Status == application.Status);
                }
                if (application.StudentID != 0)
                {
                    res = res.Where(it => it.StudentID == application.StudentID);
                }
                if (application.StaffID != 0)
                {
                    res = res.Where(it => it.StaffID == application.StaffID);
                }
            }
            if (instance != null)
            {
                res = res.Includes(it => it.AssessmentInstance)
                         .Where(x => x.AssessmentInstance
                                      .AssessmentID == instance.AssessmentID);
            }
            return res.Includes(it => it.AssessmentInstance)
                      .Includes(it => it.Staff)
                      .Includes(it => it.Student)
                      .Includes(it => it.DocumentList)
                      .ToList();
        }
    }
}
