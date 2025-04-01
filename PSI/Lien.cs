using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public class Lien<T>
    {
        public Noeud<T> noeud1;
        public Noeud<T> noeud2;
        public T temps;
        public T ligne1;
        public T ligne2;

        public Lien(Noeud<T> noeud1, Noeud<T> noeud2, T temps, T ligne1, T ligne2)
        {
            this.noeud1 = noeud1;
            this.noeud2 = noeud2;
            this.temps = temps;
            this.ligne1 = ligne1;
            this.ligne2 = ligne2;
        }
    }
}
