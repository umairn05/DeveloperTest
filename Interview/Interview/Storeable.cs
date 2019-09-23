using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
        public class Storeable<T> : IStoreable<T>
        {
            public Storeable(T newId )
            {
                Id = newId;
            }

            public T Id { get; set; }
        }
}
