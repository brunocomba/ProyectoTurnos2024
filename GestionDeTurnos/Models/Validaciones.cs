
using Models.Interfaces;
using System.Text.RegularExpressions;

namespace Models
{
    public class Validaciones : IValidaciones
    {
        public bool DniCompleto(int DNI)
        {
            int minimoDeCaracteres = 8;

            if (DNI.ToString().Length < minimoDeCaracteres)
            {
                return false;
            }

            return true;
        }


        public bool TelCompleto(uint Tel)
        {
            int minimoDeCaracteres = 10;

            if (Tel.ToString().Length < minimoDeCaracteres)
            {
                return false;
            }
            return true;
        }

        public bool SoloLetras(string textBox)
        {
            foreach (char c in textBox)
            {
                if (char.IsAsciiLetter(c) == false && c != ' ') 
                {
                    return false;
                }
            }

            return true;
        }

        public bool SoloNumeros(int textBox)
        {
            foreach (char c in textBox.ToString())
            {
                if (char.IsAsciiDigit(c) == false)
                {
                    return false;
                }
            }

            return true;
        }
        public bool SoloNumerosEnTel(uint textBox)
        {
            foreach (char c in textBox.ToString())
            {
                if (char.IsAsciiDigit(c) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool SoloNumerosEnPrecio(decimal textBox)
        {
            foreach (char c in textBox.ToString())
            {
                if (char.IsDigit(c) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CumpleRequisitosEmail(string email)
        {
            if (email.Contains("@") && email.Contains(".") && email.Contains("com"))
            {
                return true;
            }

            return false;
        }

        public bool CumpleRequisitosPass(string Pass)
        {
            bool tieneMayuscula = Regex.IsMatch(Pass, @"[A-Z]");
            bool tieneNumero = Regex.IsMatch(Pass, @"\d");

            if (tieneMayuscula && tieneNumero)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmarPass(string Pass, string ConfirPass)
        {
            if (Pass == ConfirPass)
            {
                return true;

            }
            return false;

        }


        public bool Mayor18(DateTime nacimiento)
        {
            int edad = DateTime.Now.Year - nacimiento.Year;

            if (edad >= 18)
            {
                return true;
            }
            return false;
        }

       
    }
}
