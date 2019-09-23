﻿using System;
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
            entities.RemoveAll(e=>e.Id.Equals(id));
        }

        public T Get(object id)
        {
            return entities.Find(e=>e.Id.Equals(id));
        }

        public void Save(T item)
        {
            entities.Add(item);
        }

    }
}
