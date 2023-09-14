using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionApp_1w3.Presentacion
{
    public partial class FrmDetallesFactura : Form
    {
        public FrmDetallesFactura(int facturaNro)
        {
            InitializeComponent();
        }

        public int facturaNro { get; set; }
    }
}
