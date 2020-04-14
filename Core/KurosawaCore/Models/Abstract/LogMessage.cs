using DSharpPlus.EventArgs;
using KurosawaCore.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KurosawaCore.Models.Abstract
{
    public class LogMessage : EventArgs
    {
        public NivelLog Level { get; }
        public string Application { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        internal LogMessage(DebugLogMessageEventArgs e)
        {
            Level = (NivelLog)Convert.ToByte(e.Level);
            Application = e.Application;
            Message = e.Message;
            Timestamp = e.Timestamp;
        }

        public override string ToString()
        {
            return $"[{Timestamp}] [{Application}] [{Level}] {Message}";
        }
    }
}
