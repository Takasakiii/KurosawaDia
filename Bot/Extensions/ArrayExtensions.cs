using System;

namespace Bot.Extensions
{
    public class ArrayExtensions
    {
        public string GetRandom(object[] arr)
        {
            Random rand = new Random();
            int i = rand.Next(arr.Length);

            return (string)arr[i];
        }
    }
}
