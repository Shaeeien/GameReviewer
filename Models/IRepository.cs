namespace GameReviewer.Models
{
    public interface IRepository<T>
    {
        public bool Add(T entity);
        public bool Update(int id, T entity);
        public bool Remove(T entity);
        public T? GetById(int id);
        public bool Exists(T entity);
        public bool Remove(int id);
        public IEnumerable<T> GetAll();
        public T? GetByTitle(string title);
        public int GetId(T entity);
    }
}
