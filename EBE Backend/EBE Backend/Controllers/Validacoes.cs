using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EBE_Backend.Controller
{
    class Validacoes
    {
        public void VerifyString(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) //Para str sem numeros
            {
                throw new Exception("Name field is missing");
            } 

            if (!Regex.IsMatch(name, @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$"))
            {
                throw new Exception("Name field is invalid");
            }
            
        }
        
        public void VerifyCPF(string cpf)
        {
            if(cpf.Length != 11)
            {
                throw new Exception("CPF field is invalid");
            }

            if (string.IsNullOrWhiteSpace(cpf)) //Para str de cpf
            {
                throw new Exception("CPF field is missing");
            }

            if (Regex.IsMatch(cpf, @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$"))
            {
                throw new Exception("CPF field is invalid");
            }
        }
        public void VerifyCNPJ(string cnpj)
        {
            if (cnpj.Length != 14)
            {
                throw new Exception("CNPJF field is invalid");
            }

            if (string.IsNullOrWhiteSpace(cnpj)) //Para str de cpf
            {
                throw new Exception("CNPJ field is missing");
            }

            if (Regex.IsMatch(cnpj, @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$"))
            {
                throw new Exception("CNPJ field is invalid");
            }
        }
        /*public void VerifyEMail (string email)
        {

        }*/
    }
}