﻿using Bot.Singletons;
using Discord;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot.Nucleo
{
    //Classe responsavel por mandar o log da discord.net para a interface de usuario
    public class Log
    {
        //Evento que captura o log da discord.net e joga para a interface de usuario
        public Task LogTask(LogMessage msg)
        {
            MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
            object[] parms = new object[1];
            parms[0] = msg.ToString();
            metodo.Invoke(SingletonLogs.instanced, parms);

            return Task.CompletedTask;
        }
    }
}
