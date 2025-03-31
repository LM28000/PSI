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
        public string name;
        public T ligne;
        public T changement;
        public Noeud(T id, string name, T ligne, T changement)
        {
            this.id = id;
            this.name = name;
            this.ligne = ligne;
            this.changement = changement;
        }
    }
}
