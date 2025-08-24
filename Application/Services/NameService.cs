using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NameService : INameService
    {
        public bool IsValidName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            var regex = new Regex(@"^[\p{L}\s]{3,100}$", RegexOptions.Compiled);

            return regex.IsMatch(fullName.Trim());
        }
    }
}
