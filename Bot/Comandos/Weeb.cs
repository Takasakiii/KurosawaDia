using Bot.Extensions;
using Bot.GenericTypes;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Threading.Tasks;
using Weeb.net;
using Weeb.net.Data;
using System.Text.RegularExpressions;
using static MainDatabaseControler.Modelos.Servidores;
using static Bot.Extensions.ErrorExtension;
using TokenType = Weeb.net.TokenType;
using UserExtensions = Bot.Extensions.UserExtensions;
using System.Globalization;
using System.Text;
using System.Linq;

namespace Bot.Comandos
{
    // Classe contem o modulo weeb em GenericModule e seus comandos
    public class Weeb : GenericModule
    {
        //Contrutor do modulo e passagem do Contexto e args
        public Weeb(CommandContext contexto, params object[] args) : base(contexto, args)
        {

        }

        //Struct com os dados relacionados a Weeb
        private struct WeebInfo
        {
            //Tipo de Weeb
            public string Tipo { private set; get; }
            //Msg que sera exibida no embed Weeb
            public string Msg { private set; get; }
            //Msg caso o usuario não marque ninguem
            public string Selfmsg { private set; get; }
            //Switch caso para saber se existe ou não nescessidade de usuario
            public bool Auto { private set; get; }
            //Contrutor da struct
            public WeebInfo(string tipo, string msg, string selfmsg = "", bool auto = true)
            {
                if (!string.IsNullOrEmpty(selfmsg))
                {
                    Selfmsg = selfmsg;
                }
                else
                {
                    Selfmsg = msg + " " + StringCatch.GetStringAsync("weebSelfDefault", "ele(a) mesmo.").GetAwaiter().GetResult();
                }

                Tipo = tipo;
                Msg = msg;
                Auto = auto;
            }
        }

        //Metodo que gerencia todos os comandos de weeb
        private async Task GetWeeb(WeebInfo weeb)
        {
            ApisConfig[] apiConfig = await new ApisConfigDAO().CarregarAsync();

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(apiConfig[0].Token, TokenType.Wolke).GetAwaiter().GetResult();
            RandomData img = weebClient.GetRandomAsync(weeb.Tipo, new string[] { }, FileType.Any, false, NsfwSearch.False).GetAwaiter().GetResult();

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);
            embed.WithImageUrl(img.Url);

            if (weeb.Auto)
            {
                if (!Contexto.IsPrivate)
                {
                    string[] comando = Comando;
                    string cmd = string.Join(" ", comando, 1, (comando.Length - 1));

                    UserExtensions userExtensions = new UserExtensions();
                    Tuple<IUser, string> getUser = userExtensions.GetUser(await Contexto.Guild.GetUsersAsync(), cmd);

                    
                    string author = userExtensions.GetNickname(Contexto.User, !Contexto.IsPrivate);

                    if (getUser.Item1 == null || getUser.Item1 == Contexto.User)
                    {
                        embed.WithTitle($"{author} {weeb.Selfmsg}.");
                    }
                    else
                    {
                        string user = userExtensions.GetNickname(getUser.Item1, !Contexto.IsPrivate);
                        embed.WithTitle($"{author} {weeb.Msg} {user}.");
                    }

                    
                }
                else
                {
                    embed.WithDescription(await StringCatch.GetStringAsync("weebDm", "Desculpe, mas só posso executar esse comando em um servidor 😔"));
                    embed.WithColor(Color.Red);
                    embed.WithImageUrl(null);
                }
            }
            else
            {
                embed.WithTitle(weeb.Msg);
            }

            await Contexto.Channel.SendMessageAsync(embed: embed.Build());
        }

        //Comando de Hug (Abraço)
        public async Task hug()
        {
            await GetWeeb(new WeebInfo("hug", await StringCatch.GetStringAsync("hugTxt", "está abraçando"), await StringCatch.GetStringAsync("hugSelf", "está se abraçando")));
        }

        //Comando de Kiss (Beijar)
        public async Task kiss()
        {
            await GetWeeb(new WeebInfo("kiss", await StringCatch.GetStringAsync("kissTxt", "está beijando")));
        }

        //Comando de Slap (Bater)
        public async Task slap()
        {
            await GetWeeb(new WeebInfo("slap", await StringCatch.GetStringAsync("slapTxt", "está dando um tapa no(a)"), await StringCatch.GetStringAsync("slapSelf", "está se batendo")));
        }

        //Comando de Punch (Socar)
        public async Task punch()
        {
            await GetWeeb(new WeebInfo("punch", await StringCatch.GetStringAsync("punchTxt", "está socando")));
        }

        //Comando de Lick (Lamber)
        public async Task lick()
        {
            await GetWeeb(new WeebInfo("lick", await StringCatch.GetStringAsync("lickTxt", "está lambendo")));
        }

        //Comando de Cry (Chorar)
        public async Task cry()
        {
            await GetWeeb(new WeebInfo("cry", await StringCatch.GetStringAsync("cryTxt", "está chorando com"), await StringCatch.GetStringAsync("crySelf", "está chorando")));
        }

        //Comando Megumin (mostra uma imagem da megumin) 
        public async Task megumin()
        {
            await GetWeeb(new WeebInfo("megumin", await StringCatch.GetStringAsync("meguminTxt", "Megumin ❤"), auto: false));
        }

        //Comando Rem (Mostra uma imagem da rem)
        public async Task rem()
        {
            await GetWeeb(new WeebInfo("rem", await StringCatch.GetStringAsync("remTxt", "rem ❤"), auto: false));
        }

