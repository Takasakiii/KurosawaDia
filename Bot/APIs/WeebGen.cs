using Bot.APIs;
using Weeb.net;

namespace Bot.Constructors
{
    public class WeebGen
    {
        public WeebClient weebClient { get; private set; }
        private ApisGen gen = new ApisGen();
        public WeebGen()
        {
            weebClient = new WeebClient("Yummi", "1.0.0");
            weebClient.Authenticate(gen.apiConfig.weebToken, TokenType.Wolke);
        }
    }
}
