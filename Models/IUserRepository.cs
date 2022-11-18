namespace GameReviewer.Models
{
    public interface IUserRepository<T>
    {
        public bool Add(T entity);
        public bool Update(int id, T entity);
        public bool Remove(T entity);
        public T? GetById(int id);
        public bool Exists(T entity);
        public bool ExistsById(int id);
        public bool Remove(int id);
        public IEnumerable<T> GetAll();
        public T? GetByTitle(string title);
        public int GetId(T entity);
        public bool Login(string login, string password);
    }
}
