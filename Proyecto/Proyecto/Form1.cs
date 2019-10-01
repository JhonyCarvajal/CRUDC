using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Form1 : Form
    {

        Conexion con = new Conexion();
        int Id;
        Boolean editar;

        public Form1()
        {
            InitializeComponent();
        }

       

        private void Button2_Click(object sender, EventArgs e)
        {
            if (editar)
            {
                con.Conectar();
                String consulta = "update people set  Cedula = '" + txtCedula.Text + "',  Nombre ='" + txtName.Text + "' , Apellido = '" + txtLastName.Text + "', Correo ='" + txtEmail.Text + "', Celular = '" + txtPhone.Text + "', Direccion = '" + txtAddress.Text + "'  where id = " + Id + ";";
                con.EjecSql(consulta);
                this.ActualizarGrid();
                LimpiarCeladas();
                con.Desconectar();
                editar = false;
            }
            else
            {
                con.Conectar();
                String consulta = "insert into people (Cedula, Nombre, Apellido, Correo, Celular, Direccion) values ('" + txtCedula.Text + "','" + txtName.Text + "' ,'" + txtLastName.Text + "', '" + txtEmail.Text + "', '" + txtPhone.Text + "', '" + txtAddress.Text + "')";
                con.EjecSql(consulta);
                this.ActualizarGrid();
                LimpiarCeladas();
                con.Desconectar();

            }

        }
        public void ActualizarGrid()
        {
            con.ActualizarGrid(this.data, "select * from people");
        }

        public void LimpiarCeladas()
        {
            txtCedula.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            editar = false;

            ActualizarGrid();
            
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            editar = true;

            Id = int.Parse(this.data.CurrentRow.Cells[0].Value.ToString());
            txtCedula.Text = this.data.CurrentRow.Cells[1].Value.ToString();
            txtName.Text = this.data.CurrentRow.Cells[2].Value.ToString();
            txtLastName.Text = this.data.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = this.data.CurrentRow.Cells[4].Value.ToString();
            txtPhone.Text = this.data.CurrentRow.Cells[5].Value.ToString();
            txtAddress.Text = this.data.CurrentRow.Cells[6].Value.ToString();

        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            con.ActualizarGrid(data, "select * from people where Nombre like '" + txtBuscar.Text + "%';");
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Id = int.Parse(this.data.CurrentRow.Cells[0].Value.ToString());
            var resultado = MessageBox.Show("¿Desea eliminar el resgistro?", "Confirmacion de eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(resultado == DialogResult.Yes)
            {
                con.Conectar();
                String consulta = "delete from people where id = '" + Id + "';";
                con.EjecSql(consulta);
                this.ActualizarGrid();
                //this.LimpiarCeladas();
                con.Desconectar();
                    
            }
        }
    }
}
