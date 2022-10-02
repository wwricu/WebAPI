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
        public void Insert(Application application)
        {
            db.Insertable(application).ExecuteCommand();
        }
        public void Update(Application application)
        {
            db.Updateable(application).ExecuteCommand();
        }
        public List<Application> Query(Application? application,
                                       SysUser? user,
                                       AssessmentTemplate? template)
        {
            var res = db.Queryable<Application>();

            if (application != null)
            {
                if (application.ApplicationID != 0)
                {
                    res = res.Where(it => it.ApplicationID == application.ApplicationID);
                }
                if (application.Type != null)
                {
                    res = res.Where(it => it.Type == application.Type);
                }
                if (application.SubmitDate != null)
                {
                    res = res.Where(it => it.SubmitDate == application.SubmitDate);
                }
                if (application.Status != null)
                {
                    res = res.Where(it => it.Status == application.Status);
                }
            }
            if (user != null && user.SysUserID != 0 )
            {
                if (user.Permission == 1)
                {
                    res = res.Where(it => it.StudentID == user.SysUserID);
                }
                else if (user.Permission == 2)
                {
                    res = res.Where(it => it.StaffID == user.SysUserID);
                }
            }
            if (template != null)
            {
                res = res.Includes(it => it.AssessmentInstance)
                         .Where(x => x.AssessmentInstance
                                      .BaseAssessmentID == template.AssessmentID);
            }

            return res.ToList();
        }
    }
}
