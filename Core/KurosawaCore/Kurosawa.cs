using ConfigController.Models;
using DSharpPlus;
using System.Threading.Tasks;

namespace KurosawaCore
{
    public sealed class Kurosawa
    {
        private DiscordClient Cliente { get; set; }
        private readonly BaseConfig Config;

        public Kurosawa(BaseConfig config)
        {
            Config = config;
        }

        public async Task Iniciar()
        {
            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot
            };

            Cliente = new DiscordClient(config);
            await Cliente.ConnectAsync();
            await Task.Delay(-1);
        }
    }

}
