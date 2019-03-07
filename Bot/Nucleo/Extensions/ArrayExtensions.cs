using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class ArrayExtensions //bbbbbbbbbb
    {
        public static string GetRandom(this string[] arr) // ai essa doeu
        {
            Random rand = new Random();
            int i = rand.Next(arr.Length);

            return arr[i];
        }
    }
}
