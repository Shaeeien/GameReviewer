namespace GameReviewer.Models
{
    public class ProducerRepository : IRepository<Producer>
    {
        private readonly ReviewContext _reviewContext;

        public ProducerRepository()
        {
            _reviewContext = new ReviewContext();
        }

        public bool Add(Producer entity)
        {
            if (!Exists(entity))
            {
                _reviewContext.Add(entity);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;            
        }

        public bool Exists(Producer entity)
        {
            return _reviewContext.Producers.Contains(entity);
        }

        public IEnumerable<Producer> GetAll()
        {
            return _reviewContext.Producers;
        }

        public Producer? GetById(int id)
        {
            return _reviewContext.Producers.Find(id);
        }

        public Producer? GetByTitle(string name)
        {
            return _reviewContext?.Producers.Where(x => x.Name == name).FirstOrDefault();
        }

        public bool Remove(Producer entity)
        {
            var removedProducer = _reviewContext.Producers.Remove(entity);
            if (removedProducer != null)
            {
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        bool IRepository<Producer>.Remove(int id)
        {
            Producer? producerToRemove = _reviewContext.Producers.Where(x => x.Id == id).FirstOrDefault();
            if (producerToRemove != null)
            {
                _reviewContext.Producers.Remove(producerToRemove);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, Producer updatedEntity)
        {
            Producer? producerToUpdate = _reviewContext.Producers.Where(x => x.Id == id).FirstOrDefault();
            if (producerToUpdate != null)
            {
                producerToUpdate.Desciption = updatedEntity.Desciption;
                producerToUpdate.Name = updatedEntity.Name;
                producerToUpdate.Games = updatedEntity.Games;
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetId(Producer entity)
        {
            return entity.Id;
        }

        public bool ExistsById(int id)
        {
            Producer p = _reviewContext.Producers.Where(x => x.Id == id).FirstOrDefault();
            if (p != null)
                return true;
            return false;
        }
    }
}
