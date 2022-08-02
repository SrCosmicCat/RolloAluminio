using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RolloAluminio
{
    internal class Rollo
    {
        private int idR;
        private double GroR;
        private double MaleR;
        private double ResR;
        private double TempR;
        private int IdPr;

        public int IdR { get => idR; set => idR = value; }
        public double GroR1 { get => GroR; set => GroR = value; }
        public double MaleR1 { get => MaleR; set => MaleR = value; }
        public double ResR1 { get => ResR; set => ResR = value; }
        public double TempR1 { get => TempR; set => TempR = value; }
        public int IdPr1 { get => IdPr; set => IdPr = value; }
    }
}
