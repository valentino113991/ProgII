using DepositoInd.presentacion;
using DepositoInd.servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepositoInd
{
    public partial class Principal : Form
    {
        FabricaServicio f = null;
        public Principal(FabricaServicio f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void nuevaOrdenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevaOrden nuevaOrden = new FrmNuevaOrden(f);
            nuevaOrden.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }
    }
}
