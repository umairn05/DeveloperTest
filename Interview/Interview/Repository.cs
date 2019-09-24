using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{ 

     public class Repository<T> : IRepository<T,object> where T : IStoreable<object>
    {
        private List<T> entities;

        public Repository(List<T> storeables)
        {
            entities = storeables;
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public void Delete(object id)
        {
            if (id == null) throw new ArgumentNullException(string.Format("Id {0} must not be null", id));
            if (!entities.Exists(s => s.Id.Equals(id))) throw new ArgumentException(string.Format("Id {0} does not exist", id));


            entities.RemoveAll(e => e.Id.Equals(id));
        
        }

        public T Get(object id)
        {
            if (id == null) throw new ArgumentNullException(string.Format("Id {0} must not be null", id));
            if(!entities.Exists(s=>s.Id.Equals(id))) throw new ArgumentException(string.Format("Id {0} does not exist", id));


            return entities.Find(e => e.Id.Equals(id));
            
        }

        public void Save(T item)
        {
            if (item.Id == null) throw new ArgumentNullException(string.Format("Id {0} must not be null", item.Id));
            if (entities.Exists(s => s.Id.Equals(item.Id))) throw new ArgumentException(string.Format("Id {0} already exist", item.Id));
            entities.Add(item);
        }

    }
}
