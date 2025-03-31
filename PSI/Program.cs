namespace PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lecture = new List<string>();
            lecture = ReadFile("MetroParisfinal.csv"); // lien du  doc
            List<string> lecture2 = new List<string>();
            lecture2 = ReadFile("MetroParis4.csv");
            List<Noeud<string>> noeuds=creernoeud(lecture, lecture2);
            creerlien(lecture, lecture2, noeuds);
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
            
            static List<Noeud<string>> creernoeud(List<string> lecture, List<string> lecture2)
            {
                List<Noeud<string>> noeud = new List<Noeud<string>>();
                string chgn=null;
                foreach (string line in lecture2)
                {
                    string[] noeuds = line.Split(';');
                    foreach (string line2 in lecture)
                    {
                        string[] noeuds2 = line2.Split(';');
                        if (noeuds2[0]== noeuds[0])
                        {
                            chgn = noeuds2[5];
                        }
                    }
                        Noeud<string> noeud1 = new Noeud<string>(noeuds[0], noeuds[2], noeuds[1], chgn);
                    noeud.Add(noeud1);
                    
                }
                return noeud;
            }
            
            static void creerlien(List<string> lecture, List<string> lecture2, List<Noeud<string>> noeuds)
            {
                List<Lien<string>> liens = new List<Lien<string>>();
                string temps = null;
                for (int i = 1; i < 15; i++)
                {
                    for (int j=1; j<noeuds.Count;j++){
                        if (Convert.ToString(i) == noeuds[j].ligne)
                        {
                            foreach (string line2 in lecture)
                            {
                                string[] noeuds2 = line2.Split(';');
                                if (noeuds2[0] == noeuds[j].id)
                                {
                                    temps = noeuds2[4];
                                }
                            }
                            Lien<string> lien = new Lien<string>(noeuds[j - 1], noeuds[j],  temps);
                            liens.Add(lien);
                        }
                    }
                }
                for (int i=0; i < lecture.Count; i++)
                {
                    for (int j=i+1; j < lecture.Count; j++)
                    {
                        if (lecture[i][1] == lecture[j][1])
                        {
                            foreach(Noeud<string> noeud in noeuds)
                            {
                                if (noeud.id == lecture[i][0])
                                {

                                }
                            }
                        }
                    }
                }

                foreach (var lien in liens)
                {
                    Console.WriteLine(lien.noeud1.name + lien.noeud2.name + lien.temps);
                }
            }
        }
}
    }
