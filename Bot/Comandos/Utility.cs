using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Linq;
using System.Threading;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Comandos
{

    public class Utility : GenericModule
    {
        public Utility(CommandContext contexto, object[] args) : base (contexto, args)
        {

        }
        private struct PossiveisMsg
        {
            public string identifier { get; private set; }
            public string msgDefault { get; private set; }

            public PossiveisMsg(string _identifier, string _msgDefault)
            {
                identifier = _identifier;
                msgDefault = _msgDefault;

            }
        }

        public void avatar()
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));


            if (!contexto.IsPrivate)
            {
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(contexto.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);
                if (getUser.Item1 != null || msg == "")
                {
                    IUser user;
                    if (msg != "")
                    {
                        user = getUser.Item1;
                    }
                    else
                    {
                        user = contexto.User;
                    }

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                    PossiveisMsg[] msgs = { new PossiveisMsg("avatarMsgNice", "Nossa que avatar bonito, agora sei porque você queria ve-lo"), new PossiveisMsg("avatarMsgJoy", "Vocês são realmente criativos para avatares 😂"), new PossiveisMsg("avatarMsgIdol", "Com avatar assim seria um disperdicio não se tornar idol 😃"), new PossiveisMsg("avatarMsgFiltro", "Talvez se você posse um filtro ficaria melhor...") };
                    int rnd = new Random().Next(0, msgs.Length);

                    string msgfinal = StringCatch.GetString(msgs[rnd].identifier, msgs[rnd].msgDefault);

                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithTitle(msgfinal)
                        .WithDescription(StringCatch.GetString("avatarMsg", "\n\n{0}\n[Link Direto]({1})", user, avatarUrl))
                        .WithImageUrl(avatarUrl)
                    .Build());
                }
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("avatarErro", "**{0}** não encontrei essa pessoa", contexto.User.ToString()))
                            .AddField(StringCatch.GetString("usoCmd", "Uso do Comando: "), StringCatch.GetString("avatarUso", "`{0}avatar @pessoa`", (string)args[0]))
                            .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemloAvatar", "`{0}avatar @Hikari#3172`", (string)args[0]))
                            .WithColor(Color.Red)
                     .Build());
                }
            }
            else
            {
                if (msg == "")
                {
                    string avatar = contexto.User.GetAvatarUrl(0, 2048) ?? contexto.User.GetDefaultAvatarUrl();
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithAuthor($"{contexto.User}")
                        .WithDescription(StringCatch.GetString("avatarMsg", "[Link Direto]({0})", avatar))
                        .WithImageUrl(avatar)
                    .Build());
                }
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("avatarDm", "**{0}** desculpa mas eu não consigo pegar o avatar de outras pessoas no privado 😔", contexto.User.ToString()))
                            .WithColor(Color.Red)
                     .Build());
                }
            }
        }

        public void videochamada()
        {
            SocketGuildUser usr = contexto.User as SocketGuildUser;

            if (!contexto.IsPrivate && usr.VoiceChannel != null)
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(StringCatch.GetString("videoChamada", "Para acessar o compartilhamento de tela basta [Clicar Aqui](https://discordapp.com/channels/{0}/{1}) 😀", contexto.Guild.Id, usr.VoiceChannel.Id))
                .Build());
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithDescription(StringCatch.GetString("videoChamadaDm", "você precisa estar em um canal de voz e em um servidor para usar esse comando"))
                .Build());
            }
        }

        public void emoji()
        {
            string[] comando = (string[])args[1];

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            try
            {
                bool parse = Emote.TryParse(comando[1], out Emote emote);

                if (parse)
                {

                    embed.WithTitle(emote.Name);
                    embed.WithDescription(StringCatch.GetString("emoteLink", "[Link Direto]({0})", emote.Url));
                    embed.WithImageUrl(emote.Url);
                }
                else
                {
                    HttpExtensions http = new HttpExtensions();
                    string name = http.GetSiteHttp("https://ayura.com.br/links/emojis.json", comando[1]);

                    string shortName = "";
                    foreach (char character in name)
                    {
                        if (character != ':')
                        {
                            shortName += character;
                        }
                    }
                    string unicode = http.GetSite($"https://www.emojidex.com/api/v1/emoji/{shortName}", "unicode");

                    embed.WithTitle(shortName);
                    embed.WithDescription(StringCatch.GetString("emoteLinkUnicode", "[Link Direto]({0})", $"https://twemoji.maxcdn.com/2/72x72/{unicode}.png"));
                    embed.WithImageUrl($"https://twemoji.maxcdn.com/2/72x72/{unicode}.png");
                }
            }
            catch
            {
                embed.WithTitle(StringCatch.GetString("emoteInvalido", "Desculpe mas o emoji que você digitou é invalido"));
                embed.AddField(StringCatch.GetString("usoCmd", "Uso do comando: "), StringCatch.GetString("emoteUso", "`{0}emote emoji`", (string)args[0]));
                embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("emoteExeemplo", "`{0}emote :kanna:`", (string)args[0]));
                embed.WithColor(Color.Red);
            }

            contexto.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void say()
        {
            if (!contexto.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    IGuildUser user = contexto.User as IGuildUser;
                    if (user.GuildPermissions.ManageMessages)
                    {
                        contexto.Message.DeleteAsync().GetAwaiter().GetResult();
                    }

                    new EmbedControl().SendMessage(contexto.Channel, msg);
                }
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("sayErro", "**{0}** você precisa de me falar uma mensagem", contexto.User.ToString()))
                            .AddField(StringCatch.GetString("usoCmd", "Uso do comando:"), StringCatch.GetString("usoSay", "`{0}say <mensagem>`", (string)args[0]))
                            .AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("ExemploSay", "`{0}say @Sora#5614 cade o wallpaper?`", (string)args[0]))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("sayDm", "você so pode usar esse comando em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void simg()
        {
            if (!contexto.IsPrivate)
            {
                if (contexto.Guild.IconUrl != null)
                {
                    string url = $"{contexto.Guild.IconUrl}?size=2048";
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(contexto.Guild.Name)
                            .WithDescription(StringCatch.GetString("simgTxt", "[Link Direto]({0})", url))
                            .WithImageUrl(url)
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("simgIconErro", "**{0}** o servidor não tem um icone", contexto.User.ToString()))
                        .WithColor(Color.Red)
                    .Build()); ;
                }
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("simgDm", "**{0}** esse comando so pode ser usado em servidores", contexto.User.ToString()))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void sugestao()
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string servidor = "";

            if (!contexto.IsPrivate)
            {
                servidor = contexto.Guild.Name;
            }
            else
            {
                servidor = "Privado";
            }

            if (msg != "")
            {
                IMessageChannel canal = contexto.Client.GetChannelAsync(556598669500088320).GetAwaiter().GetResult() as IMessageChannel;

                canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Nova sugestão de: {contexto.User}")
                        .AddField("Sugestão: ", msg)
                        .AddField("Servidor: ", servidor)
                        .WithColor(Color.DarkPurple)
                    .Build());

                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("sugestaoEnviada", "**{0}** eu sou muito grata por você me dar essa sugestão, vou usa-la para melhorar e te atender melhor ❤", contexto.User.ToString()))
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("sugestaoFalar", "**{0}** você precisa me falara uma sugestão", contexto.User.ToString()))
                        .AddField(StringCatch.GetString("usoCmd", "Uso do Comando: "), StringCatch.GetString("usoSugestao", "`{0}sugestao <sugestão>`", (string)args[0]))
                        .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploCmd", "`{0}sugestao fazer com que o bot ficasse mais tempo on`" ,(string)args[0]))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void perfil()
        {
            if (!contexto.IsPrivate)
            {
                PontosInterativos pi = new PontosInterativos(new Servidores_Usuarios(new Servidores(contexto.Guild.Id), new Usuarios(contexto.User.Id)));
                Tuple<bool, ulong> sucesso_total = new PontosInterativosDAO().GetPiInfo(ref pi);

                if (sucesso_total.Item1)
                {
                    string barra = "";
                    for (ulong i = 0; i < 10; i++)
                    {
                        if( i < ((pi.FragmentosPI * 100) / sucesso_total.Item2) / 10)
                        {
                            barra += "💠";
                        }
                        else
                        {
                            barra += "🔹";
                        }
                    }

                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(StringCatch.GetString("perfilTitle", contexto.User.ToString()))
                            .WithThumbnailUrl(contexto.User.GetAvatarUrl(size: 2048) ?? contexto.User.GetDefaultAvatarUrl())
                            .WithDescription(StringCatch.GetString("perfilDesc", "Você tem {0}% dos pontos que faltam pra você subir de nivel", ((pi.FragmentosPI * 100) / sucesso_total.Item2)))
                            .AddField(StringCatch.GetString("perilFieldTitle1", "Seus Pontos:"), StringCatch.GetString("perilFieldValue1", pi.FragmentosPI.ToString()), true)
                            .AddField(StringCatch.GetString("perilFieldTitle2", "Seu Nivel:"), StringCatch.GetString("perilFieldValue2", pi.PI.ToString()), true)
                            .AddField(StringCatch.GetString("perilFieldTitle3", "Seu Progresso:"), StringCatch.GetString("perilFieldValue3", barra))
                            .WithFooter(StringCatch.GetString("perilFooter", "{0}/{1}", pi.FragmentosPI.ToString(), sucesso_total.Item2.ToString()))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("perilDesativado", "**{0}** os pontos interativos estão desativados nesse servidor", contexto.User.ToString()))
                            .WithColor(Color.Red)
                         .Build());
                }
            }
            else
            {
                contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(StringCatch.GetString("perilDm", "Esse comando do pode ser usado em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void PIEvent()
        {
            if (!contexto.IsPrivate)
            {
                new Thread(() =>
                {
                    SocketGuildUser botRepresentacao = contexto.Guild.GetCurrentUserAsync().GetAwaiter().GetResult() as SocketGuildUser;
                    if (botRepresentacao.GuildPermissions.ManageRoles)
                    {
                        new BotCadastro(() =>
                        {
                            Servidores server = new Servidores(Id: contexto.Guild.Id, Nome: contexto.Guild.Name);
                            Usuarios usuario = new Usuarios(contexto.User.Id, contexto.User.ToString(), 0);
                            Servidores_Usuarios servidores_Usuarios = new Servidores_Usuarios(server, usuario);
                            PontosInterativos pontos = new PontosInterativos(servidores_Usuarios, 0);
                            PI pI;
                            Cargos cargos;
                            PontosInterativosDAO dao = new PontosInterativosDAO();
                            if (dao.AdicionarPonto(ref pontos, out pI, out cargos))
                            {
                                StringVarsControler varsControler = new StringVarsControler(contexto);
                                varsControler.AdicionarComplemento(new StringVarsControler.VarTypes("%pontos%", pontos.PI.ToString()));
                                new EmbedControl().SendMessage(contexto.Channel, varsControler.SubstituirVariaveis(pI.MsgPIUp));

                            }

                            if (cargos != null)
                            {
                                IRole cargoganho = contexto.Guild.Roles.ToList().Find(x => x.Id == cargos.Id);
                                if (cargoganho != null)
                                {
                                    ((IGuildUser)contexto.User).AddRoleAsync(cargoganho);
                                }
                            }
                        }, contexto).EsperarOkDb();
                    }
                }).Start();
            }
        }

    }
}
