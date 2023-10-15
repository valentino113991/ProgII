using DepositoInd.dominio;
using DepositoInd.servicio;
using DepositoInd.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepositoInd.presentacion
{
    public partial class FrmNuevaOrden : Form
    {
        Orden nuevo = null;
        IServicio servicio = null;
        public FrmNuevaOrden(FabricaServicio f)
        {
            InitializeComponent();
            servicio = f.CrearServicio();
            nuevo = new Orden();
        }

        private void FrmNuevaOrden_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            numCantidad.Value = 1;
            txtResponsable.Text = "Consumidor final";
            CargarMateriales("SP_CONSULTAR_MATERIALES", cboMateriales);
        }

        private void CargarMateriales(string nombreSP, ComboBox cbo)
        {
            cboMateriales.DataSource = servicio.TraerMateriales();
            cboMateriales.ValueMember = "Codigo";
            cboMateriales.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboMateriales.SelectedIndex == -1)
            {
                MessageBox.Show("Elija un producto por favor", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("Ingrese una cantidad", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells["ColMaterial"].Value.ToString().Equals(cboMateriales.Text))
                {
                    MessageBox.Show("Este producto ya esta presupuestado..", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Material m = (Material)cboMateriales.SelectedItem;
            int cant = Convert.ToInt32(numCantidad.Value);
            DetalleOrden dO = new DetalleOrden(m, cant);
            nuevo.AgregarDetalle(dO);

            dgvDetalle.Rows.Add(new object[] { m.Codigo, m.Nombre, m.Stock, dO.Cantidad, "Quitar" });
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dgvDetalle.CurrentRow.Index);
                dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtResponsable.Text))
            {
                MessageBox.Show("Ingrese un responsable", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Debe presupuestar un material", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GrabarOrden();
        }

        private void GrabarOrden()
        {
            nuevo.Fecha = Convert.ToDateTime(dtpFecha.Value);
            nuevo.Responsable = txtResponsable.Text;

            if (servicio.Grabar(nuevo))
            {
                MessageBox.Show("Se grabo con exito la orden..",
                  "Control",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("No se pudo grabar su orden. Inténtelo más tarde...",
                "Control",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
