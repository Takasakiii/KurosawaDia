using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bot.Nucleo.Extensions
{
    public static class CollectionExtensions //aaaaaaaaaa
    {
        public static ulong First(this IReadOnlyCollection<ulong> collection)
        {
            return collection.ElementAt(0);
        }
    }
}

