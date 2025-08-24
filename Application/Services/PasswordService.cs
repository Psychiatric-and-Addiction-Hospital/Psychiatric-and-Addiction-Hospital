using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return password.Length >= 8 && hasUpper && hasLower && hasDigit && hasSpecial;
        }
    }
}
