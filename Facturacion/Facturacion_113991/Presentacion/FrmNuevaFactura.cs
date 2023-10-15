using CarpinteriaApp_1w3.Datos;
using CarpinteriaApp_1w3.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp_1w3.Presentacion
{
    public partial class FrmNuevaFactura : Form
    {
        Factura factura;
        DBHelper gestor;
        public FrmNuevaFactura()
        {
            InitializeComponent();
            factura = new Factura();
            gestor = new DBHelper();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            lblFacturaId.Text += gestor.ProximaFactura();
            CargarArticulos();
            CargarFormasPago();
            txtCliente.Text = "Consumidor Final";
            txtCantidad.Text = "1";
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void CargarFormasPago()
        {
            DataTable tabla = gestor.Consultar("SP_CONSULTAR_FORMAS_PAGO");
            cboFormaPago.DataSource = tabla;
            cboFormaPago.ValueMember = "nro_forma_pago";
            cboFormaPago.DisplayMember = "nombre";
        }

        private void CargarArticulos()
        {
            DataTable tabla = gestor.Consultar("SP_CONSULTAR_ART");
            cboArticulo.DataSource = tabla;
            cboArticulo.ValueMember = "nro_articulo";
            cboArticulo.DisplayMember = "nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (cboArticulo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un artículo"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            if (cboFormaPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }
            if (txtCantidad.Text == "" || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Seleccione una cantidad"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColArticulo"].Value.ToString().Equals(cboArticulo.Text))
                {
                    MessageBox.Show("Artículo ya seleccionado"
                    , "Control"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView) cboArticulo.SelectedItem;
            int nro_articulo = Convert.ToInt32(item.Row.ItemArray[0]);
            string n_articulo = Convert.ToString(item.Row.ItemArray[1]);
            double precio = Convert.ToDouble(item.Row.ItemArray[2]);
            Articulo a = new Articulo(nro_articulo, n_articulo, precio);

            int cantidad = Convert.ToInt32(txtCantidad.Text);
            DetalleFactura dF = new DetalleFactura(a, cantidad);

            factura.AgregarDetalle(dF);
            dgvDetalles.Rows.Add(new Object[] { nro_articulo, n_articulo,precio, cantidad, "Quitar" });

            calcularTotales();
        }

        private void calcularTotales()
        {
            double total = factura.CalcularTotal();
            txtTotal.Text = total.ToString();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                factura.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                calcularTotales();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe ingresar un cliente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvDetalles.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un detalle...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            GrabarPresupuesto();
        }

        private void GrabarPresupuesto()
        {
            factura.Fecha = Convert.ToDateTime(txtFecha.Text);
            factura.Cliente = txtCliente.Text;

            DataRowView itemFormaPago = (DataRowView)cboFormaPago.SelectedItem;
            int nro_forma_pago = Convert.ToInt32(itemFormaPago.Row.ItemArray[0]);
            string nombre_forma_pago = Convert.ToString(itemFormaPago.Row.ItemArray[1]);
            FormaPago fp = new FormaPago(nombre_forma_pago, nro_forma_pago);

            factura.FormaPago = fp;
            if (gestor.ConfirmarFactura(factura))
            {
                MessageBox.Show("Se registró con éxito la factura...", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("NO se pudo registrar la factura...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    } 
}
