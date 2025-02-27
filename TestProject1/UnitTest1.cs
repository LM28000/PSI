using PSI;
using Xunit;
using System.Collections.Generic;

namespace PSI.Tests
{
    public class GrapheTests
    {
        [Fact]
        public void TestOrdre()
        {
            // Arrange
            var liens = new Lien[]
            {
                new Lien(new Noeud(1), new Noeud(2)),
                new Lien(new Noeud(2), new Noeud(3))
            };
            var graphe = new Graphe(liens);
            graphe.Initialiser();

            // Act
            int ordre = graphe.Ordre();

            // Assert
            Assert.Equal(3, ordre);
        }

        [Fact]
        public void TestTaille()
        {
            // Arrange
            var liens = new Lien[]
            {
                new Lien(new Noeud(1), new Noeud(2)),
                new Lien(new Noeud(2), new Noeud(3))
            };
            var graphe = new Graphe(liens);

            // Act
            int taille = graphe.Taille();

            // Assert
            Assert.Equal(2, taille);
        }

        [Fact]
        public void TestEstConnexe()
        {
            // Arrange
            var liens = new Lien[]
            {
                new Lien(new Noeud(1), new Noeud(2)),
                new Lien(new Noeud(2), new Noeud(3))
            };
            var graphe = new Graphe(liens);
            graphe.Initialiser();

            // Act
            bool estConnexe = graphe.EstConnexe();

            // Assert
            Assert.True(estConnexe);
        }

        [Fact]
        public void TestContientCircuit()
        {
            // Arrange
            var liens = new Lien[]
            {
                new Lien(new Noeud(1), new Noeud(2)),
                new Lien(new Noeud(2), new Noeud(3)),
                new Lien(new Noeud(3), new Noeud(1))
            };
            var graphe = new Graphe(liens);
            graphe.Initialiser();

            // Act
            bool contientCircuit = graphe.ContientCircuit();

            // Assert
            Assert.True(contientCircuit);
        }

        [Fact]
        public void TestModeliserLeGrapheAvecSystemDrawing()
        {
            // Arrange
            var liens = new Lien[]
            {
                new Lien(new Noeud(1), new Noeud(2)),
                new Lien(new Noeud(2), new Noeud(3))
            };
            var graphe = new Graphe(liens);
            graphe.Initialiser();
            string filename = "testGraphe.png";

            // Act
            graphe.ModeliserLeGrapheAvecSystemDrawing(filename);

            // Assert
            Assert.True(System.IO.File.Exists(filename));
            System.IO.File.Delete(filename); // Nettoyage
        }
    }

}