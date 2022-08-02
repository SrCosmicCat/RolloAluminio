using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RolloAluminio
{
    public partial class frmRollo : Form
    {
        String opMod = "", transaccion = "";
        int IdR1, IdP1;
        double Gro1, Male1, Res1, Temp1;
        string calidad;
        Conexion Con = new Conexion();
        Rollo Ro = new Rollo();
        String SQL = "";
        MySqlCommand Query;
        MySqlDataReader Registros;

        bool existe = false;
        int elimina = 0, modificaGro = 0, modificaMale = 0, modificaTemp = 0, modificaRes = 0, inserta = 0, modificaPru = 0;


        public frmRollo()
        {
            InitializeComponent();
            inhabilitarComponentes();
            limpiar();
            PruebaCalidad();
        }

        public int EliminarRollo()
        {
            if (Con.abrirBD())
            {
                //MessageBox.Show("BD conectada uwu");
                try
                {
                    SQL = "DELETE FROM Rollo WHERE id_ro = " + Ro.IdR;
                    Query = new MySqlCommand(SQL, Con.connection);
                    elimina = Query.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex);
                    elimina = 0;
                }
            }
            else
            {
                MessageBox.Show("BD no conectada unu");
            }
            Con.cerrarBD();
            return elimina;
        }

        public int ModificaGroRollo()
        {
            if (Con.abrirBD())
            {
                //MessageBox.Show("BD conectada uwu");
                try
                {
                    SQL = "UPDATE Rollo SET gro_ro = " + Ro.GroR1 + ", id_pru1 = " + Ro.IdPr1 + " WHERE id_ro = " + Ro.IdR; //ro.IdR1;
                    Query = new MySqlCommand(SQL, Con.connection);
                    modificaGro = Query.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex);
                    modificaGro = 0;
                }
            }
            else
            {
                MessageBox.Show("BD no conectada unu");
            }
            Con.cerrarBD();
            return modificaGro;
        }

        public void PruebaCalidad()
        {
            if (Con.abrirBD())
            {
                MessageBox.Show("BD conectada uwu");
                try
                {
                    SQL = "SELECT desc_pru FROM Prueba";
                    Query = new MySqlCommand(SQL, Con.connection);
                    Registros = Query.ExecuteReader();
                    while (Registros.Read())
                    {
                        calidad = Registros.GetString(0);
                        cmbCalidad.Items.Add(calidad);
                    }
                    Con.cerrarBD();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
            else
            {
                MessageBox.Show("BD no conectada unu");
            }
        }

        public bool BuscaRollo()
        {
            if (Con.abrirBD())
            {
                //MessageBox.Show("BD conectada uwu");
                try
                {
                    SQL = "SELECT * FROM Rollo WHERE id_ro = " + Ro.IdR;
                    Query = new MySqlCommand(SQL, Con.connection);
                    Registros = Query.ExecuteReader();
                    if (Registros.Read())
                    {
                        existe = true;
                        IdR1 = Registros.GetInt32(0);
                        Gro1 = Registros.GetDouble(1);
                        Male1 = Registros.GetDouble(2);
                        Res1 = Registros.GetDouble(3);
                        Temp1 = Registros.GetDouble(4);
                        IdP1 = Registros.GetInt32(5);

                        txtGro.Text = Gro1 + "";
                        txtMale.Text = Male1 + "";
                        txtRes.Text = Res1 + "";
                        txtTemp.Text = Temp1 + "";

                        //cmbCalidad.Text = IdP1 + "";
                        cmbCalidad.SelectedIndex = IdP1 - 1;
                    }
                    
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex);
                    existe = false;
                }
            }
            else
            {
                MessageBox.Show("BD no conectada unu");
            }
            Con.cerrarBD();
            return existe;
        }

        public void inhabilitarComponentes()
        {
            txtGro.Enabled = false;
            txtId.Enabled = false;
            txtRes.Enabled = false;
            txtMale.Enabled = false;
            txtTemp.Enabled = false;
            cmbCalidad.Enabled = false;
            cmbMod.Enabled = false;
            cmbMod.Visible = false;
            btnBuscar.Visible = false;
            btnAceptar.Visible = false;
        }

        public void limpiar()
        {
            txtGro.Clear();
            txtMale.Clear();
            txtRes.Clear();
            txtId.Clear();
            txtTemp.Clear();
            cmbCalidad.SelectedIndex = -1;
            cmbCalidad.Text = "Seleccion";
            cmbMod.SelectedIndex = -1;
            cmbMod.Text = "¿Que desea modificar?";
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (transaccion)
            {
                case "insertar": break;
                case "modificar":
                    switch (opMod)
                    {
                        case "Grosor":
                            if (txtGro.Text.Trim() != "")
                            {
                                Ro.GroR1 = Convert.ToDouble(txtGro.Text);
                                if (Ro.GroR1 >= 0.95 && Ro.GroR1 <= 1.05)
                                {
                                    Ro.IdPr1 = 1;
                                }
                                else if (Ro.GroR1 > 1.05)
                                {
                                    Ro.IdPr1 = 2;
                                }
                                else
                                {
                                    Ro.IdPr1 = 3;   
                                }

                                if (ModificaGroRollo() == 1)
                                {
                                    MessageBox.Show("Grosor modificado");
                                    limpiar();
                                    inhabilitarComponentes();
                                }
                                else
                                {
                                    MessageBox.Show("Grosor no modificado");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ingrese grosor del rollo");
                                txtGro.Focus();
                            }
                            break;
                        case "Maleabilidad": break;
                        case "Resistencia": break;
                        case "Temperatura": break;
                    }
                    break;
                case "eliminar":
                    if (EliminarRollo() == 1)
                    {
                        MessageBox.Show("Rollo eliminado");
                        limpiar();
                        inhabilitarComponentes();
                    }
                    else
                    {
                        MessageBox.Show("Registro no eliminado");
                    }
                    break;
                case "consultar": 
                    if (txtId.Text.Trim() != "")
                    {
                        Ro.IdR = Convert.ToInt32(txtId.Text);
                        if (BuscaRollo())
                        {
                            txtId.Enabled = false;

                        }
                        else
                        {
                            MessageBox.Show(null, "El rollo no existe", "Advertencia");
                            limpiar();
                            inhabilitarComponentes();
                        }
                    }
                    else
                    {
                        MessageBox.Show(null, "Ingrese Id del rollo", "Advertencia");
                        txtId.Focus();
                    }
                    break;
            }
            //BuscaRollo();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transaccion = "insertar";
            inhabilitarComponentes();
            limpiar();
            txtGro.Enabled = true;
            txtMale.Enabled = true;
            txtRes.Enabled = true;
            txtTemp.Enabled = true;
            btnAceptar.Visible = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                Ro.IdR = Convert.ToInt32(txtId.Text);
                if (BuscaRollo())
                {
                    txtId.Enabled = false;
                    btnBuscar.Visible = false;
                    btnAceptar.Visible = true;

                    if (transaccion == "modificar")
                    {
                        cmbMod.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show(null,"El rollo no existe", "Advertencia");
                    limpiar();
                    inhabilitarComponentes();
                }
            }
            else
            {
                MessageBox.Show("ingrese el id del rollo");
                txtId.Focus();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transaccion = "modificar";
            inhabilitarComponentes();
            limpiar();
            txtId.Enabled = true;
            btnBuscar.Visible = true;
            cmbMod.Visible = true;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transaccion = "eliminar";
            inhabilitarComponentes();
            limpiar();
            txtId.Enabled = true;
            btnBuscar.Visible = true;
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transaccion = "consultar";
            inhabilitarComponentes();
            limpiar();
            txtId.Enabled = true;
            btnAceptar.Visible = true;
        }

        private void cmbMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            btnBuscar.Enabled = false;
            opMod = cmbMod.SelectedItem as string;
            switch (opMod)
            {
                case "Grosor":
                    txtGro.Enabled = true; 
                    txtMale.Enabled = false; 
                    txtRes.Enabled = false;
                    txtTemp.Enabled = false;

                    txtMale.Clear();
                    txtRes.Clear();
                    txtTemp.Clear();

                    txtGro.Focus();
                    break;
                case "Maleabilidad":
                    txtGro.Enabled = false;
                    txtMale.Enabled = true;
                    txtRes.Enabled = false;
                    txtTemp.Enabled = false;

                    txtGro.Clear();
                    txtRes.Clear();
                    txtTemp.Clear();

                    txtMale.Focus();
                    break;
                case "Resistencia":
                    txtGro.Enabled = false;
                    txtMale.Enabled = false;
                    txtRes.Enabled = true;
                    txtTemp.Enabled = false;

                    txtGro.Clear();
                    txtMale.Clear();
                    txtTemp.Clear();

                    txtRes.Focus();
                    break;
                case "Temperatura":
                    txtGro.Enabled = false;
                    txtMale.Enabled = false;
                    txtRes.Enabled = false;
                    txtTemp.Enabled = true;

                    txtGro.Clear();
                    txtMale.Clear();
                    txtRes.Clear();

                    txtTemp.Focus();
                    break;
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;
            if (!(tecla > 47 && tecla <58 || tecla == 8))
            {
                MessageBox.Show(null,"Ingrese solo números","Validación de datos");
                e.Handled = true; //Controla el evento de presionar una tecla
            }
        }

        private void txtGro_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;
            if (!(tecla > 47 && tecla < 58 || tecla == 8 || tecla == 46))
            {
                MessageBox.Show(null, "Ingrese solo números", "Validación de datos");
                e.Handled = true; //Controla el evento de presionar una tecla no especificada
            }
            if (txtGro.Text.Contains("."))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar.Equals("\b")) {
                    e.Handled = false;
                }
            }
        }

        private void txtMale_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;
            if (!(tecla > 47 && tecla < 58 || tecla == 8 || tecla == 46))
            {
                MessageBox.Show(null, "Ingrese solo números", "Validación de datos");
                e.Handled = true; //Controla el evento de presionar una tecla no especificada
            }
            if (txtMale.Text.Contains("."))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar.Equals("\b"))
                {
                    e.Handled = false;
                }
            }
        }

        private void txtRes_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;
            if (!(tecla > 47 && tecla < 58 || tecla == 8 || tecla == 46))
            {
                MessageBox.Show(null, "Ingrese solo números", "Validación de datos");
                e.Handled = true; //Controla el evento de presionar una tecla no especificada
            }
            if (txtRes.Text.Contains("."))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar.Equals("\b"))
                {
                    e.Handled = false;
                }
            }
        }

        private void txtTemp_KeyPress(object sender, KeyPressEventArgs e)
        {
            int tecla = (int)e.KeyChar;
            if (!(tecla > 47 && tecla < 58 || tecla == 8 || tecla == 46))
            {
                MessageBox.Show(null, "Ingrese solo números", "Validación de datos");
                e.Handled = true; //Controla el evento de presionar una tecla no especificada
            }
            if (txtTemp.Text.Contains("."))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar.Equals("\b"))
                {
                    e.Handled = false;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            inhabilitarComponentes();
            limpiar();
        }
    }
}
