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
using static MainDatabaseControler.Modelos.Servidores;
using static Bot.Extensions.ErrorExtension;
using TokenType = Weeb.net.TokenType;
using UserExtensions = Bot.Extensions.UserExtensions;
using System.Globalization;
using System.Text;
using System.Linq;
using Bot.Modelos;

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
                    Selfmsg = msg + " " + "ele(a) mesmo.";
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

            EmbedBuilder embed = new EmbedBuilder()
            .WithColor(Color.DarkPurple)
            .WithImageUrl(img.Url);

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
                    await Erro.EnviarErroAsync("desculpe, mas esse comando só pode ser usado em um servidor.");
                    return;
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
            await GetWeeb(new WeebInfo("hug", "está abraçando", "está se abraçando"));
        }

        //Comando de Kiss (Beijar)
        public async Task kiss()
        {
            await GetWeeb(new WeebInfo("kiss", "está beijando"));
        }

        //Comando de Slap (Bater)
        public async Task slap()
        {
            await GetWeeb(new WeebInfo("slap", "está dando um tapa no(a)", "está se batendo"));
        }

        //Comando de Punch (Socar)
        public async Task punch()
        {
            await GetWeeb(new WeebInfo("punch", "está socando"));
        }

        //Comando de Lick (Lamber)
        public async Task lick()
        {
            await GetWeeb(new WeebInfo("lick", "está lambendo"));
        }

        //Comando de Cry (Chorar)
        public async Task cry()
        {
            await GetWeeb(new WeebInfo("cry", "está chorando com", "está chorando"));
        }

        //Comando Megumin (mostra uma imagem da megumin) 
        public async Task megumin()
        {
            await GetWeeb(new WeebInfo("megumin", "Megumin ❤", auto: false));
        }

        //Comando Rem (Mostra uma imagem da rem)
        public async Task rem()
        {
            await GetWeeb(new WeebInfo("rem", "rem ❤", auto: false));
        }

        //Comando Pat (Acariciar)
        public async Task pat()
        {
            await GetWeeb(new WeebInfo("pat", "está fazendo carinho no(a)", "está se acariciando"));
        }

        //Comando Dance (Dançar)
        public async Task dance()
        {
            await GetWeeb(new WeebInfo("dance", "começou a dançar"));
        }

        //Comando Fuck (lenny face)
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
                        if (user.Item1 == null || user.Item1 == Contexto.User)
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle($"{authorNick} está se masturbando.")
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                        else
                        {
                            string userNick = userExtensions.GetNickname(user.Item1, !Contexto.IsPrivate);

                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle($"{authorNick} está fudendo {userNick}.")
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Comando OwOify
        /// </summary>
        public async Task owoify()
        {
            if (Comando.Length != 1)
            {
                string input = string.Join(" ", Comando, 1, Comando.Length - 1);
                if (input.Length > 800) {
                    await Erro.EnviarErroAsync("desculpe, mas o texto não poder ser maior que 800 caracteres.");
                    return;
                }
                string[] faces = { "OwO", "owo", "oωo", "òωó", "°ω°", "UwU", ">w<", "^w^" };
                string owoifiedText = string.Empty;

                Random rand = new Random();
                for (int i = 0; i < input.Length; i++) {
                    char ch = input[i];

                    if (ch == 'r' || ch == 'l')
                        owoifiedText += 'w';
                    else if (input.Length - i != 1 && (ch == 'n' || ch == 'N')) {
                        char nextNormalizated = input[i + 1].ToString().Normalize(NormalizationForm.FormD)[0];

                        if (nextNormalizated == 'a' || nextNormalizated == 'e' || nextNormalizated == 'i' || nextNormalizated == 'o' || nextNormalizated == 'u')
                            owoifiedText += $"{ch}y";
                        else if (nextNormalizated == 'A' || nextNormalizated == 'E' || nextNormalizated == 'I' || nextNormalizated == 'O' || nextNormalizated == 'U') 
                            owoifiedText += $"{ch}Y";
                        else
                            owoifiedText += ch;
                    }
                    else if (ch == 'R' || ch == 'L')
                        owoifiedText += 'W';
                    else if (ch == '!') {
                        if (!(input.Length - i != 1 && input[i + 1] == '!')) 
                            owoifiedText += $" {faces[rand.Next(0, faces.Length)]} ";
                    }
                    else  if (input.Length - i > 2) {
                        if (ch == 'o' && input[i + 1] == 'v' && input[i + 2] == 'e') {
                            owoifiedText += "uv";
                            i += 2;
                        }
                        else if (ch == 'O' && input[i + 1] == 'V' && input[i + 2] == 'E') {
                            owoifiedText += "UV";
                            i += 2;
                        }              
                        else
                            owoifiedText += ch;              
                    }
                    else 
                        owoifiedText += ch;
                }
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.DarkPurple)
                    .WithDescription(owoifiedText)
                .Build());
            }
            else
            {
                await Erro.EnviarErroAsync("você precisa me falar um texto.", new DadosErro("<texto>", "Nozomi, eu estou com fome."));
            }
        }
        /// <summary>
        /// Comando BigText, que transforma um texto em um texto feito de emojis do Discord.
        /// </summary>
        public async Task bigtext()
        {
            if (Comando.Length != 1)
            {
                string texto = string.Join(" ", Comando, 1, Comando.Length - 1);
                texto = texto.ToLower();
                texto = texto.Normalize(NormalizationForm.FormD);
                if (texto.Length > 90) {
                    await Erro.EnviarErroAsync("o texto não pode ter mais de 90 caracteres.");
                    return;
                }
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
                            textFormatted += $":{(EmojisNumberList) Convert.ToInt32(ch.ToString())}:";
                        }
                        else if (charCategory == UnicodeCategory.LineSeparator) {
                            textFormatted += ch;
                        }
                        else if (charCategory == UnicodeCategory.SpaceSeparator) {
                            textFormatted += "   ";
                        }
                        else if (texto.Length - i != 1) {
                            if (NeoSmart.Unicode.Emoji.IsEmoji($"{ch}{texto[i + 1]}", 1)) {
                                textFormatted += $"{ch}{texto[i + 1]}";
                                i++;    
                            }
                            else {
                                switch ($"{ch}{texto[i + 1]}") {
                                    case "🏻":
                                    case "🏼":
                                    case "🏽":
                                    case "🏾":
                                    case "🏿":
                                        textFormatted = textFormatted.Remove(textFormatted.Length - 1);
                                        textFormatted += $"{ch}{texto[i + 1]}";
                                        i++;
                                        break;
                                    default:
                                        textFormatted += "❌";
                                        break;
                                }
                            }
                        }
                        else
                        {
                            textFormatted += "❌";
                        }

                        textFormatted += " ";
                    }
                }
                                
                await Contexto.Channel.SendMessageAsync(textFormatted);                
            }
            
            else
            {
                await Erro.EnviarErroAsync("você precisa me falar um texto.", new DadosErro("<texto>", "Kurosawa Dia melhor bot"));
            }
        }
    }
}
