using System;
using System.Reflection;

namespace Bot.Singletons
{
    public static class SingletonLogs
    {
        public static object instanced { get; private set; }

        public static Type tipo { get; private set; }

        public static void SetInstance(object _instanced, Type _tipo)
        {
            instanced = _instanced;
            tipo = _tipo;
        }

        public static void Fechar()
        {
            MethodInfo metodo = tipo.GetMethod("Fechar");
            metodo.Invoke(instanced, null);
        }
    }
}
