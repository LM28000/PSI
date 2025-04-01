using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public class Lien<T>
    {
        public Noeud<T> noeud1;
        public Noeud<T> noeud2;
        public T temps;
        public T ligne1;
        public T ligne2;
        public double distance_haversine;

        public Lien(Noeud<T> noeud1, Noeud<T> noeud2, T temps, T ligne1, T ligne2)
        {
            this.noeud1 = noeud1;
            this.noeud2 = noeud2;
            this.temps = temps;
            this.ligne1 = ligne1;
            this.ligne2 = ligne2;
            this.distance_haversine = DistanceHaversine(noeud1.latitude, noeud1.longitude, noeud2.latitude, noeud2.longitude);

        }
        public double DistanceHaversine(string lat1, string lon1, string lat2, string lon2)
        {
            double R = 6371; // rayon de la terre en km
            double dLat = (double.Parse(lat2) - double.Parse(lat1)) * Math.PI / 180;
            double dLon = (double.Parse(lon2) - double.Parse(lon1)) * Math.PI / 180;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(double.Parse(lat1) * Math.PI / 180) * Math.Cos(double.Parse(lat2) * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }

    }
}
