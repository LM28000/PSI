using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    internal class Graphe
    {
        Noeud[] noeuds;
        Lien[] liens;

        public Graphe(Noeud[] noeuds, Lien[] liens)
        {
            this.noeuds = noeuds;
            this.liens = liens;
        }
    }
}
