using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo.Modulos.WeebCmds
{
    public class WeebGen
    {
        public WeebClient weebClient { get; private set; }

        public WeebGen()
        {
            weebClient = new WeebClient("Yummi", "1.0.0");
            weebClient.Authenticate("SEpGb1JpYkJtOjUzMGQ0MWE4YTkwZDNiOGU0NWFkZDhjOGQzODBmMDhmZDVjNDQ4ZmM0OWQ3YjdhNzI5ZmU2NWJj", TokenType.Wolke); //n deixar token  no code
        }
    }
}

