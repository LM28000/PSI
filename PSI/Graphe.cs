using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace PSI
{
    internal class Graphe
    {
        Dictionary<int, Noeud> noeuds;
        int[,] matriceAdjacence;
        List<int> liste_adjacence;
        Lien[] liens;

        public Graphe(Lien[] liens)
        {
            noeuds = new Dictionary<int, Noeud>();
            this.liens = liens;
        }
        public int Ordre()
        {
            return noeuds.Count;
        }
        public int Taille()
        {
            return liens.Length;
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
        /// Initialiser le graphe avec une liste d'adjacence
        /// </summary>
        public void initialiser_graphe_avec_liste_adjacence()
        {
            liste_adjacence = new List<int>();
            foreach (Lien lien in liens)
            {
                if (!liste_adjacence.Contains(lien.noeud1.id))
                {
                    liste_adjacence.Add(lien.noeud1.id);
                }
                if (!liste_adjacence.Contains(lien.noeud2.id))
                {
                    liste_adjacence.Add(lien.noeud2.id);
                }
            }
        }

        /// <summary>
        /// Afficher la liste d'adjacence du graphe
        /// </summary>
        /// <returns></returns>
        public string toStringListeAdjacence()
        {
            string res = "";
            foreach (int i in liste_adjacence)
            {
                res += i + " : ";
                foreach (Lien lien in liens)
                {
                    if (lien.noeud1.id == i)
                    {
                        res += lien.noeud2.id + " ";
                    }
                    if (lien.noeud2.id == i)
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
        /// <summary>
        /// Vérifier si le graphe contient un circuit
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// Vérifier si le graphe est orienté
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Vérifier si le graphe est multiple
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Modeliser le graphe avec System.Drawing
        /// </summary>
        /// <param name="filename"></param>
        public void ModeliserLeGrapheAvecSystemDrawing(string filename)
        {
            int width = 500, height = 500;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            int rayon = 200;
            Point centre = new Point(width / 2, height / 2);
            Dictionary<int, Point> positions = new Dictionary<int, Point>();
            int totalNoeuds = noeuds.Count;
            int i = 0;

            foreach (var noeud in noeuds.Values)
            {
                double angle = 2 * Math.PI * i / totalNoeuds;
                int x = centre.X + (int)(rayon * Math.Cos(angle));
                int y = centre.Y + (int)(rayon * Math.Sin(angle));
                positions[noeud.id] = new Point(x, y);
                i++;
            }

            Pen pen = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;
            Brush nodeBrush = Brushes.LightBlue;

            foreach (var lien in liens)
            {
                Point p1 = positions[lien.noeud1.id];
                Point p2 = positions[lien.noeud2.id];
                g.DrawLine(pen, p1, p2);
            }

            foreach (var noeud in noeuds.Values)
            {
                Point pos = positions[noeud.id];
                g.FillEllipse(nodeBrush, pos.X - 15, pos.Y - 15, 30, 30);
                g.DrawEllipse(pen, pos.X - 15, pos.Y - 15, 30, 30);
                g.DrawString(noeud.id.ToString(), font, brush, pos.X - 7, pos.Y - 7);
            }

            bitmap.Save(filename, ImageFormat.Png);
        }




    }
}
