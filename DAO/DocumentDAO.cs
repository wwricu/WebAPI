using WebAPI.Entity;

namespace WebAPI.DAO
{
    public class DocumentDAO : BaseDAO
    {
        public int Insert(List<Document> documents)
        {
            return db.Insertable(documents).ExecuteCommand();
        }
        public void Delete(List<Document> documents)
        {
            db.Deleteable(documents).ExecuteCommand();
        }
    }
}
