using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    internal class Lien
    {
        public Noeud noeud1;
        public Noeud noeud2;
        public Lien(Noeud noeud1,Noeud noeud2)
        {
            this.noeud2 = noeud2;
            this.noeud1 = noeud1;
        }


    }
}
