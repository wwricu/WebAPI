/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 23/09/2022
******************************************/

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
        public void Update(Assessment assessment)
        {
            if (assessment == null) return;
            if (assessment is AssessmentTemplate)
            {
                Debug.WriteLine("update template");
                db.Updateable<AssessmentTemplate>(assessment).ExecuteCommand();
            }
            else if (assessment is AssessmentInstance)
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
            if (assessment is AssessmentTemplate)
                db.Deleteable<AssessmentTemplate>(assessment).ExecuteCommand();
            else if (assessment is AssessmentInstance)
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
            var res = db.Queryable<AssessmentTemplate>()
                        .Includes(it => it.Location);

            if (template != null)
            {
                if (template.AssessmentID != null)
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
            var res = db.Queryable<AssessmentInstance>()
                        .Includes(it => it.Location);

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
        public List<AssessmentInstance> Query(AssessmentInstance instance)
        {
            var res = db.Queryable<AssessmentInstance>()
                        .Includes(it => it.Location);

            if (instance.AssessmentID != null)
            {
                res = res.Where(it => it.AssessmentID == instance.AssessmentID);
            }
            if (instance.Name != null)
            {
                res = res.Where(it => it.Name == instance.Name);
            }
            if (instance.CourseOfferingName != null)
            {
                res = res.Where(it => it.CourseOfferingName
                            == instance.CourseOfferingName);
            }
            if (instance.CourseOfferingID != 0)
            {
                res = res.Where(it => it.CourseOfferingID
                            == instance.CourseOfferingID);
            }
            return res.ToList();
        }
    }
}
