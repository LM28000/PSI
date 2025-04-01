namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list1 = new List<string>();
            list1 = ReadFile("MetroParisfinal.csv");
            List<string> list2 = new List<string>();
            list2 = ReadFile("MetroParis4.csv");
            List<Noeud<string>> noeuds = creernoeud(list1, list2);
            List<Lien<string>> liens =creerlien(list1, list2, noeuds);
            afficher_liens(liens);
            //afficher_noeuds(noeuds);

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
            static List<Noeud<string>> creernoeud(List<string> list1, List<string> list2)
            {
                List<Noeud<string>> noeud = new List<Noeud<string>>();
                string chgn = null;
                foreach (string line in list2)
                {
                    string[] noeuds = line.Split(';');
                    
                    Noeud<string> noeud1 = new Noeud<string>(noeuds[0], noeuds[2], noeuds[1]);
                    noeud.Add(noeud1);

                }
                return noeud;
            }
            static List<Lien<string>> creerlien(List<string> list1, List<string> list2, List<Noeud<string>> noeuds)
            {
                List<Lien<string>> liens = new List<Lien<string>>();
                string temps = null;
                int i = 0;
                bool ok1 = false;
                bool ok2 = false;
                while (i < 15)
                {
                    Console.WriteLine("1ok1");
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
                    Console.WriteLine("1ok2");
                    for (int j = 0; j < noeuds.Count-1; j++)
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
                                    if (noeuds2[3] == noeuds[j +1 ].id)
                                    {
                                        ok2 = true;
                                        break;
                                    }
                                }

                            }
                            if (ok2)
                            {
                                Console.WriteLine("ok2");
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
            static void afficher_liens(List<Lien<string>> liens)
            {
                foreach (var lien in liens)
                {
                    if (lien.noeud1 != null && lien.noeud2 != null && lien.noeud1.name != lien.noeud2.name)
                    {
                        Console.WriteLine("De " + lien.noeud1.name + " à " + lien.noeud2.name + " trajet de " + lien.temps + " minutes ");
                    }
                    else
                    {
                        if (lien.temps == "")
                        {
                            Console.WriteLine(lien.noeud1.name + " changement de ?? minutes de M" + lien.ligne1 + " à M" + lien.ligne2);
                        }
                        else
                        {
                            Console.WriteLine(lien.noeud1.name + " changement de " + lien.temps + " minutes de M" + lien.ligne1 + " à M" + lien.ligne2);
                        }
                    }
                }
            }
            static void afficher_noeuds(List<Noeud<string>> noeuds)
            {
                foreach (var noeud in noeuds)
                {
                    Console.WriteLine("ID "+ noeud.id + " station " + noeud.name + " M" + noeud.ligne);
                }
            }

        }
    }
}