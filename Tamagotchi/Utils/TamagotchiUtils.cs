using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Utils
{
    internal static class TamagotchiUtils
    {
        public static string Capitalize(string str)
        {
            return $"{str[0].ToString().ToUpper()}{str.Substring(1)}";
        }
    }
}
