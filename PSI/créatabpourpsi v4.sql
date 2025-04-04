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
   Nom_pl Varchar(50),
   
   Date_de_fabrication DATE,
   Date_de_peremption DATE,
   Regime VARCHAR(50),
   Nature VARCHAR(50),
   Photo VARCHAR(50),
    ID_cuisinier INT,
   PRIMARY KEY(ID_plat),
   FOREIGN KEY(ID_cuisinier) REFERENCES Cuisinier(ID_cuisinier) ON DELETE CASCADE
);

CREATE TABLE Ingredient(
   ID_ingredient INT,
   Nom VARCHAR(50),
   PRIMARY KEY(ID_ingredient)
);

CREATE TABLE Cuisinier(
   ID_cuisinier INT,
   ID_Particulier INT ,
   PRIMARY KEY(ID_cuisinier),
   FOREIGN KEY(ID_Particulier) REFERENCES Particulier(ID_Particulier) ON DELETE CASCADE
);

CREATE TABLE Client(
   ID_client INT,
   ID_Particulier INT unique,
   ID_entreprise INT UNIQUE ,
   PRIMARY KEY(ID_client),
   FOREIGN KEY(ID_Particulier) REFERENCES Particulier(ID_Particulier) ON DELETE CASCADE,
   FOREIGN KEY(ID_entreprise) REFERENCES Entreprise_locale(ID_entreprise) ON DELETE CASCADE
);

CREATE TABLE Particulier(
   ID_Particulier INT,
   Prenom VARCHAR(50),
   Nom VARCHAR(50),
   Rue VARCHAR(50),
   Numero_rue INT,
   Ville VARCHAR(50),
   Code_postal INT,
   Telephone INT,
   Email VARCHAR(50),
   metro_plus_proche VARCHAR(50),
   PRIMARY KEY(ID_Particulier)
);

CREATE TABLE Est_compose(
   ID_plat INT ,
   ID_ingredient INT,
   Quantité INT,
   PRIMARY KEY(ID_plat, ID_ingredient),
   FOREIGN KEY(ID_plat) REFERENCES Plat(ID_plat),
   FOREIGN KEY(ID_ingredient) REFERENCES Ingredient(ID_ingredient)
);

CREATE TABLE Passe_commande(
   ID_client INT,
   ID_commande INT,
   PRIMARY KEY(ID_client, ID_commande),
   FOREIGN KEY(ID_client) REFERENCES Client(ID_client) ON DELETE CASCADE,
   FOREIGN KEY(ID_commande) REFERENCES Commande(ID_commande) ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS Avis(
    ID_avis INT PRIMARY KEY,
    ID_client INT,
    ID_plat INT,
    Note VARCHAR(50),
    FOREIGN KEY(ID_client) REFERENCES Client(ID_client) ON DELETE CASCADE,
    FOREIGN KEY(ID_plat) REFERENCES Plat(ID_plat) ON DELETE CASCADE
);