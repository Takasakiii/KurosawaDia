using Bot.Extensions;
using Bot.GenericTypes;
using ConfigurationControler.DAO;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using Weeb.net;
using Weeb.net.Data;
using static MainDatabaseControler.Modelos.Servidores;
using TokenType = Weeb.net.TokenType;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    // Classe contem o modulo weeb em GenericModule e seus comandos
    public class Weeb : GenericModule
    {
        //Contrutor do modulo e passagem do Contexto e args
        public Weeb(CommandContext contexto, object[] args) : base (contexto, args)
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
                    Selfmsg = msg + " " + StringCatch.GetString("weebSelfDefault", "ele(a) mesmo");
                }

                Tipo = tipo;
                Msg = msg;
                Auto = auto;
            }
        }

        //Metodo que gerencia todos os comandos de weeb
        private void GetWeeb(WeebInfo weeb)
        {
            var apiConfig = new ApisConfigDAO().Carregar();

            WeebClient weebClient = new WeebClient();
            weebClient.Authenticate(apiConfig.Item2[0].Token, TokenType.Wolke).GetAwaiter().GetResult();
            RandomData img = weebClient.GetRandomAsync(weeb.Tipo, new string[] { }, FileType.Any, false, NsfwSearch.False).GetAwaiter().GetResult();

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);
            embed.WithImageUrl(img.Url);

            if (weeb.Auto)
            {
                if (!Contexto.IsPrivate)
                {
                    string[] comando = (string[])args[1];
                    string cmd = string.Join(" ", comando, 1, (comando.Length - 1));

                    UserExtensions userExtensions = new UserExtensions();
                    Tuple<IUser, string> getUser = userExtensions.GetUser(Contexto.Guild.GetUsersAsync().GetAwaiter().GetResult(), cmd);

                    
                    string author = userExtensions.GetNickname(Contexto.User, !Contexto.IsPrivate);

                    if (getUser.Item1 == null || getUser.Item1 == Contexto.User)
                    {
                        embed.WithTitle($"{author} {weeb.Selfmsg}");
                    }
                    else
                    {
                        string user = userExtensions.GetNickname(getUser.Item1, !Contexto.IsPrivate);
                        embed.WithTitle($"{author} {weeb.Msg} {user}");
                    }

                    
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("weebDm", "Desculpe, mas so posso execultar esse comando em um servidor 😔"));
                    embed.WithColor(Color.Red);
                    embed.WithImageUrl(null);
                }
            }
            else
            {
                embed.WithTitle(weeb.Msg);
            }

            Contexto.Channel.SendMessageAsync(embed: embed.Build());
        }

        //Comando de Hug (Abraço)
        public void hug()
        {
            GetWeeb(new WeebInfo("hug", StringCatch.GetString("hugTxt", "está abraçando"), StringCatch.GetString("hugSelf", "está se abraçando")));
        }

        //Comando de Kiss (Beijar)
        public void kiss()
        {
            GetWeeb(new WeebInfo("kiss", StringCatch.GetString("kissTxt", "está beijando")));
        }

        //Comando de Slap (Bater)
        public void slap()
        {
            GetWeeb(new WeebInfo("slap", StringCatch.GetString("slapTxt", "está dando um tapa no(a)"), StringCatch.GetString("slapSelf", "está se batendo")));
        }

        //Comando de Punch (Socar)
        public void punch()
        {
            GetWeeb(new WeebInfo("punch", StringCatch.GetString("punchTxt", "esta socando")));
        }

        //Comando de Lick (Lamber)
        public void lick()
        {
            GetWeeb(new WeebInfo("lick", StringCatch.GetString("lickTxt", "esta lambendo")));
        }

        //Comando de Cry (Chorar)
        public void cry()
        {
            GetWeeb(new WeebInfo("cry", StringCatch.GetString("cryTxt", "está chorando com"), StringCatch.GetString("crySelf", "está chorando")));
        }

        //Comando Megumin (mostra uma imagem da megumin) 
        public void megumin()
        {
            GetWeeb(new WeebInfo("megumin", StringCatch.GetString("meguminTxt", "Megumin ❤"), auto: false));
        }

        //Comando Rem (Mostra uma imagem da rem)
        public void rem()
        {
            GetWeeb(new WeebInfo("rem", StringCatch.GetString("remTxt", "rem ❤"), auto: false));
        }

        //Comando Pat (Acariciar)
        public void pat()
        {
            GetWeeb(new WeebInfo("pat", StringCatch.GetString("patTxt", "está fazendo carinho no(a)"), StringCatch.GetString("patSelf", "está se acariciando")));
        }

        //Comando Dance (Dançar)
        public void dance()
        {
            GetWeeb(new WeebInfo("dance", StringCatch.GetString("danceTxt", "está dançando com"), StringCatch.GetString("danceSelf", "começou a dançar")));
        }

        //Comando Fuck (leny face)
        public void fuck()
        {
            if (!Contexto.IsPrivate)
            {
                Servidores servidor = new Servidores(Contexto.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    bool explicitImg = false;
                    if (servidor.Permissoes == PermissoesServidores.ServidorPika || servidor.Permissoes == PermissoesServidores.LolisEdition)
                    {
                        explicitImg = true;
                    }

                    Fuck fuck = new Fuck(explicitImg);

                    if (new FuckDAO().GetImg(ref fuck))
                    {

                        string[] comando = (string[])args[1];
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                        UserExtensions userExtensions = new UserExtensions();
                        Tuple<IUser, string> user = userExtensions.GetUser(Contexto.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                        string authorNick = userExtensions.GetNickname(Contexto.User, !Contexto.IsPrivate);
                        if (user.Item1 != null)
                        {
                            string userNick = userExtensions.GetNickname(user.Item1, !Contexto.IsPrivate);
                            

                            Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(StringCatch.GetString("fuckTxt", "{0} esta fudendo {1}", authorNick, userNick))
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                        else
                        {
                            Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(StringCatch.GetString("fuckSelf", "{0} esta se masturbando", authorNick))
                                    .WithImageUrl(fuck.Img)
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                    }
                }
            }
        }
    }
}
