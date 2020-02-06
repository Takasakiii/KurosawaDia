using Bot.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGUI.Core.Extensions
{
    public class LogHub
    {
        internal static string LogHistoric { private set; get; }

        internal delegate void LogAtualizado();

        internal static event LogAtualizado EventLog;


        internal static void OnAlteracaoHub()
        {
            EventLog?.Invoke();
        }

        public void Log(LogEmiter.TipoLog logtype, string e)
        {
            LogHistoric += e + "\n";
            OnAlteracaoHub();
        }


    }


}
