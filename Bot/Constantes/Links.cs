using System;

namespace Bot.Constantes
{
    public class Links
    {
        public readonly Tuple<string, string> neko = Tuple.Create("https://nekos.life/api/v2/img/neko", "url");
        public readonly Tuple<string, string> cat = Tuple.Create("https://nekos.life/api/v2/img/meow", "url");
        public readonly Tuple<string, string> dog = Tuple.Create("https://random.dog/woof.json", "url");
        public readonly Tuple<string, string> img = Tuple.Create("https://nekos.life/api/v2/img/avatar", "url");
        public readonly Tuple<string, string> hentai = Tuple.Create("https://nekobot.xyz/api/image?type=hentai", "message");
        public readonly Tuple<string, string> nsfw_hentai_gif = Tuple.Create("https://nekos.life/api/v2/img/nsfw_neko_gif", "url");
        public readonly Tuple<string, string> lewdk = Tuple.Create("https://nekos.life/api/v2/img/lewdk", "url");
        public readonly Tuple<string, string> anal = Tuple.Create("https://nekobot.xyz/api/image?type=anal", "message");
        public readonly Tuple<string, string> holo = Tuple.Create("https://nekobot.xyz/api/image?type=holo", "message");
    }
}
