INSERT INTO Commande (ID_commande, Nom, Prix, Quantite)
VALUES ('1', 'Raclette', 10, 6),
       ('2', 'Salade de fruit', 5, 6);
INSERT INTO Cuisinier (ID_cuisinier, ID_commande)
VALUES ('1','1');
INSERT INTO Particulier (ID_Particulier, Prenom, Nom, Rue, Numero_rue, Ville, Code_postal, Telephone, Email, metro__plus_proche, ID_cuisinier)
VALUES ('1', 'Marie', 'Dupond', 'Rue de la République', 30, 'Paris', 75011, 1234567890, 'Mdupond@gmail.com', 'République', '1');
INSERT INTO Client (ID_client, ID_Particulier, ID_entreprise)
VALUES ('1', '1', NULL);
INSERT INTO Plat (ID_plat, Plat, Date_de_fabrication, Date_de_peremption, Regime, Nature, Photo, ID_commande)
VALUES ('1', 'Plat', '2025-01-10', '2025-01-15', '', 'Française', '', '1'),
       ('2', 'Dessert', '2025-01-10', '2025-01-15', 'Végétarien', 'Indifférent', '', '2');
INSERT INTO Ingredient (ID_ingredient, Nom, Volume)
VALUES ('1', 'raclette fromage', '250'),
       ('2', 'pommes_de_terre', '200'),
       ('3', 'jambon', '200'),
       ('4', 'cornichon', '3'),
       ('5', 'fraise', '100'),
       ('6', 'kiwi', '100'),
       ('7', 'sucre', '10');
INSERT INTO Est_compose (ID_plat, ID_ingredient)
VALUES ('1', '1'),
       ('1', '2'),
       ('1', '3'),
       ('1', '4'),
       ('2', '5'),
       ('2', '6'),
       ('2', '7');
INSERT INTO Passe_commande (ID_client, ID_commande)
VALUES ('1', '1'),
       ('1', '2');

