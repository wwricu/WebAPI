using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class DocumentDAO : BaseDAO
    {
        public void Insert(List<Document> documents)
        {
            db.Insertable(documents).ExecuteCommand();
        }
        public List<Document> Query(Document document)
        {
            var res = db.Queryable<Document>();
            if (document.DocumentID != 0)
            {
                res = res.Where(it => it.DocumentID == document.DocumentID);
            }
            if (document.ApplicationID != 0)
            {
                res = res.Where(it => it.ApplicationID == document.ApplicationID);
            }
            if (document.Url != null)
            {
                res = res.Where(it => it.Url == document.Url);
            }
            return res.ToList();
        }
        public void Delete(List<Document> documents)
        {
            db.Deleteable(documents).ExecuteCommand();
        }
    }
}
