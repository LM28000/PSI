using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        /// <summary>
        /// Initialiser le graphe
        /// </summary>
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
        /// <summary>
        /// Afficher la liste d'adjacence du graphe
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Afficher la matrice d'adjacence du graphe
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Parcours en largeur du graphe
        /// </summary>
        /// <returns></returns>
        public List<Noeud> ParcoursLargeur()
        {
            Queue<Noeud> file = new Queue<Noeud>();
            List<Noeud> noeudsParcourus = new List<Noeud>();
            file.Enqueue(noeuds[1]);
            noeudsParcourus.Add(noeuds[1]);
            while (file.Count > 0)
            {
                Noeud noeud = file.Dequeue();
                foreach (Lien lien in liens)
                {
                    if (lien.noeud1.id == noeud.id && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        file.Enqueue(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id == noeud.id && !noeudsParcourus.Contains(lien.noeud1))
                    {
                        file.Enqueue(lien.noeud1);
                        noeudsParcourus.Add(lien.noeud1);
                    }
                }
            }
            foreach (Noeud noeud in noeudsParcourus)
            {
                Console.WriteLine(noeud.id);
            }
            return noeudsParcourus;
        }
        /// <summary>
        /// Vérifier si le graphe est connexe
        /// </summary>
        /// <returns></returns>
        public bool EstConnexe()
        {
            int[] tab = new int[noeuds.Count];
            List<Noeud> parcours = ParcoursLargeur();
            foreach (Noeud noeud in parcours)
            {
                tab[noeud.id - 1] = 1;
            }

            foreach (int i in tab)
            {
                if (i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Parcours en profondeur du graphe
        /// </summary>
        public void ParcoursProfondeur()
        {
            Stack<Noeud> pile = new Stack<Noeud>();
            List<Noeud> noeudsParcourus = new List<Noeud>();
            pile.Push(noeuds[1]);
            noeudsParcourus.Add(noeuds[1]);
            while (pile.Count > 0)
            {
                Noeud noeud = pile.Pop();
                foreach (Lien lien in liens)
                {
                    if (lien.noeud1.id == noeud.id && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        pile.Push(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id == noeud.id && !noeudsParcourus.Contains(lien.noeud1))
                    {
                        pile.Push(lien.noeud1);
                        noeudsParcourus.Add(lien.noeud1);
                    }
                }
            }
            foreach (Noeud noeud in noeudsParcourus)
            {
                Console.WriteLine(noeud.id);
            }
        }
        //Ecrire une fonction qui retourne vrai si le graphe contient un circuit
        public bool ContientCircuit()
        {
            Stack<Noeud> pile = new Stack<Noeud>();
            List<Noeud> noeudsParcourus = new List<Noeud>();
            pile.Push(noeuds[1]);
            noeudsParcourus.Add(noeuds[1]);
            while (pile.Count > 0)
            {
                Noeud noeud = pile.Pop();
                foreach (Lien lien in liens)
                {
                    if (lien.noeud1.id == noeud.id && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        pile.Push(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id == noeud.id && !noeudsParcourus.Contains(lien.noeud1))
                    {
                        pile.Push(lien.noeud1);
                        noeudsParcourus.Add(lien.noeud1);
                    }
                    if (noeudsParcourus.Contains(lien.noeud1) && noeudsParcourus.Contains(lien.noeud2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int Ordre()
        {
            return noeuds.Count;
        }
        public int Taille()
        {
            return liens.Length;
        }
        public bool estorienté()
        {
            foreach (Lien lien in liens)
            {
                if (lien.noeud1.id == lien.noeud2.id)
                {
                    return false;
                }
            }
            return true;
        }
        public bool estmultiple()
        {
            foreach (Lien lien in liens)
            {
                foreach (Lien lien2 in liens)
                {
                    if (lien.noeud1.id == lien2.noeud1.id && lien.noeud2.id == lien2.noeud2.id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void modeliserlegrapheavecSystemDrawing()
        {
            Bitmap bmp = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(bmp);
            Pen pen = new Pen(Color.Black);
            foreach (Lien lien in liens)
            {
                g.DrawLine(pen, lien.noeud1.id * 10, lien.noeud2.id * 10, lien.noeud2.id * 10, lien.noeud1.id * 10);
            }
            bmp.Save("graphe.bmp");
        }




    }
}
