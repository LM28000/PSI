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
    public class Graphe<T>
    {
        Dictionary<T, Noeud<T>> noeuds;
        int[,] matriceAdjacence;
        List<T> liste_adjacence;
        Lien<T>[] liens;

        public Graphe(Lien<T>[] liens)
        {
            noeuds = new Dictionary<T, Noeud<T>>();
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
            foreach (Lien<T> lien in liens)
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
            foreach (Lien<T> lien in liens)
            {
                matriceAdjacence[Convert.ToInt32(lien.noeud1.id) - 1, Convert.ToInt32(lien.noeud2.id) - 1] = 1;
                matriceAdjacence[Convert.ToInt32(lien.noeud2.id) - 1, Convert.ToInt32(lien.noeud1.id) - 1] = 1;
            }
        }

        /// <summary>
        /// Initialiser le graphe avec une liste d'adjacence
        /// </summary>
        public void initialiser_graphe_avec_liste_adjacence()
        {
            liste_adjacence = new List<T>();
            foreach (Lien<T> lien in liens)
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
            foreach (T i in liste_adjacence)
            {
                res += i + " : ";
                foreach (Lien<T> lien in liens)
                {
                    if (lien.noeud1.id.Equals(i))
                    {
                        res += lien.noeud2.id + " ";
                    }
                    if (lien.noeud2.id.Equals(i))
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
        public List<Noeud<T>> ParcoursLargeur()
        {
            Queue<Noeud<T>> file = new Queue<Noeud<T>>();
            List<Noeud<T>> noeudsParcourus = new List<Noeud<T>>();
            file.Enqueue(noeuds.Values.First());
            noeudsParcourus.Add(noeuds.Values.First());
            while (file.Count > 0)
            {
                Noeud<T> noeud = file.Dequeue();
                foreach (Lien<T> lien in liens)
                {
                    if (lien.noeud1.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        file.Enqueue(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud1))
                    {
                        file.Enqueue(lien.noeud1);
                        noeudsParcourus.Add(lien.noeud1);
                    }
                }
            }
            foreach (Noeud<T> noeud in noeudsParcourus)
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
            List<Noeud<T>> parcours = ParcoursLargeur();
            foreach (Noeud<T> noeud in parcours)
            {
                tab[Convert.ToInt32(noeud.id) - 1] = 1;
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
            Stack<Noeud<T>> pile = new Stack<Noeud<T>>();
            List<Noeud<T>> noeudsParcourus = new List<Noeud<T>>();
            pile.Push(noeuds.Values.First());
            noeudsParcourus.Add(noeuds.Values.First());
            while (pile.Count > 0)
            {
                Noeud<T> noeud = pile.Pop();
                foreach (Lien<T> lien in liens)
                {
                    if (lien.noeud1.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        pile.Push(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud1))
                    {
                        pile.Push(lien.noeud1);
                        noeudsParcourus.Add(lien.noeud1);
                    }
                }
            }
            foreach (Noeud<T> noeud in noeudsParcourus)
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
            Stack<Noeud<T>> pile = new Stack<Noeud<T>>();
            List<Noeud<T>> noeudsParcourus = new List<Noeud<T>>();
            pile.Push(noeuds.Values.First());
            noeudsParcourus.Add(noeuds.Values.First());
            while (pile.Count > 0)
            {
                Noeud<T> noeud = pile.Pop();
                foreach (Lien<T> lien in liens)
                {
                    if (lien.noeud1.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud2))
                    {
                        pile.Push(lien.noeud2);
                        noeudsParcourus.Add(lien.noeud2);
                    }
                    if (lien.noeud2.id.Equals(noeud.id) && !noeudsParcourus.Contains(lien.noeud1))
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
            foreach (Lien<T> lien in liens)
            {
                if (lien.noeud1.id.Equals(lien.noeud2.id))
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
            foreach (Lien<T> lien in liens)
            {
                foreach (Lien<T> lien2 in liens)
                {
                    if (lien.noeud1.id.Equals(lien2.noeud1.id) && lien.noeud2.id.Equals(lien2.noeud2.id))
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
            Dictionary<T, Point> positions = new Dictionary<T, Point>();
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