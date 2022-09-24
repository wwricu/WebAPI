using SqlSugar;
using System.Diagnostics;
using WebAPI.Entity;
using WebAPI.Service;

namespace WebAPI.DAO
{
    public class AssessmentDAO
    {
        private SqlSugarClient db;
        public AssessmentDAO()
        {
            db = UtilService.GetDBClient();
        }

        public void Insert(List<AssessmentTemplate>? templates,
                           List<AssessmentInstance>? instances)
        {
            if (templates != null)
                db.Insertable(templates).ExecuteCommand();
            if (instances != null)
                db.Insertable(instances).ExecuteCommand();
        }
        public void Update(Assessment assessment, bool template)
        {
            if (assessment == null) return;
            if (template)
            {
                Debug.WriteLine("update template");
                db.Updateable<AssessmentTemplate>(assessment).ExecuteCommand();
            }
            else
            {
                Debug.WriteLine("update template");
                db.Updateable<AssessmentInstance>(assessment).ExecuteCommand();
            }
        }
        public void Update(List<AssessmentTemplate>? templates,
                           List<AssessmentInstance>? instances)
        {
            if (templates != null)
                db.Updateable(templates).ExecuteCommand();
            if (instances != null)
                db.Updateable(instances).ExecuteCommand();
        }
        public void Delete(Assessment assessment)
        {
            if (assessment == null) return;
            if (assessment.GetType() == typeof(AssessmentTemplate))
                db.Deleteable<AssessmentTemplate>(assessment).ExecuteCommand();
            else if (assessment.GetType() == typeof(AssessmentInstance))
                db.Deleteable<AssessmentInstance>(assessment).ExecuteCommand();
        }
        public void Delete(List<AssessmentTemplate>? templates,
                           List<AssessmentInstance>? instances)
        {
            if (templates != null)
                db.Deleteable(templates).ExecuteCommand();
            if (instances != null)
                db.Deleteable(instances).ExecuteCommand();
        }
        public List<AssessmentTemplate> Query(Assessment? template,
                                              CourseOffering? course)
        {
            var res = db.Queryable<AssessmentTemplate>();

            if (template != null)
            {
                if (template.AssessmentID != 0)
                {
                    res = res.Where(it => it.AssessmentID
                                       == template.AssessmentID);
                }
                if (template.Name != null)
                {
                    res = res.Where(it => it.Name
                                       == template.Name);
                }
                if (template.Type != null)
                {
                    res = res.Where(it => it.Type
                                       == template.Type);
                }
                if (template.EndDate != null)
                {
                    res = res.Where(it => it.EndDate
                                       == template.EndDate);
                }
            }

            if (course != null && course.CourseOfferingID != 0)
            {
                res = res.Where(it => it.CourseOfferingID
                                   == course.CourseOfferingID);
            }

            return res.ToList();
        }
        public List<AssessmentInstance> Query(SysUser? student,
                                              Assessment? template)
        {
            var res = db.Queryable<AssessmentInstance>();

            if (template != null)
            {
                res = res.Where(it => it.BaseAssessmentID
                                   == template.AssessmentID);
            }
            if (student != null) // decide instance or template
            {
                res = res.Where(it => it.StudentID
                                   == student.SysUserID);
            }
            return res.ToList();
        }
    }
}
