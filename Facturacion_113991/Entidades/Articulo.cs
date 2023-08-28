using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp_1w3.Entidades
{
    internal class Articulo
    {
        public int NroArticulo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }

        public Articulo()
        {
            NroArticulo = 0;
            Nombre = string.Empty;
            Precio = 0;
        }
        public Articulo(int nroArticulo, string nombre, double precio)
        {
            NroArticulo = nroArticulo;
            Nombre = nombre;
            Precio = precio;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
