using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QUS.Auth.Domain.ValueObjects
{
    public class Email
    {
        public string Entrada { get; private set; }

        public Email(string entrada)
        {
            if (!EhValido(entrada))
                throw new Exception("Email inválido.");

            Entrada = entrada;
        }

        private bool EhValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}