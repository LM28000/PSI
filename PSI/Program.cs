using MySql.Data.MySqlClient;
namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=louis;");
                connection.Open();
                Console.WriteLine("Connection opened");

                CreateDatabaseAndTables(connection);
                InsertData(connection);
                SelectData(connection, "SELECT * FROM Client;");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    Console.WriteLine("Connection closed");
                }

                List<string> list1 = new List<string>();
                list1 = ReadFile("MetroParisfinal.csv");
                List<string> list2 = new List<string>();
                list2 = ReadFile("MetroParis4.csv");
                List<Noeud<string>> noeuds = creernoeud(list1, list2);
                List<Lien<string>> liens = creerlien(list1, list2, noeuds);
                // afficher_liens(liens);
                // afficher_noeuds(noeuds);
                Noeud<string> depart = noeuds[0];
                Noeud<string> arrivee = noeuds[330];
                Dijkstra(noeuds, liens, depart, arrivee);
                Console.WriteLine("");
                BellmanFord(noeuds, liens, depart, arrivee);
                Console.WriteLine("");
                FloydWarshall(noeuds, liens, depart, arrivee);
                Console.WriteLine("");

                            }
        }
        /// <summary>
        /// Lecture d'un fichier et ajout de chaque ligne dans une liste
        /// </summary>
        /// <param name="filemname"></param>
        /// <returns></returns>
        static List<string> ReadFile(string filemname)
        {
            List<string> list1 = new List<string>();
            StreamReader sReader = new StreamReader(filemname);
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                list1.Add(line);
            }
            return list1;
        }

        /// <summary>
        /// Création de la liste de noeuds
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        static List<Noeud<string>> creernoeud(List<string> list1, List<string> list2)
        {
            List<Noeud<string>> noeud = new List<Noeud<string>>();
            string chgn = null;
            foreach (string line in list2)
            {
                string[] noeuds = line.Split(';');
                Noeud<string> noeud1 = new Noeud<string>(noeuds[0], noeuds[2], noeuds[1], noeuds[3], noeuds[4], noeuds[6]);
                noeud.Add(noeud1);
            }
            return noeud;
        }

        /// <summary>
        /// Création de la liste de liens
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="noeuds"></param>
        /// <returns></returns>
        static List<Lien<string>> creerlien(List<string> list1, List<string> list2, List<Noeud<string>> noeuds)
        {
            List<Lien<string>> liens = new List<Lien<string>>();
            string temps = null;
            int i = 0;
            bool ok1 = false;
            bool ok2 = false;
            while (i < 15)
            {
                for (int j = 1; j < noeuds.Count; j++)
                {
                    ok1 = false;
                    if (Convert.ToString(i) == noeuds[j].ligne)
                    {
                        foreach (string line2 in list1)
                        {
                            string[] noeuds2 = line2.Split(';');
                            if (noeuds2[0] == noeuds[j].id)
                            {
                                temps = noeuds2[4];
                                if (noeuds2[2] == noeuds[j - 1].id)
                                {
                                    ok1 = true;
                                    break;
                                }
                            }
                        }
                        if (ok1)
                        {
                            Lien<string> lien = new Lien<string>(noeuds[j - 1], noeuds[j], temps, noeuds[j - 1].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                        else if (ok1 = false)
                        {
                            Lien<string> lien = new Lien<string>(noeuds[j - 1], noeuds[j], " invalide ", noeuds[j - 1].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                    }
                }
                i++;
            }
            i = 0;
            while (i < 15)
            {
                for (int j = 0; j < noeuds.Count - 1; j++)
                {
                    ok2 = false;
                    if (Convert.ToString(i) == noeuds[j].ligne)
                    {
                        foreach (string line2 in list1)
                        {
                            string[] noeuds2 = line2.Split(';');
                            if (noeuds2[0] == noeuds[j].id)
                            {
                                temps = noeuds2[4];
                                if (noeuds2[3] == noeuds[j + 1].id)
                                {
                                    ok2 = true;
                                    break;
                                }
                            }
                        }
                        if (ok2)
                        {
                            Lien<string> lien = new Lien<string>(noeuds[j + 1], noeuds[j], temps, noeuds[j].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                        else if (ok2 = false)
                        {
                            Lien<string> lien = new Lien<string>(noeuds[j + 1], noeuds[j], " invalide ", noeuds[j].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                    }
                }
                i++;
            }

            int a = noeuds.Count();
            for (int k = 0; k < a; k++)
            {
                for (int j = 1; j < a; j++)
                {
                    if (noeuds[k].name == noeuds[j].name && noeuds[k].ligne != noeuds[j].ligne && noeuds[k].id != noeuds[j].id)
                    {
                        foreach (string line2 in list1)
                        {
                            string[] noeuds2 = line2.Split(';');
                            if (noeuds2[0] == noeuds[j].id)
                            {
                                temps = noeuds2[5];
                            }
                        }
                        Lien<string> lien = new Lien<string>(noeuds[k], noeuds[j], temps, noeuds[k].ligne, noeuds[j].ligne);
                        liens.Add(lien);
                    }
                }
            }
            return liens;
        }

        /// <summary>
        /// Affichage des liens
        /// </summary>
        /// <param name="liens"></param>
        static void afficher_liens(List<Lien<string>> liens)
        {
            foreach (var lien in liens)
            {
                if (lien.noeud1 != null && lien.noeud2 != null && lien.noeud1.name != lien.noeud2.name)
                {
                    Console.WriteLine("De " + lien.noeud1.name + " à " + lien.noeud2.name + " trajet de " + lien.temps + " minutes, distance haversine " + lien.distance_haversine);
                }
                else
                {
                    if (lien.temps == "")
                    {
                        Console.WriteLine(lien.noeud1.name + " changement de ?? minutes de M" + lien.ligne1 + " à M" + lien.ligne2 + " distance haversine " + lien.distance_haversine);
                    }
                    else
                    {
                        Console.WriteLine(lien.noeud1.name + " changement de " + lien.temps + " minutes de M" + lien.ligne1 + " à M" + lien.ligne2 + " distance haversine " + lien.distance_haversine);
                    }
                }
            }
        }

        /// <summary>
        /// Affichage des noeuds
        /// </summary>
        /// <param name="noeuds"></param>
        static void afficher_noeuds(List<Noeud<string>> noeuds)
        {
            foreach (var noeud in noeuds)
            {
                Console.WriteLine("ID " + noeud.id + " station " + noeud.name + " M" + noeud.ligne + " latitude " + noeud.latitude + " longitude " + noeud.longitude + " arrondissement " + noeud.arrondissemnt);
            }
        }

        /// <summary>
        /// Affichage du chemin le plus court
        /// </summary>
        /// <param name="chemin"></param>
        /// <param name="liens"></param>
        static void AfficherChemin(List<Noeud<string>> chemin, List<Lien<string>> liens)
        {
            double temps_total = 0;
            Console.WriteLine("Le chemin le plus court est : ");
            for (int i = 0; i < chemin.Count - 1; i++)
            {
                var noeud_courant = chemin[i];
                var noeud_suivant = chemin[i + 1];
                var lien = liens.FirstOrDefault(l => l.noeud1 == noeud_courant && l.noeud2 == noeud_suivant);
                if (lien != null)
                {
                    Console.WriteLine($"{noeud_courant.name} (M{noeud_courant.ligne}) -> {noeud_suivant.name} (M{noeud_suivant.ligne}) : {lien.temps} minutes");
                    if (double.TryParse(lien.temps, out double parsedTemps))
                    {
                        temps_total += parsedTemps;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid time value: {lien.temps} entre {lien.noeud1.name} et {lien.noeud2.name}");
                    }
                }
            }
            Console.WriteLine($"Le temps de trajet total est de : {temps_total} minutes");
        }
        
        /// <summary>
        /// Calculer le plus court chemin entre deux stations avec l'algorithme de Dijkstra
        /// </summary>
        /// <param name="noeuds"></param>
        /// <param name="liens"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        static void Dijkstra(List<Noeud<string>> noeuds, List<Lien<string>> liens, Noeud<string> depart, Noeud<string> arrivee)
        {
            List<Noeud<string>> noeuds_non_visites = new List<Noeud<string>>();
            List<Noeud<string>> noeuds_visites = new List<Noeud<string>>();
            Dictionary<Noeud<string>, Noeud<string>> predecesseurs = new Dictionary<Noeud<string>, Noeud<string>>();
            Dictionary<Noeud<string>, double> distances = new Dictionary<Noeud<string>, double>();
            Noeud<string> noeud_courant = new Noeud<string>("", "", "", "", "", "");
            double distance = 0;
            double temps_total = 0; // Variable pour stocker le temps de trajet total

            foreach (var noeud in noeuds)
            {
                if (noeud.id == depart.id)
                {
                    distances[noeud] = 0;
                }
                else
                {
                    distances[noeud] = double.MaxValue;
                }
                noeuds_non_visites.Add(noeud);
            }

            while (noeuds_non_visites.Count != 0)
            {
                noeud_courant = noeuds_non_visites.OrderBy(noeud => distances[noeud]).First();
                noeuds_visites.Add(noeud_courant);
                noeuds_non_visites.Remove(noeud_courant);
                foreach (var voisin in liens.Where(l => l.noeud1 == noeud_courant && !noeuds_visites.Contains(l.noeud2)).Select(l => l.noeud2))
                {
                    var lien = liens.First(l => l.noeud1 == noeud_courant && l.noeud2 == voisin);
                    distance = distances[noeud_courant] + lien.distance_haversine;
                    if (distance < distances[voisin])
                    {
                        distances[voisin] = distance;
                        predecesseurs[voisin] = noeud_courant;
                    }
                }
            }

            List<Noeud<string>> chemin = new List<Noeud<string>>();
            noeud_courant = arrivee;

            if (predecesseurs.ContainsKey(noeud_courant) || noeud_courant == depart)
            {
                while (noeud_courant != null)
                {
                    chemin.Add(noeud_courant);
                    if (predecesseurs.ContainsKey(noeud_courant))
                    {
                        noeud_courant = predecesseurs[noeud_courant];
                    }
                    else
                    {
                        break;
                    }
                }
                chemin.Reverse();
                Console.WriteLine("Dijkstra");
                AfficherChemin(chemin, liens);
            }
            else
            {
                Console.WriteLine("Il n'y a pas de chemin possible entre ces deux stations");
            }
        }

        /// <summary>
        /// Calculer le plus court chemin entre deux stations avec l'algorithme de Bellman-Ford
        /// </summary>
        /// <param name="noeuds"></param>
        /// <param name="liens"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        static void BellmanFord(List<Noeud<string>> noeuds, List<Lien<string>> liens, Noeud<string> depart, Noeud<string> arrivee)
        {
            Dictionary<Noeud<string>, Noeud<string>> predecesseurs = new Dictionary<Noeud<string>, Noeud<string>>();
            Dictionary<Noeud<string>, double> distances = new Dictionary<Noeud<string>, double>();
            Noeud<string> noeud_courant = new Noeud<string>("", "", "", "", "", "");
            double distance = 0;
            double temps_total = 0; // Variable pour stocker le temps de trajet total

            foreach (var noeud in noeuds)
            {
                if (noeud.id == depart.id)
                {
                    distances[noeud] = 0;
                }
                else
                {
                    distances[noeud] = double.MaxValue;
                }
            }

            for (int i = 0; i < noeuds.Count - 1; i++)
            {
                foreach (var lien in liens)
                {
                    if (distances[lien.noeud1] + lien.distance_haversine < distances[lien.noeud2])
                    {
                        distances[lien.noeud2] = distances[lien.noeud1] + lien.distance_haversine;
                        predecesseurs[lien.noeud2] = lien.noeud1;
                    }
                }
            }

            foreach (var lien in liens)
            {
                if (distances[lien.noeud1] + lien.distance_haversine < distances[lien.noeud2])
                {
                    Console.WriteLine("Il y a un cycle de poids négatif");
                    return;
                }
            }

            List<Noeud<string>> chemin = new List<Noeud<string>>();
            noeud_courant = arrivee;

            if (predecesseurs.ContainsKey(noeud_courant) || noeud_courant == depart)
            {
                while (noeud_courant != null)
                {
                    chemin.Add(noeud_courant);
                    if (predecesseurs.ContainsKey(noeud_courant))
                    {
                        noeud_courant = predecesseurs[noeud_courant];
                    }
                    else
                    {
                        break;
                    }
                }
                chemin.Reverse();
                Console.WriteLine("Bellman-Ford");
                AfficherChemin(chemin, liens);
            }
            else
            {
                Console.WriteLine("Il n'y a pas de chemin possible entre ces deux stations");
            }
        }

        /// <summary>
        /// Calculer le plus court chemin entre deux stations avec l'algorithme de Floyd-Warshall
        /// </summary>
        /// <param name="noeuds"></param>
        /// <param name="liens"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        static void FloydWarshall(List<Noeud<string>> noeuds, List<Lien<string>> liens, Noeud<string> depart, Noeud<string> arrivee)
        {
            Dictionary<Noeud<string>, Noeud<string>> predecesseurs = new Dictionary<Noeud<string>, Noeud<string>>();
            Dictionary<Noeud<string>, double> distances = new Dictionary<Noeud<string>, double>();
            Noeud<string> noeud_courant = new Noeud<string>("", "", "", "", "", "");
            double distance = 0;
            double temps_total = 0; // Variable pour stocker le temps de trajet total

            foreach (var noeud in noeuds)
            {
                if (noeud.id == depart.id)
                {
                    distances[noeud] = 0;
                }
                else
                {
                    distances[noeud] = double.MaxValue;
                }
            }

            for (int i = 0; i < noeuds.Count - 1; i++)
            {
                foreach (var lien in liens)
                {
                    if (distances[lien.noeud1] + lien.distance_haversine < distances[lien.noeud2])
                    {
                        distances[lien.noeud2] = distances[lien.noeud1] + lien.distance_haversine;
                        predecesseurs[lien.noeud2] = lien.noeud1;
                    }
                }
            }

            foreach (var lien in liens)
            {
                if (distances[lien.noeud1] + lien.distance_haversine < distances[lien.noeud2])
                {
                    Console.WriteLine("Il y a un cycle de poids négatif");
                    return;
                }
            }

            List<Noeud<string>> chemin = new List<Noeud<string>>();
            noeud_courant = arrivee;

            if (predecesseurs.ContainsKey(noeud_courant) || noeud_courant == depart)
            {
                while (noeud_courant != null)
                {
                    chemin.Add(noeud_courant);
                    if (predecesseurs.ContainsKey(noeud_courant))
                    {
                        noeud_courant = predecesseurs[noeud_courant];
                    }
                    else
                    {
                        break;
                    }
                }
                chemin.Reverse();
                Console.WriteLine("Floyd-Warshall");
                AfficherChemin(chemin, liens);
            }
            else
            {
                Console.WriteLine("Il n'y a pas de chemin possible entre ces deux stations");
            }
        }

        /// <summary>
        /// Création de la base de données et des tables
        /// </summary>
        /// <param name="connection"></param>
        static void CreateDatabaseAndTables(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                DROP DATABASE IF EXISTS projet;
                CREATE DATABASE projet;
                USE projet;
                CREATE TABLE Entreprise_locale(
                    ID_entreprise VARCHAR(50),
                    Nom_entreprise VARCHAR(50),
                    Nome_referent VARCHAR(50),
                    PRIMARY KEY(ID_entreprise)
                );
                CREATE TABLE Commande(
                    ID_commande VARCHAR(50),
                    Nom VARCHAR(50),
                    Prix INT,
                    Quantite INT,
                    PRIMARY KEY(ID_commande)
                );
                CREATE TABLE Plat(
                    ID_plat VARCHAR(50),
                    Plat VARCHAR(50),
                    Date_de_fabrication DATE,
                    Date_de_peremption DATE,
                    Regime VARCHAR(50),
                    Nature VARCHAR(50),
                    Photo VARCHAR(50),
                    ID_commande VARCHAR(50) NOT NULL,
                    PRIMARY KEY(ID_plat),
                    FOREIGN KEY(ID_commande) REFERENCES Commande(ID_commande)
                );
                CREATE TABLE Ingredient(
                    ID_ingredient VARCHAR(50),
                    Nom VARCHAR(50),
                    Volume INT,
                    PRIMARY KEY(ID_ingredient)
                );
                CREATE TABLE Cuisinier(
                    ID_cuisinier VARCHAR(50),
                    ID_commande VARCHAR(50) NOT NULL,
                    PRIMARY KEY(ID_cuisinier),
                    FOREIGN KEY(ID_commande) REFERENCES Commande(ID_commande)
                );
                CREATE TABLE Particulier(
                    ID_Particulier VARCHAR(50),
                    Prenom VARCHAR(50),
                    Nom VARCHAR(50),
                    Rue VARCHAR(50),
                    Numero_rue INT,
                    Ville VARCHAR(50),
                    Code_postal INT,
                    Telephone INT,
                    Email VARCHAR(50),
                    metro__plus_proche VARCHAR(50),
                    ID_cuisinier VARCHAR(50) NOT NULL,
                    PRIMARY KEY(ID_Particulier),
                    UNIQUE(ID_cuisinier),
                    FOREIGN KEY(ID_cuisinier) REFERENCES Cuisinier(ID_cuisinier)
                );
                CREATE TABLE Client(
                    ID_client VARCHAR(50),
                    ID_Particulier VARCHAR(50),
                    ID_entreprise VARCHAR(50),
                    PRIMARY KEY(ID_client),
                    UNIQUE(ID_Particulier),
                    UNIQUE(ID_entreprise),
                    FOREIGN KEY(ID_Particulier) REFERENCES Particulier(ID_Particulier),
                    FOREIGN KEY(ID_entreprise) REFERENCES Entreprise_locale(ID_entreprise)
                );
                CREATE TABLE Est_compose(
                    ID_plat VARCHAR(50),
                    ID_ingredient VARCHAR(50),
                    PRIMARY KEY(ID_plat, ID_ingredient),
                    FOREIGN KEY(ID_plat) REFERENCES Plat(ID_plat),
                    FOREIGN KEY(ID_ingredient) REFERENCES Ingredient(ID_ingredient)
                );
                CREATE TABLE Passe_commande(
                    ID_client VARCHAR(50),
                    ID_commande VARCHAR(50),
                    PRIMARY KEY(ID_client, ID_commande),
                    FOREIGN KEY(ID_client) REFERENCES Client(ID_client),
                    FOREIGN KEY(ID_commande) REFERENCES Commande(ID_commande)
                );
            ";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Insertion de données dans les tables
        /// </summary>
        /// <param name="connection"></param>
        static void InsertData(MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Commande (ID_commande, Nom, Prix, Quantite) VALUES ('1', 'Raclette', 10, 6), ('2', 'Salade de fruit', 5, 6);
                INSERT INTO Cuisinier (ID_cuisinier, ID_commande) VALUES ('1','1');
                INSERT INTO Particulier (ID_Particulier, Prenom, Nom, Rue, Numero_rue, Ville, Code_postal, Telephone, Email, metro__plus_proche, ID_cuisinier) VALUES ('1', 'Marie', 'Dupond', 'Rue de la République', 30, 'Paris', 75011, 1234567890, 'Mdupond@gmail.com', 'République', '1');
                INSERT INTO Client (ID_client, ID_Particulier, ID_entreprise) VALUES ('1', '1', NULL);
                INSERT INTO Plat (ID_plat, Plat, Date_de_fabrication, Date_de_peremption, Regime, Nature, Photo, ID_commande) VALUES ('1', 'Plat', '2025-01-10', '2025-01-15', '', 'Française', '', '1'), ('2', 'Dessert', '2025-01-10', '2025-01-15', 'Végétarien', 'Indifférent', '', '2');
                INSERT INTO Ingredient (ID_ingredient, Nom, Volume) VALUES ('1', 'raclette fromage', '250'), ('2', 'pommes_de_terre', '200'), ('3', 'jambon', '200'), ('4', 'cornichon', '3'), ('5', 'fraise', '100'), ('6', 'kiwi', '100'), ('7', 'sucre', '10');
                INSERT INTO Est_compose (ID_plat, ID_ingredient) VALUES ('1', '1'), ('1', '2'), ('1', '3'), ('1', '4'), ('2', '5'), ('2', '6'), ('2', '7');
                INSERT INTO Passe_commande (ID_client, ID_commande) VALUES ('1', '1'), ('1', '2');
            ";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Sélection de données dans les tables
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        static void SelectData(MySqlConnection connection, string query)
        {
            MySqlCommand selectCommand = connection.CreateCommand();
            selectCommand.CommandText = query;
            using (MySqlDataReader reader = selectCommand.ExecuteReader())
            {
                do
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i).ToString() + "\t");
                        }
                        Console.WriteLine();
                    }
                } while (reader.NextResult());
            }
        }

        
    }
}

/*
 * Algorithme de Dijkstra
•	Complexité en temps : O(V^2), où V est le nombre de nœuds. Si une file de priorité (comme un tas binaire) est utilisée, la complexité peut être réduite à O((V + E) log V), où E est le nombre de liens.
Algorithme de Bellman-Ford
•	Complexité en temps : O(V * E), où V est le nombre de nœuds et E est le nombre de liens. Cet algorithme est moins efficace que Dijkstra pour les graphes sans cycles de poids négatif.
Algorithme de Floyd-Warshall
•	Complexité en temps : O(V^3), où V est le nombre de nœuds. Cet algorithme est moins efficace pour les grands graphes, mais il est capable de trouver les plus courts chemins entre toutes les paires de nœuds.
*/