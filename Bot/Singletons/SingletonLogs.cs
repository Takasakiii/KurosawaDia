using System;
using System.Reflection;

namespace Bot.Singletons
{
    /*
     * Classe responsavel por armazenar e disponibilizar acesso direto a interfaces grafica/texto do bot para fins de criar um log
     * de eventos do bot e da Discord.Net
     */
    public static class SingletonLogs
    {
        //Objeto contendo a instancia da interface aberta no momento
        public static object instanced { get; private set; }

        //Tipo da classe GUI utilizada para abrir o bot
        public static Type tipo { get; private set; }

        //Metodo responsavel por definir os dados de interface aos atributos
        public static void SetInstance(object _instanced, Type _tipo)
        {
            instanced = _instanced;
            tipo = _tipo;
        }
    }
}
