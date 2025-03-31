namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lecture = new List<string>();
            lecture = ReadFile("MetroParis.csv"); // lien du  doc
            AfficherListe_Fonction(lecture);
            Console.ReadLine();

            static List<string> ReadFile(string filemname)
            {
                List<string> lecture = new List<string>();
                StreamReader sReader = new StreamReader(filemname);
                string line;
                while ((line = sReader.ReadLine()) != null)
                {
                    lecture.Add(line);
                }
                return lecture;
            }
            
            static void AfficherListe_Fonction(List<string> lecture)
            {
                List<Lien<string>> liens = new List<Lien<string>>();
                Console.WriteLine("--------------------");
                foreach (string line in lecture)
                {
                    string[] noeuds = line.Split(';');

                    //Console.WriteLine(line.Split(','));
                    Noeud<string> noeud1 = new Noeud<string>(noeuds[0]);
                    Noeud<string> noeud2 = new Noeud<string>(noeuds[3]);
                    liens.Add(new Lien<string>(noeud1, noeud2));
                    Console.WriteLine(line);
                }
                Console.WriteLine("--------------------");
                foreach (Lien<string> lien in liens)
                {
                    Console.WriteLine("Lien entre noeud " + lien.noeud1.id + " et noeud " + lien.noeud2.id);
                }

            }
            //Créer liens

        }
            ////Afficher les liens
            
    //Graphe<int> graphe = new Graphe<int>(liens.ToArray());
    //graphe.Initialiser();
    //graphe.initialiser_graphe_avec_liste_adjacence();
    //Console.WriteLine(graphe.toStringListeAdjacence());
    //Console.WriteLine(graphe.toStringMatriceAdjacence());
    //Console.WriteLine("Parcours en largeur : ");
    //graphe.ParcoursLargeur();
    //Console.WriteLine("Parcours en profondeur : ");
    //graphe.ParcoursProfondeur();
    //Console.WriteLine("Est connexe : ");
    //Console.WriteLine(graphe.EstConnexe());
    //Console.WriteLine("Contient circuit : ");
    //Console.WriteLine(graphe.ContientCircuit());
    //Console.WriteLine("Est multiple : ");
    //Console.WriteLine(graphe.estmultiple());
    //Console.WriteLine("Est pondere : ");
    //Console.WriteLine(false);
    //graphe.ModeliserLeGrapheAvecSystemDrawing("image.jpg");



}
    }