        //Comando Pat (Acariciar)
        public async Task pat()
        {
            await GetWeeb(new WeebInfo("pat", await StringCatch.GetStringAsync("patTxt", "está fazendo carinho no(a)"), await StringCatch.GetStringAsync("patSelf", "está se acariciando")));
        }

        //Comando Dance (Dançar)
        public async Task dance()
        {
            await GetWeeb(new WeebInfo("dance", await StringCatch.GetStringAsync("danceTxt", "está dançando com"), await StringCatch.GetStringAsync("danceSelf", "começou a dançar")));
        }

        //Comando Fuck (leny face)
        public async Task fuck()
        {
            if (!Contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(Contexto.Guild.Id);
                Tuple<bool, Servidores> res = await new ServidoresDAO().GetPermissoesAsync(servidor);
                servidor = res.Item2;
                if (res.Item1)
                {
                    bool explicitImg = false;
                    if (servidor.Permissoes == PermissoesServidores.ServidorPika || servidor.Permissoes == PermissoesServidores.LolisEdition)
                    {
                        explicitImg = true;
                    }

                    Fuck fuck = new Fuck(explicitImg);
                    Tuple<bool, Fuck> res2 = await new FuckDAO().GetImgAsync(fuck);
                    fuck = res2.Item2;
                    if (res2.Item1)
                    {

                        string[] comando = Comando;
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                        UserExtensions userExtensions = new UserExtensions();
                        Tuple<IUser, string> user = userExtensions.GetUser(await Contexto.Guild.GetUsersAsync(), msg);

                        string authorNick = userExtensions.GetNickname(Contexto.User, !Contexto.IsPrivate);
                        if (user.Item1 != null)
                        {
                            string userNick = userExtensions.GetNickname(user.Item1, !Contexto.IsPrivate);
                            

                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(await StringCatch.GetStringAsync("fuckTxt", "{0} está fudendo {1}.", authorNick, userNick))
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(await StringCatch.GetStringAsync("fuckSelf", "{0} está se masturbando.", authorNick))
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                    }
                }
            }
        }
        public async Task owoify()
        {
            if (!(Comando.Length == 1))
            {
                string input = string.Join(" ", Comando, 1, Comando.Length - 1);
                string[] faces = { @"(・\`ω\´・)", "OwO", "owo", "oωo", "òωó", "°ω°", "UwU", ">w<", "^w^" };
                input = Regex.Replace(input, @"(?:r|l)", "w");
                input = Regex.Replace(input, @"(?:R|L)", "W");
                input = Regex.Replace(input, @"n([aeiouãõáéíóúâêîôûàèìòùäëïöü])", "ny$1");
                input = Regex.Replace(input, @"N([aeiouãõáéíóúâêîôûàèìòùäëïöü])", "Ny$1");
                input = Regex.Replace(input, @"N([AEIOUÃÕÁÉÍÓÚÂÊÎÔÛÀÈÌÒÙÄËÏÖÜ])", "NY$1");
                input = Regex.Replace(input, @"ove", "uv");

                Random rand = new Random();
                Regex regex = new Regex(@"\!+");
                while (regex.Match(input).Success) {
                    input = regex.Replace(input, $" {faces[rand.Next(0, faces.Length)]} ", 1);
                }

                if (!(input.Length > 2048))
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(input)
                    .Build());
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("owoifyGrande", "desculpe, mas seu texto é muito grande para que eu possa enviar."));
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("owoifyIncompleto", "você precisa me falar um texto."), new DadosErro(await StringCatch.GetStringAsync("owoifyUso", "<texto>"), await StringCatch.GetStringAsync("owoifyExemplo", "Nozomi, eu estou com fome.")));
            }
        }
        
        public async Task bigtext()
        {
            if (!(Comando.Length == 1))
            {
                string texto = string.Join(" ", Comando, 1, Comando.Length - 1);
                texto = texto.ToLower();
                texto = texto.Normalize(NormalizationForm.FormD);
                string textFormatted = "";
                for (int i = 0; i < texto.Length; i++)
                {
                    char ch = texto[i];
                    UnicodeCategory charCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
                    if (charCategory != UnicodeCategory.NonSpacingMark)
                    {
                        if (charCategory == UnicodeCategory.LowercaseLetter) {
                            textFormatted += $":regional_indicator_{ch}:";
                        }
                        else if (charCategory == UnicodeCategory.DecimalDigitNumber) {
                            // 0 -> :zero:
                            // ...
                            // 9 -> :nine:
                        }
                        else if (charCategory == UnicodeCategory.LineSeparator) {
                            textFormatted += ch;
                        }
                        else if (charCategory == UnicodeCategory.SpaceSeparator) {
                            textFormatted += "   ";
                        }
                        else if (texto.Length - i != 1 && NeoSmart.Unicode.Emoji.IsEmoji($"{ch}{texto[i + 1]}", 1)) {
                            textFormatted += $"{ch}{texto[i + 1]}";
                            i++;
                        }
                        else
                        {
                            // falta arrumar o emoji de cor
                            if (!(texto.Length - i != 1 && NeoSmart.Unicode.Emoji.SkinTones.All.All(t => t.AsUtf32.ToString() == $"{ch}{texto[i + 1]}"))) {
                                textFormatted += "❌";
                            }
                        }

                        textFormatted += " ";
                    }
                }
                textFormatted = textFormatted.Normalize(NormalizationForm.FormC);

                await Contexto.Channel.SendMessageAsync(textFormatted);
            }
            
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("bigtextIncompleto", "você precisa me falar um texto."), new DadosErro(await StringCatch.GetStringAsync("bigtextUso", "<texto>"), await StringCatch.GetStringAsync("owoifyExemplo", "Kurosawa Dia melhor bot")));
            }
        }
    }
}
