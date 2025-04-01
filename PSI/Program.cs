namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list1 = new List<string>();
            list1 = ReadFile("MetroParisfinal.csv"); // lien du  doc
            List<string> list2 = new List<string>();
            list2 = ReadFile("MetroParis4.csv");
            List<Noeud<string>> noeuds = creernoeud(list1, list2);
            creerlien(list1, list2, noeuds);
            Console.ReadLine();

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
                    foreach (string line2 in list1)
                    {
                        string[] noeuds2 = line2.Split(';');
                        if (noeuds2[0] == noeuds[0])
                        {
                            chgn = noeuds2[5];
                        }
                    }
                    Noeud<string> noeud1 = new Noeud<string>(noeuds[0], noeuds[2], noeuds[1], chgn);
                    noeud.Add(noeud1);

                }
                return noeud;
            }

            static void creerlien(List<string> list1, List<string> list2, List<Noeud<string>> noeuds)
            {
                List<Lien<string>> liens = new List<Lien<string>>();
                string temps = null;
                for (int i = 1; i < 15; i++)
                {
                    for (int j = 1; j < noeuds.Count; j++)
                    {
                        if (noeuds[i].ligne == noeuds[j].ligne)
                        {
                            foreach (string line2 in list1)
                            {
                                string[] noeuds2 = line2.Split(';');
                                if (noeuds2[0] == noeuds[j].id)
                                {
                                    temps = noeuds2[4];
                                }
                            }
                            Lien<string> lien = new Lien<string>(noeuds[j - 1], noeuds[j], temps, noeuds[j-1].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                    }
                }
                int a = noeuds.Count();
                for (int i = 0; i < a; i++)
                {
                    for (int j = 1; j < a; j++)
                    {
                        if (noeuds[i].name == noeuds[j].name && noeuds[i].ligne != noeuds[j].ligne && noeuds[i].id != noeuds[j].id)
                        {
                            foreach (string line2 in list1)
                            {
                                string[] noeuds2 = line2.Split(';');
                                if (noeuds2[0] == noeuds[j].id)
                                {
                                    temps = noeuds2[5];
                                }
                            }
                            Lien<string> lien = new Lien<string>(noeuds[i], noeuds[j], temps, noeuds[i].ligne, noeuds[j].ligne);
                            liens.Add(lien);
                        }
                    }
                }



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
        }
    }
}