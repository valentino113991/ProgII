using CarpinteriaApp_1w3.Datos;
using FacturacionApp_1w3.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp_1w3.Presentacion
{
    public partial class FrmConsultarFacturas : Form
    {
        DBHelper gestor;
        public FrmConsultarFacturas()
        {
            InitializeComponent();
            gestor = new DBHelper();
        }

        private void ConsultarFacturas_Load(object sender, EventArgs e)
        {
            dtpFecDesde.Value = DateTime.Now.AddDays(-7);
            dtpFecHasta.Value = DateTime.Now;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            List<Parametro> lstParametro = new List<Parametro>();
            lstParametro.Add(new Parametro("@fecha_desde", dtpFecDesde.Value.ToString("yyyyMMdd")));
            lstParametro.Add(new Parametro("@fecha_hasta", dtpFecHasta.Value.ToString("yyyyMMdd")));
            lstParametro.Add(new Parametro("@cliente", txtCliente.Text));
            DataTable tabla = gestor.Consultar("SP_CONSULTAR_FACTURAS", lstParametro);
            dgvFacturas.Rows.Clear();
            foreach (DataRow fila in tabla.Rows)
            {
                dgvFacturas.Rows.Add(new object[]
                {
                    fila[0].ToString(),
                    ((DateTime)fila[1]).ToShortDateString(),
                    fila[2].ToString(),
                    fila[3].ToString()
                });
            }
        }

        private void dgvFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFacturas.CurrentCell.ColumnIndex == 4)
            {
                //int nro = int.Parse(dgvPresupuestos.CurrentRow.Cells["ColNro"].Value.ToString());
                int nro = Convert.ToInt32(dgvFacturas.CurrentRow.Cells["ColNro"].Value);
                FrmDetallesFactura detalle = new FrmDetallesFactura(nro); //llamada con constructor con parámetro.
                detalle.facturaNro = nro; //llamada con propiedad.   
                detalle.ShowDialog();
            }
        }
    }
}
