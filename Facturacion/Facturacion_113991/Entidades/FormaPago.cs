using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp_1w3.Entidades
{
    internal class FormaPago
    {
        public int NroFormaPago { get; set; }
        public string NombreFormaPago { get; set; }

        public FormaPago()
        {
            NombreFormaPago = string.Empty;
        }
        public FormaPago(string nombreFormaPago, int nroFormaPago)
        {
            NroFormaPago = nroFormaPago;
            NombreFormaPago = nombreFormaPago;
        }
        public override string ToString()
        {
            return NombreFormaPago;
        }
    }
}
