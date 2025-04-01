using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public class Noeud<T>
    {
        public T id;
        public string name;
        public T ligne;
        public string latitude;
        public string longitude;
        public T arrondissemnt;
        public Noeud(T id, string name, T ligne, string latitude, string longitude, T arrondissemnt)
        {
            this.id = id;
            this.name = name;
            this.ligne = ligne;
            this.latitude = latitude;
            this.longitude = longitude;
            this.arrondissemnt = arrondissemnt;
            
        }
    }
}
