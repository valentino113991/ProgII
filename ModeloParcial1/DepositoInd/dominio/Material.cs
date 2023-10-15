using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepositoInd.dominio
{
    public class Material
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }

        public Material(int codMaterial, string nomMaterial, int stock)
        {
            this.Codigo = codMaterial;
            this.Nombre = nomMaterial;
            this.Stock = stock;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
