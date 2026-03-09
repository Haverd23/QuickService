using QUS.Core.Exceptions;
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
                throw new AppException("Telefone não pode ser vazio",400);
            }
            string limpo = Regex.Replace(number, @"[\s\-\.\(\)]", "");

            string pattern = @"^(\+55)?\d{10,11}$";

            if (!Regex.IsMatch(limpo, pattern))
            {
                throw new AppException("Telefone inválido",400);
            }

        }
    }
}
