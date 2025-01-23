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
            List<Lien> liens = new List<Lien>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] noeuds = lines[i].Split(' ');
                Noeud noeud1 = new Noeud(int.Parse(noeuds[0]));
                Noeud noeud2 = new Noeud(int.Parse(noeuds[1]));
                liens.Add(new Lien(noeud1, noeud2));
            }
            //Afficher les liens
            foreach (Lien lien in liens)
            {
                Console.WriteLine("Lien entre noeud " + lien.noeud1.id + " et noeud " + lien.noeud2.id);
            }
            Graphe graphe = new Graphe(liens.ToArray());
            graphe.Initialiser();
            Console.WriteLine(graphe.toStringListeAdjacence());
            Console.WriteLine(graphe.toStringMatriceAdjacence());






        }
    }
}
