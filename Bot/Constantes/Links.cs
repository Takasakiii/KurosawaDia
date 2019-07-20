using System;

namespace Bot.Constantes
{
    public class Links
    {
        public readonly Tuple<string, string> cat = Tuple.Create("https://nekos.life/api/v2/img/meow", "url");
        public readonly Tuple<string, string> dog = Tuple.Create("https://random.dog/woof.json", "url");
        public readonly Tuple<string, string> hentai = Tuple.Create("https://nekobot.xyz/api/image?type=hentai", "message");
        public readonly Tuple<string, string> anal = Tuple.Create("https://nekobot.xyz/api/image?type=anal", "message");
    }
}
