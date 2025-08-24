using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PhoneService : IPhoneService
    {
        public bool PhoneValid(string Phone)
        {
            if (string.IsNullOrWhiteSpace(Phone))
                return false;

            var InternationalRegex = new Regex(@"^\+[1-9]\d{7,14}$");
            var EgyptionRegex = new Regex(@"^01[0-2,5]\d{8}$");

            return InternationalRegex.IsMatch(Phone) || EgyptionRegex.IsMatch(Phone);

        }
    }
}
