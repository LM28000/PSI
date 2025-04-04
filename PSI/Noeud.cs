public class Noeud<T>
{
    public string id { get; set; }
    public string name { get; set; }
    public string ligne { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string arrondissemnt { get; set; }

    public Noeud(string id, string name, string ligne, string latitude, string longitude, string arrondissemnt)
    {
        this.id = id;
        this.name = name;
        this.ligne = ligne;
        this.latitude = latitude;
        this.longitude = longitude;
        this.arrondissemnt = arrondissemnt;
    }
}