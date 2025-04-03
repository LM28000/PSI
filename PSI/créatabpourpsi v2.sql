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
   PRIMARY KEY(ID_ingredient)
);

CREATE TABLE Cuisinier(
   ID_cuisinier VARCHAR(50),
   ID_commande VARCHAR(50) NOT NULL,
   PRIMARY KEY(ID_cuisinier),
   FOREIGN KEY(ID_commande) REFERENCES Commande(ID_commande)
);

CREATE TABLE Client(
   ID_client VARCHAR(50),
   ID_Particulier varchar(50),
   ID_entreprise VARCHAR(50) ,
   PRIMARY KEY(ID_client),
   UNIQUE(ID_entreprise),
   FOREIGN KEY(ID_entreprise) REFERENCES Entreprise_locale(ID_entreprise)
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
   ID_client VARCHAR(50) NOT NULL,
   ID_cuisinier VARCHAR(50) NOT NULL,
   PRIMARY KEY(ID_Particulier),
   UNIQUE(ID_client),
   UNIQUE(ID_cuisinier),
   FOREIGN KEY(ID_client) REFERENCES Client(ID_client),
   FOREIGN KEY(ID_cuisinier) REFERENCES Cuisinier(ID_cuisinier)
);

CREATE TABLE Est_compose(
   ID_plat VARCHAR(50),
   ID_ingredient VARCHAR(50),
   Quantité DECIMAL(15,2),
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