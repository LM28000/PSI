using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public class Noeud<T>
    {
        public T id;
        public Noeud(T id)
        {
            this.id = id;
        }
    }
}
