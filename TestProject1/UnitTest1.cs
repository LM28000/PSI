using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using PSI;
using System.Collections.Generic;
using System.IO;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private MySqlConnection connection;

        [TestInitialize]
        public void TestInitialize()
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Uid=root;Pwd=louis;");
            connection.Open();
            Program.CreateDatabaseAndTables(connection);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void TestCreateDatabaseAndTables()
        {
            // Test if the database and tables are created successfully
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SHOW TABLES IN projet;";
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<string> tables = new List<string>();
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }

                Assert.IsTrue(tables.Contains("Entreprise_locale"));
                Assert.IsTrue(tables.Contains("Commande"));
                Assert.IsTrue(tables.Contains("Plat"));
                Assert.IsTrue(tables.Contains("Ingredient"));
                Assert.IsTrue(tables.Contains("Cuisinier"));
                Assert.IsTrue(tables.Contains("Particulier"));
                Assert.IsTrue(tables.Contains("Client"));
                Assert.IsTrue(tables.Contains("Est_compose"));
                Assert.IsTrue(tables.Contains("Passe_commande"));
            }
        }

        [TestMethod]
        public void TestInsertData()
        {
            // Test if the data is inserted successfully
            Program.InsertData(connection);

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Commande;";
            int count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(2, count);

            command.CommandText = "SELECT COUNT(*) FROM Cuisinier;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(1, count);

            command.CommandText = "SELECT COUNT(*) FROM Particulier;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(1, count);

            command.CommandText = "SELECT COUNT(*) FROM Client;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(1, count);

            command.CommandText = "SELECT COUNT(*) FROM Plat;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(2, count);

            command.CommandText = "SELECT COUNT(*) FROM Ingredient;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(7, count);

            command.CommandText = "SELECT COUNT(*) FROM Est_compose;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(7, count);

            command.CommandText = "SELECT COUNT(*) FROM Passe_commande;";
            count = (int)(long)command.ExecuteScalar();
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void TestSelectData()
        {
            // Test if the data is selected successfully
            Program.InsertData(connection);

            using (StringWriter sw = new StringWriter())
            {
                System.Console.SetOut(sw);
                Program.SelectData(connection, "SELECT * FROM Client;");
                string result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains("1\t1\t"));
            }
        }

        [TestMethod]
        public void TestReadFile()
        {
            // Test if the file is read successfully
            List<string> lines = Program.ReadFile("MetroParisfinal.csv");
            Assert.IsTrue(lines.Count > 0);
        }

        [TestMethod]
        public void TestCreerNoeud()
        {
            // Test if the nodes are created successfully
            List<string> list1 = Program.ReadFile("MetroParisfinal.csv");
            List<string> list2 = Program.ReadFile("MetroParis4.csv");
            List<Noeud<string>> noeuds = Program.creernoeud(list1, list2);
            Assert.IsTrue(noeuds.Count > 0);
        }

        [TestMethod]
        public void TestCreerLien()
        {
            // Test if the links are created successfully
            List<string> list1 = Program.ReadFile("MetroParisfinal.csv");
            List<string> list2 = Program.ReadFile("MetroParis4.csv");
            List<Noeud<string>> noeuds = Program.creernoeud(list1, list2);
            List<Lien<string>> liens = Program.creerlien(list1, list2, noeuds);
            Assert.IsTrue(liens.Count > 0);
        }
    }
}
