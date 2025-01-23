using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    internal class Graphe
    {
        Dictionary<int, Noeud> noeuds;
        Lien[] liens;

        public Graphe(Lien[] liens)
        {
            noeuds = new Dictionary<int, Noeud>();
            this.liens = liens;

        }

        public void Initialiser()
        {
            foreach (Lien lien in liens)
            {
                if (!noeuds.ContainsKey(lien.noeud1.id))
                {
                    noeuds.Add(lien.noeud1.id, lien.noeud1);
                }
                if (!noeuds.ContainsKey(lien.noeud2.id))
                {
                    noeuds.Add(lien.noeud2.id, lien.noeud2);
                }
            }
        }

        public string toString()
        {
            string res = "";
            foreach (Lien lien in liens)
            {
                res += lien.noeud1.id + " -> " + lien.noeud2.id + "\n";
            }
            return res;
        }


    }
}
