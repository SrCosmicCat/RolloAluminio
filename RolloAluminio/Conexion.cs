using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RolloAluminio
{
    internal class Conexion
    {
        //Declaracion de atributos para realizar la conexión
        //Conexion

        String servidor = "localhost"; //Nombre o ip del servidor MySql
        String bd = "EmpresaAluminioSD";
        String usuario = "root";
        String password = "";
        public MySqlConnection connection;

        public Conexion()
        {
            abrirBD();
        }

        public bool abrirBD()
        {
            try
            {
                string connectionString = "server=" + servidor + "; database=" + bd + "; user id=" + usuario + "; password=" + password + ";";

                connection = new MySqlConnection(connectionString);

                connection.Open(); //Abrir conexion
                Console.WriteLine("BD conectada");
                return true;
            }
            catch (MySqlException ex) 
            {
                Console.WriteLine("Error: "+ex);
                return false;
            }
        }

        public bool cerrarBD()
        {
            try
            {
                connection.Close(); //Cerrar conexion
                Console.WriteLine("BD desconectada");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex);
                return false;
            }
        }

    }
    
}
