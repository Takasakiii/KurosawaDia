using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class ArrayExtensions
    {
        public static string GetRandom(this string[] arr)
        {
            Random rand = new Random();
            int i = rand.Next(arr.Length);

            return arr[i];
        }
    }
}
