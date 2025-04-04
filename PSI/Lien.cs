using PSI;

public class Lien<T>
{
    public Noeud<T> noeud1 { get; set; }
    public Noeud<T> noeud2 { get; set; }
    public string temps { get; set; }
    public string ligne1 { get; set; }
    public string ligne2 { get; set; }
    public double distance_haversine { get; set; }

    public Lien(Noeud<T> noeud1, Noeud<T> noeud2, string temps, string ligne1, string ligne2)
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
        double R = 6371; // Rayon de la Terre en kilomètres
        double dLat = DegreeToRadian(double.Parse(lat2) - double.Parse(lat1));
        double dLon = DegreeToRadian(double.Parse(lon2) - double.Parse(lon1));
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(DegreeToRadian(double.Parse(lat1))) * Math.Cos(DegreeToRadian(double.Parse(lat2))) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = R * c;
        return distance;
    }

    private double DegreeToRadian(double degree)
    {
        return degree * Math.PI / 180;
    }
}