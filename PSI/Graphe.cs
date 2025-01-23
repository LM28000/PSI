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
        int[,] matriceAdjacence;
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
            matriceAdjacence = new int[noeuds.Count, noeuds.Count];
            foreach (Lien lien in liens)
            {
                matriceAdjacence[lien.noeud1.id-1, lien.noeud2.id-1] = 1;
                matriceAdjacence[lien.noeud2.id-1, lien.noeud1.id-1] = 1;
            }
        }

        public string toStringListeAdjacence()
        {
            string res = "";
            foreach (Noeud noeud in noeuds.Values)
            {
                res += noeud.id + " : ";
                foreach (Lien lien in liens)
                {
                    if (lien.noeud1.id == noeud.id)
                    {
                        res += lien.noeud2.id + " ";
                    }
                    if (lien.noeud2.id == noeud.id)
                    {
                        res += lien.noeud1.id + " ";
                    }
                }
                res += "\n";
            }
            return res;
        }
        public string toStringMatriceAdjacence()
        {
            string res = "";
            for (int i = 0; i < matriceAdjacence.GetLength(0); i++)
            {
                for (int j = 0; j < matriceAdjacence.GetLength(1); j++)
                {
                    res += matriceAdjacence[i, j] + " ";
                }
                res += "\n";
            }
            return res;
        }


    }
}
