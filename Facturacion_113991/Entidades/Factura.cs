using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp_1w3.Entidades
{
    internal class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public FormaPago FormaPago { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }
        public void AgregarDetalle(DetalleFactura detalle)
        {
            Detalles.Add(detalle);
        }
        public void QuitarDetalle(int posicion)
        {
            Detalles.RemoveAt(posicion);
        }
        public double CalcularTotal()
        {
            double total = 0;
            // Con For -------------------------------------

            //for (int i = 0; i < Detalles.Count; i++)
            //{
            //    total += Detalles[i].CalcularSubtotal();
            //}

            // Con Foreach --------------------------------

            foreach (DetalleFactura d in Detalles)
            {
                total += d.CalcularSubtotal();
            }
            return total;
        }
    }
}
