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
        internal static Action<string> EventManual;
        internal static string LogHistoric
        {
            get { return LogHistoric; }
            private set
            {
                LogHistoric = value;
                EventManual?.Invoke(value);
            }
        }

        //public delegate void HubAlterada(string Valor);

        //public static event HubAlterada AlteracaoHub;



        public void Log(LogEmiter.TipoLog logtype, string e)
        {
            LogHistoric += e + "\n";
        }
    }


}
