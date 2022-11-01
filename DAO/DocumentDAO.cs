using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class DocumentDAO : BaseDAO
    {
        public void Insert(List<Document> documents)
        {
            db.Insertable(documents).ExecuteCommand();
        }
        public void Delete(List<Document> documents)
        {
            db.Deleteable(documents).ExecuteCommand();
        }
    }
}
