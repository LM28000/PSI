namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "soc-karate.mtx";
            //Lire le fichier avec ReadLines
            string[] lines = File.ReadAllLines(filepath);
            //Créer liens
            List<Lien<int>> liens = new List<Lien<int>>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] noeuds = lines[i].Split(' ');
                Noeud<int> noeud1 = new Noeud<int>(int.Parse(noeuds[0]));
                Noeud<int> noeud2 = new Noeud<int>(int.Parse(noeuds[1]));
                liens.Add(new Lien<int>(noeud1, noeud2));
            }
            //Afficher les liens
            foreach (Lien<int> lien in liens)
            {
                Console.WriteLine("Lien entre noeud " + lien.noeud1.id + " et noeud " + lien.noeud2.id);
            }
            Graphe<int> graphe = new Graphe<int>(liens.ToArray());
            graphe.Initialiser();
            graphe.initialiser_graphe_avec_liste_adjacence();
            Console.WriteLine(graphe.toStringListeAdjacence());
            Console.WriteLine(graphe.toStringMatriceAdjacence());
            Console.WriteLine("Parcours en largeur : ");
            graphe.ParcoursLargeur();
            Console.WriteLine("Parcours en profondeur : ");
            graphe.ParcoursProfondeur();
            Console.WriteLine("Est connexe : ");
            Console.WriteLine(graphe.EstConnexe());
            Console.WriteLine("Contient circuit : ");
            Console.WriteLine(graphe.ContientCircuit());
            Console.WriteLine("Est multiple : ");
            Console.WriteLine(graphe.estmultiple());
            Console.WriteLine("Est pondere : ");
            Console.WriteLine(false);
            graphe.ModeliserLeGrapheAvecSystemDrawing("image.jpg");



        }
    }
}
