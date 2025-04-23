using BibliotecaEjemplo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFormEjemplo
{
    public partial class Form1 : Form
    {
        // Lista para almacenar elementos 

        private List<string> productos = new List<string>();
        public Form1()
        {
            InitializeComponent();
            txtNombre.KeyPress += new KeyPressEventHandler(txtNombre_KeyPress);
            txtPrecio.KeyPress += new KeyPressEventHandler(txtPrecio_KeyPress);
            btnLimpiar.Click += new EventHandler(btnLimpiar_Click);
        }

        // Evento para verificar que en txtNombre solo se ingresen letras
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras (mayúsculas y minúsculas), la tecla de retroceso y espacios
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true; // Cancela la entrada del carácter
            }
        }

        // Evento para verificar que en txtPrecio solo se ingresen números y punto decimal
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal, coma decimal (solo uno) y tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Permitir un solo punto o coma decimal
                if ((e.KeyChar == '.' || e.KeyChar == ',') && !txtPrecio.Text.Contains(".") && !txtPrecio.Text.Contains(","))
                {
                    // Permitir el punto o la coma
                }
                else
                {
                    e.Handled = true; // Cancelar cualquier otro carácter
                }
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            producto.Nombre = txtNombre.Text;
            if (decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                producto.Precio = precio;
                lblResultado.Text = producto.ObtenerDescripcion();
            }
            else
            {
                MessageBox.Show("Ingresa un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Método para agregar elementos 

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nuevoElemento = txtNombre.Text;
            if (!string.IsNullOrWhiteSpace(nuevoElemento))
            {
                productos.Add(nuevoElemento);
                ActualizarLista();
                txtNombre.Clear();
            }
            else
            {
                MessageBox.Show("Por favor ingresa un elemento válido.");
            }
        }
        // Método para actualizar ListBox 

        private void ActualizarLista()
        {
            lstProductos.DataSource = null;
            lstProductos.DataSource = productos;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar campos de texto
            txtNombre.Clear();
            txtPrecio.Clear();
            lblResultado.Text = "";

            // Opcional: limpiar la lista de productos
            productos.Clear();
            ActualizarLista();
        }
    }
}
