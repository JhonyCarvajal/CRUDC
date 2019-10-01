using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Proyecto
{
    class Conexion
    {
        SqlConnection conn;

        public void Conectar()
        {
            conn =  new SqlConnection("Data Source=DESKTOP-R3ID9P7\\SQLEXPRESS;Initial Catalog=prueba2;Integrated Security=True");
            conn.Open();

        }
        public void Desconectar()
        {
            conn.Close();

        }

        public void EjecSql(String consulta)
        {
            SqlCommand com = new SqlCommand(consulta, conn);

            int filasAfectadas = com.ExecuteNonQuery();

            if (filasAfectadas > 0)
            {
                MessageBox.Show("Operacion Realizada con exito", "La Base de datos ha sido modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No se ha conecatdo a la base de datos", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }   

        public void ActualizarGrid(DataGridView dg, String consulta)
        {
            this.Conectar();
            System.Data.DataSet dc = new System.Data.DataSet();
            SqlDataAdapter da = new SqlDataAdapter(consulta, conn);

            da.Fill(dc, "people");
            dg.DataSource = dc;
            dg.DataMember = "people";
            this.Desconectar();

        }
        

    }
}
