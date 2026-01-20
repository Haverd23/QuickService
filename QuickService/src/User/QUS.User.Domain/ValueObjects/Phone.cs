using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QUS.Users.Domain.ValueObjects
{
    public class Phone
    {
        public string Number { get; private set; }
        public Phone(string number)
        {
            IsValid(number);
            Number = number;
        }
        private void IsValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Telefone não pode ser vazio");
            }
            string limpo = Regex.Replace(number, @"[\s\-\.\(\)]", "");

            string pattern = @"^(\+55)?\d{10,11}$";

            if (!Regex.IsMatch(limpo, pattern))
            {
                throw new ArgumentException("Telefone inválido");
            }

        }
    }
}
