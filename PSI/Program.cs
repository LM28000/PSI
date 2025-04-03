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
                SelectData(connection, "SELECT * FROM Commande;");
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
                AStar(noeuds, liens, depart, arrivee);
                Console.WriteLine("");
                //Interface graphique de gestion de la base de donnée avec un menu
                Console.WriteLine("Bienvenue dans le programme de gestion de la base de données");
                Console.WriteLine("1. Afficher les noeuds");
                Console.WriteLine("2. Afficher les liens");
                Console.WriteLine("3. Afficher le chemin le plus court entre deux stations");
                Console.WriteLine("4. Afficher les stations d'une ligne");
                Console.WriteLine("5. Afficher les stations d'un arrondissement");
                Console.WriteLine("6. Afficher les stations d'une ligne dans un arrondissement");
                Console.WriteLine("7. Gestion de la base de données");
                Console.WriteLine("8. Quitter le programme");
                Console.WriteLine("Veuillez entrer votre choix : ");
                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        afficher_noeuds(noeuds);
                        break;
                    case "2":
                        afficher_liens(liens);
                        break;
                    case "3":
                        Console.WriteLine("Veuillez entrer le nom de la station de départ : ");
                        string depart_nom = Console.ReadLine();
                        Console.WriteLine("Veuillez entrer le nom de la station d'arrivée : ");
                        string arrivee_nom = Console.ReadLine();
                        Noeud<string> depart_station = noeuds.FirstOrDefault(n => n.name == depart_nom);
                        Noeud<string> arrivee_station = noeuds.FirstOrDefault(n => n.name == arrivee_nom);
                        if (depart_station != null && arrivee_station != null)
                        {
                            Dijkstra(noeuds, liens, depart_station, arrivee_station);
                            BellmanFord(noeuds, liens, depart_station, arrivee_station);
                            FloydWarshall(noeuds, liens, depart_station, arrivee_station);
                            AStar(noeuds, liens, depart_station, arrivee_station);
                        }
                        else
                        {
                            Console.WriteLine("Station non trouvée");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Veuillez entrer le nom de la ligne : ");
                        string ligne = Console.ReadLine();
                        List<Noeud<string>> stations_ligne = noeuds.Where(n => n.ligne == ligne).ToList();
                        afficher_noeuds(stations_ligne);
                        break;
                    case "5":
                        Console.WriteLine("Veuillez entrer le nom de l'arrondissement : ");
                        string arrondissement = Console.ReadLine();
                        List<Noeud<string>> stations_arrondissement = noeuds.Where(n => n.arrondissemnt == arrondissement).ToList();
                        afficher_noeuds(stations_arrondissement);
                        break;
                    case "6":
                        Console.WriteLine("Veuillez entrer le nom de la ligne : ");
                        string ligne2 = Console.ReadLine();
                        Console.WriteLine("Veuillez entrer le nom de l'arrondissement : ");
                        string arrondissement2 = Console.ReadLine();
                        List<Noeud<string>> stations_ligne_arrondissement = noeuds.Where(n => n.ligne == ligne2 && n.arrondissemnt == arrondissement2).ToList();
                        afficher_noeuds(stations_ligne_arrondissement);
                        break;
                    case "7":
                        // Gestion de la base de données
                        Console.WriteLine("Gestion de la base de données");
                        Console.WriteLine("1. Gestion des clients");
                        Console.WriteLine("2. Gestion des commandes");
                        Console.WriteLine("3. Gestion des ingrédients");
                        Console.WriteLine("4. Gestion des plats");
                        Console.WriteLine("5. Gestion des plats composés");
                        Console.WriteLine("6. Gestion des commandes passées");
                        Console.WriteLine("7. Quitter la gestion de la base de données");
                        Console.WriteLine("Veuillez entrer votre choix : ");
                        string choix_gestion = Console.ReadLine();
                        switch (choix_gestion)
                        {
                            case "1":
                                //Gestion des clients
                                Console.WriteLine("Ajouter un client");
                                //Ajouter un client dans la base de données
                                Console.WriteLine("Veuillez entrer le nom du client : ");
                                string nom_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le prénom du client : ");
                                string prenom_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer l'adresse du client : ");
                                string adresse_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le numéro de téléphone du client : ");
                                string telephone_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer l'email du client : ");
                                string email_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le mot de passe du client : ");
                                string motdepasse_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le numéro de carte du client : ");
                                string carte_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le numéro de sécurité sociale du client : ");
                                string secu_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le numéro de carte de fidélité du client : ");
                                string fidelite_client = Console.ReadLine();
                                Console.WriteLine("Veuillez entrer le numéro de carte de crédit du client : ");
                                if (connection.State != System.Data.ConnectionState.Open)
                                {
                                    connection.Open();
                                }

                                MySqlCommand command = new MySqlCommand("INSERT INTO Client (nom, prenom, adresse, telephone, email, motdepasse, carte, secu, fidelite, credit) VALUES (@nom, @prenom, @adresse, @telephone, @email, @motdepasse, @carte, @secu, @fidelite, @credit)", connection);
                                command.Parameters.AddWithValue("@nom", nom_client);
                                command.Parameters.AddWithValue("@prenom", prenom_client);
                                command.Parameters.AddWithValue("@adresse", adresse_client);
                                command.Parameters.AddWithValue("@telephone", telephone_client);
                                command.Parameters.AddWithValue("@email", email_client);
                                command.Parameters.AddWithValue("@motdepasse", motdepasse_client);
                                command.Parameters.AddWithValue("@carte", carte_client);
                                command.Parameters.AddWithValue("@secu", secu_client);
                                command.Parameters.AddWithValue("@fidelite", fidelite_client);
                                command.Parameters.AddWithValue("@credit", carte_client);
                                command.ExecuteNonQuery();

                                Console.WriteLine("Client ajouté");
                                Console.ReadLine();

                                break;
                            case "2":
                                Console.WriteLine("Gestion des commandes");
                                break;
                            case "3":
                                Console.WriteLine("Gestion des ingrédients");
                                break;
                            case "4":
                                Console.WriteLine("Gestion des plats");
                                break;
                            case "5":
                                Console.WriteLine("Gestion des plats composés");
                                break;
                            case "6":
                                Console.WriteLine("Gestion des commandes passées");
                                break;
                            case "7":
                                Console.WriteLine("Quitter la gestion de la base de données");
                                break;
                            default:
                                Console.WriteLine("Choix invalide");
                                break;
                        }
                        break;

                        Console.WriteLine("Fin du programme");
                        Console.ReadLine();


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
            /// Calculer le plus court chemin entre deux stations avec l'algorithme A*
            /// </summary>
            /// <param name="noeuds"></param>
            /// <param name="liens"></param>
            /// <param name="depart"></param>
            /// <param name="arrivee"></param>
            static void AStar(List<Noeud<string>> noeuds, List<Lien<string>> liens, Noeud<string> depart, Noeud<string> arrivee)
            {
                Dictionary<Noeud<string>, Noeud<string>> predecesseurs = new Dictionary<Noeud<string>, Noeud<string>>();
                Dictionary<Noeud<string>, double> gScores = new Dictionary<Noeud<string>, double>();
                Dictionary<Noeud<string>, double> fScores = new Dictionary<Noeud<string>, double>();
                HashSet<Noeud<string>> openSet = new HashSet<Noeud<string>>();
                HashSet<Noeud<string>> closedSet = new HashSet<Noeud<string>>();

                foreach (var noeud in noeuds)
                {
                    gScores[noeud] = double.MaxValue;
                    fScores[noeud] = double.MaxValue;
                }

                gScores[depart] = 0;
                fScores[depart] = Heuristic(depart, arrivee);
                openSet.Add(depart);

                while (openSet.Count > 0)
                {
                    var noeud_courant = openSet.OrderBy(n => fScores[n]).First();

                    if (noeud_courant.Equals(arrivee))
                    {
                        ReconstructPath(predecesseurs, noeud_courant, liens);
                        return;
                    }

                    openSet.Remove(noeud_courant);
                    closedSet.Add(noeud_courant);

                    foreach (var voisin in liens.Where(l => l.noeud1.Equals(noeud_courant)).Select(l => l.noeud2))
                    {
                        if (closedSet.Contains(voisin))
                            continue;

                        double tentative_gScore = gScores[noeud_courant] + liens.First(l => l.noeud1.Equals(noeud_courant) && l.noeud2.Equals(voisin)).distance_haversine;

                        if (!openSet.Contains(voisin))
                            openSet.Add(voisin);
                        else if (tentative_gScore >= gScores[voisin])
                            continue;

                        predecesseurs[voisin] = noeud_courant;
                        gScores[voisin] = tentative_gScore;
                        fScores[voisin] = gScores[voisin] + Heuristic(voisin, arrivee);
                    }
                }

                Console.WriteLine("Il n'y a pas de chemin possible entre ces deux stations");
            }

            /// <summary>
            /// Calculer la distance haversine entre deux noeuds
            /// </summary>
            /// <param name="noeud"></param>
            /// <param name="arrivee"></param>
            /// <returns></returns>
            static double Heuristic(Noeud<string> noeud, Noeud<string> arrivee)
            {
                // Utiliser la distance haversine comme heuristique
                Lien<string> lien = new Lien<string>(noeud, arrivee, "", noeud.ligne, arrivee.ligne);
                return lien.DistanceHaversine(noeud.latitude, noeud.longitude, arrivee.latitude, arrivee.longitude);
            }

            /// <summary>
            /// Reconstruire le chemin à partir des prédecesseurs
            /// </summary>
            /// <param name="predecesseurs"></param>
            /// <param name="noeud_courant"></param>
            /// <param name="liens"></param>
            static void ReconstructPath(Dictionary<Noeud<string>, Noeud<string>> predecesseurs, Noeud<string> noeud_courant, List<Lien<string>> liens)
            {
                List<Noeud<string>> chemin = new List<Noeud<string>>();
                double temps_total = 0;

                while (predecesseurs.ContainsKey(noeud_courant))
                {
                    chemin.Add(noeud_courant);
                    noeud_courant = predecesseurs[noeud_courant];
                }
                chemin.Add(noeud_courant);
                chemin.Reverse();

                Console.WriteLine("A*");
                AfficherChemin(chemin, liens);
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

            //addClient
            static void addClient(MySqlConnection connection, string id_client, string id_particulier, string id_entreprise)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Client (ID_client, ID_Particulier, ID_entreprise) VALUES (@id_client, @id_particulier, @id_entreprise)";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.Parameters.AddWithValue("@id_particulier", id_particulier);
                command.Parameters.AddWithValue("@id_entreprise", id_entreprise);
                command.ExecuteNonQuery();

            }
            //rmClient
            static void rmClient(MySqlConnection connection, string id_client)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Client WHERE ID_client = @id_client";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.ExecuteNonQuery();
            }
            //modifieClient
            static void modifieClient(MySqlConnection connection, string id_client, string id_particulier, string id_entreprise)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Client SET ID_Particulier = @id_particulier, ID_entreprise = @id_entreprise WHERE ID_client = @id_client";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.Parameters.AddWithValue("@id_particulier", id_particulier);
                command.Parameters.AddWithValue("@id_entreprise", id_entreprise);
                command.ExecuteNonQuery();
            }
            //afficherClient
            static void afficherClient(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Client";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_client: " + reader["ID_client"] + ", ID_Particulier: " + reader["ID_Particulier"] + ", ID_entreprise: " + reader["ID_entreprise"]);
                    }
                }
            }
            //addCuisinier
            static void addCuisinier(MySqlConnection connection, string id_cuisinier, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Cuisinier (ID_cuisinier, ID_commande) VALUES (@id_cuisinier, @id_commande)";
                command.Parameters.AddWithValue("@id_cuisinier", id_cuisinier);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //rmCuisinier
            static void rmCuisinier(MySqlConnection connection, string id_cuisinier)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Cuisinier WHERE ID_cuisinier = @id_cuisinier";
                command.Parameters.AddWithValue("@id_cuisinier", id_cuisinier);
                command.ExecuteNonQuery();
            }
            //modifieCuisinier
            static void modifieCuisinier(MySqlConnection connection, string id_cuisinier, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Cuisinier SET ID_commande = @id_commande WHERE ID_cuisinier = @id_cuisinier";
                command.Parameters.AddWithValue("@id_cuisinier", id_cuisinier);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //afficherCuisinier
            static void afficherCuisinier(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Cuisinier";
                using (MySqlDataReader reader = command.ExecuteReader())

                    while (reader.Read())
                    {
                        Console.WriteLine("ID_cuisinier: " + reader["ID_cuisinier"] + ", ID_commande: " + reader["ID_commande"]);
                    }

            }

            //addCommande
            static void addCommande(MySqlConnection connection, string id_commande, string nom, int prix, int quantite)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Commande (ID_commande, Nom, Prix, Quantite) VALUES (@id_commande, @nom, @prix, @quantite)";
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prix", prix);
                command.Parameters.AddWithValue("@quantite", quantite);
                command.ExecuteNonQuery();
            }
            //rmCommande
            static void rmCommande(MySqlConnection connection, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Commande WHERE ID_commande = @id_commande";
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //modifieCommande
            static void modifieCommande(MySqlConnection connection, string id_commande, string nom, int prix, int quantite)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Commande SET Nom = @nom, Prix = @prix, Quantite = @quantite WHERE ID_commande = @id_commande";
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@prix", prix);
                command.Parameters.AddWithValue("@quantite", quantite);
                command.ExecuteNonQuery();
            }
            //afficherCommande
            static void afficherCommande(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Commande";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_commande: " + reader["ID_commande"] + ", Nom: " + reader["Nom"] + ", Prix: " + reader["Prix"] + ", Quantite: " + reader["Quantite"]);
                    }
                }


            }
            //addPlat

            static void addPlat(MySqlConnection connection, string id_plat, string plat, DateTime date_de_fabrication, DateTime date_de_peremption, string regime, string nature, string photo, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Plat (ID_plat, Plat, Date_de_fabrication, Date_de_peremption, Regime, Nature, Photo, ID_commande) VALUES (@id_plat, @plat, @date_de_fabrication, @date_de_peremption, @regime, @nature, @photo, @id_commande)";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.Parameters.AddWithValue("@plat", plat);
                command.Parameters.AddWithValue("@date_de_fabrication", date_de_fabrication);
                command.Parameters.AddWithValue("@date_de_peremption", date_de_peremption);
                command.Parameters.AddWithValue("@regime", regime);
                command.Parameters.AddWithValue("@nature", nature);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //rmPlat
            static void rmPlat(MySqlConnection connection, string id_plat)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Plat WHERE ID_plat = @id_plat";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.ExecuteNonQuery();
            }
            //modifiePlat
            static void modifiePlat(MySqlConnection connection, string id_plat, string plat, DateTime date_de_fabrication, DateTime date_de_peremption, string regime, string nature, string photo, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Plat = @plat, Date_de_fabrication = @date_de_fabrication, Date_de_peremption = @date_de_peremption, Regime = @regime, Nature = @nature, Photo = @photo WHERE ID_plat = @id_plat";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.Parameters.AddWithValue("@plat", plat);
                command.Parameters.AddWithValue("@date_de_fabrication", date_de_fabrication);
                command.Parameters.AddWithValue("@date_de_peremption", date_de_peremption);
                command.Parameters.AddWithValue("@regime", regime);
                command.Parameters.AddWithValue("@nature", nature);
                command.Parameters.AddWithValue("@photo", photo);
                command.ExecuteNonQuery();
            }
            //afficherPlat
            static void afficherPlat(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Plat";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_plat: " + reader["ID_plat"] + ", Plat: " + reader["Plat"] + ", Date_de_fabrication: " + reader["Date_de_fabrication"] + ", Date_de_peremption: " + reader["Date_de_peremption"] + ", Regime: " + reader["Regime"] + ", Nature: " + reader["Nature"] + ", Photo: " + reader["Photo"] + ", ID_commande: " + reader["ID_commande"]);
                    }
                }
            }
            //addIngredient
            static void addIngredient(MySqlConnection connection, string id_ingredient, string nom, int volume)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Ingredient (ID_ingredient, Nom, Volume) VALUES (@id_ingredient, @nom, @volume)";
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@volume", volume);
                command.ExecuteNonQuery();
            }
            //rmIngredient
            static void rmIngredient(MySqlConnection connection, string id_ingredient)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Ingredient WHERE ID_ingredient = @id_ingredient";
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.ExecuteNonQuery();
            }
            //modifieIngredient
            static void modifieIngredient(MySqlConnection connection, string id_ingredient, string nom, int volume)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Ingredient SET Nom = @nom, Volume = @volume WHERE ID_ingredient = @id_ingredient";
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@volume", volume);
                command.ExecuteNonQuery();
            }
            //afficherIngredient
            static void afficherIngredient(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Ingredient";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_ingredient: " + reader["ID_ingredient"] + ", Nom: " + reader["Nom"] + ", Volume: " + reader["Volume"]);
                    }
                }
            }
            //addEst_compose
            static void addEst_compose(MySqlConnection connection, string id_plat, string id_ingredient)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Est_compose (ID_plat, ID_ingredient) VALUES (@id_plat, @id_ingredient)";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.ExecuteNonQuery();
            }
            //rmEst_compose
            static void rmEst_compose(MySqlConnection connection, string id_plat, string id_ingredient)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Est_compose WHERE ID_plat = @id_plat AND ID_ingredient = @id_ingredient";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.ExecuteNonQuery();
            }
            //modifieEst_compose
            static void modifieEst_compose(MySqlConnection connection, string id_plat, string id_ingredient)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Est_compose SET ID_ingredient = @id_ingredient WHERE ID_plat = @id_plat";
                command.Parameters.AddWithValue("@id_plat", id_plat);
                command.Parameters.AddWithValue("@id_ingredient", id_ingredient);
                command.ExecuteNonQuery();
            }
            //afficherEst_compose
            static void afficherEst_compose(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Est_compose";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_plat: " + reader["ID_plat"] + ", ID_ingredient: " + reader["ID_ingredient"]);
                    }
                }
            }
            //addPasse_commande
            static void addPasse_commande(MySqlConnection connection, string id_client, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Passe_commande (ID_client, ID_commande) VALUES (@id_client, @id_commande)";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //rmPasse_commande
            static void rmPasse_commande(MySqlConnection connection, string id_client, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Passe_commande WHERE ID_client = @id_client AND ID_commande = @id_commande";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //modifiePasse_commande
            static void modifiePasse_commande(MySqlConnection connection, string id_client, string id_commande)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Passe_commande SET ID_commande = @id_commande WHERE ID_client = @id_client";
                command.Parameters.AddWithValue("@id_client", id_client);
                command.Parameters.AddWithValue("@id_commande", id_commande);
                command.ExecuteNonQuery();
            }
            //afficherPasse_commande
            static void afficherPasse_commande(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Passe_commande";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_client: " + reader["ID_client"] + ", ID_commande: " + reader["ID_commande"]);
                    }
                }

            }
            //addEntrepris_locale
            static void addEntrepris_locale(MySqlConnection connection, string id_entreprise, string nom_entreprise, string nome_referent)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Entreprise_locale (ID_entreprise, Nom_entreprise, Nome_referent) VALUES (@id_entreprise, @nom_entreprise, @nome_referent)";
                command.Parameters.AddWithValue("@id_entreprise", id_entreprise);
                command.Parameters.AddWithValue("@nom_entreprise", nom_entreprise);
                command.Parameters.AddWithValue("@nome_referent", nome_referent);
                command.ExecuteNonQuery();
            }
            //rmEntrepris_locale
            static void rmEntrepris_locale(MySqlConnection connection, string id_entreprise)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Entreprise_locale WHERE ID_entreprise = @id_entreprise";
                command.Parameters.AddWithValue("@id_entreprise", id_entreprise);
                command.ExecuteNonQuery();
            }
            //modifieEntrepris_locale
            static void modifieEntrepris_locale(MySqlConnection connection, string id_entreprise, string nom_entreprise, string nome_referent)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Entreprise_locale SET Nom_entreprise = @nom_entreprise, Nome_referent = @nome_referent WHERE ID_entreprise = @id_entreprise";
                command.Parameters.AddWithValue("@id_entreprise", id_entreprise);
                command.Parameters.AddWithValue("@nom_entreprise", nom_entreprise);
                command.Parameters.AddWithValue("@nome_referent", nome_referent);
                command.ExecuteNonQuery();
            }
            //afficherEntrepris_locale
            static void afficherEntrepris_locale(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Entreprise_locale";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_entreprise: " + reader["ID_entreprise"] + ", Nom_entreprise: " + reader["Nom_entreprise"] + ", Nome_referent: " + reader["Nome_referent"]);
                    }
                }

            }

            //Statistiques
            static void Statistiques(MySqlConnection connection)
            {
                MySqlCommand command = connection.CreateCommand();
                //Afficher par cuisinier le nombre de livraisons effectuées
                command.CommandText = "SELECT ID_cuisinier, COUNT(*) AS Nombre_livraisons FROM Passe_commande GROUP BY ID_cuisinier";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_cuisinier: " + reader["ID_cuisinier"] + ", Nombre_livraisons: " + reader["Nombre_livraisons"]);
                    }
                }
                //Afficher les commandes selon une période de temps 
                command.CommandText = "SELECT ID_commande, COUNT(*) AS Nombre_commandes FROM Commande WHERE Date_de_fabrication BETWEEN '2025-01-01' AND '2025-12-31' GROUP BY ID_commande";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_commande: " + reader["ID_commande"] + ", Nombre_commandes: " + reader["Nombre_commandes"]);
                    }
                }
                //Afficher la moyenne des prix des commandes 
                command.CommandText = "SELECT AVG(Prix) AS Prix_moyen FROM Commande";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Prix_moyen: " + reader["Prix_moyen"]);
                    }
                }
                //Afficher la moyenne des comptes clients 
                command.CommandText = "SELECT AVG(Quantite) AS Quantite_moyenne FROM Commande";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Quantite_moyenne: " + reader["Quantite_moyenne"]);
                    }
                }
                //Afficher la liste des commandes pour un client selon la nationalité des plats, la période 
                command.CommandText = "SELECT ID_client, COUNT(*) AS Nombre_commandes FROM Passe_commande WHERE ID_client = '1' AND Date_de_fabrication BETWEEN '2025-01-01' AND '2025-12-31' GROUP BY ID_client";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID_client: " + reader["ID_client"] + ", Nombre_commandes: " + reader["Nombre_commandes"]);
                    }
                }
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