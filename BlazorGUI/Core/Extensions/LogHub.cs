using Bot.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGUI.Core.Extensions
{
    internal class LogHub
    {
        internal static string LogHistoric { get;  private set; }

        public void Log(LogEmiter.TipoLog logtype, string e)
        {
            LogHistoric += e + "\n";
        }
    }
}
