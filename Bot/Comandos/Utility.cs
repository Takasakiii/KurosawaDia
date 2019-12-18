using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using NeoSmart.Unicode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Bot.Extensions.ErrorExtension;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Comandos
{

    public class Utility : GenericModule
    {
        public Utility(CommandContext contexto, params object[] args) : base(contexto, args)
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

        public async Task avatar()
        {
            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));


            if (!Contexto.IsPrivate)
            {
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg);
                if (getUser.Item1 != null || msg == "")
                {
                    IUser user;
                    if (msg != "")
                    {
                        user = getUser.Item1;
                    }
                    else
                    {
                        user = Contexto.User;
                    }

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();
                    PossiveisMsg[] msgs = null;
                    if (user.Id == Contexto.Client.CurrentUser.Id)
                    {
                        msgs = ArrayExtension.CriarArray(new PossiveisMsg("selfavatarAmor",  "Ownt, que amor, você realmente quer me ver 😍"),  new PossiveisMsg("selfAvatarsemjeito" , "Assim você me deixa sem jeito 😊"));
                    }
                    else
                    {
                        msgs = ArrayExtension.CriarArray(new PossiveisMsg("avatarMsgNice", "Nossa, que avatar bonito, agora sei porque você queria vê-lo 🤣"), new PossiveisMsg("avatarMsgJoy", "Vocês são realmente criativos para avatares 😂"), new PossiveisMsg("avatarMsgIdol", "Com avatar assim seria um disperdicio não se tornar idol 😃"), new PossiveisMsg("avatarMsgFiltro", "Talvez se você pusesse um filtro ficaria melhor... 🤐") );
                    }
                    int rnd = new Random().Next(0, msgs.Length);

                    string msgfinal = await StringCatch.GetStringAsync(msgs[rnd].identifier, msgs[rnd].msgDefault);

                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithTitle(msgfinal)
                        .WithDescription(await StringCatch.GetStringAsync("avatarMsg", "\n\n{0}\n[Link Direto]({1})", user, avatarUrl))
                        .WithImageUrl(avatarUrl)
                    .Build());
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("avatarErro", "não encontrei essa pessoa."), new DadosErro(await StringCatch.GetStringAsync("avatarUso", "@pessoa"), await StringCatch.GetStringAsync("exemloAvatar", "@Hikari#3172")));
                }
            }
            else
            {
                if (msg == "")
                {
                    string avatar = Contexto.User.GetAvatarUrl(0, 2048) ?? Contexto.User.GetDefaultAvatarUrl();
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithAuthor($"{Contexto.User}")
                        .WithDescription(await StringCatch.GetStringAsync("avatarMsg", "[Link Direto]({0})", avatar))
                        .WithImageUrl(avatar)
                    .Build());
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("avatarDm", "desculpe, mas eu não consigo pegar o avatar de outras pessoas no privado 😔"));
                }
            }
        }

        public async Task videochamada()
        {
            SocketGuildUser usr = Contexto.User as SocketGuildUser;

            if (!Contexto.IsPrivate && usr.VoiceChannel != null)
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(await StringCatch.GetStringAsync("videoChamada", "Para acessar o compartilhamento de tela basta [clicar aqui](https://discordapp.com/channels/{0}/{1}) 😀", Contexto.Guild.Id, usr.VoiceChannel.Id))
                .Build());
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("videoChamadaDm", "você precisa estar em um canal de voz e em um servidor para usar esse comando 😔"));
            }
        }

        public async Task emoji()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);
            string emojiInput = Comando[1];
            bool parse = Emote.TryParse(emojiInput, out Emote emote);

            string name, url;

            if (parse)
            {
                name = emote.Name;
                url = emote.Url;
            }
            else if (NeoSmart.Unicode.Emoji.IsEmoji(emojiInput, 1))
            {
                List<string> hexSeq = new List<string>();
                foreach (uint seq in emojiInput.AsUnicodeSequence().AsUtf32)
                {
                    hexSeq.Add(seq.ToString("x"));
                }
                name = "";
                url = $"https://twemoji.maxcdn.com/2/72x72/{string.Join('-', hexSeq)}.png";
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("emoteInvalido", "desculpe, mas o emoji que você digitou é invalido."), new DadosErro(await StringCatch.GetStringAsync("emoteUso", "emoji"), await StringCatch.GetStringAsync("emoteExemplo", ":kanna:")));
                return;                    
            }
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithTitle(name)
                .WithDescription(await StringCatch.GetStringAsync("emoteLink", "[Link Direto]({0})", url))
                .WithImageUrl(url)
                .WithColor(Color.DarkPurple)
            .Build());
        }

        public async Task say()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser userGuild = Contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.ManageMessages)
                {
                    string[] comando = Comando;
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    if (msg != "")
                    {
                        IGuildUser user = await Contexto.Guild.GetUserAsync(Contexto.Client.CurrentUser.Id);
                        if (user.GuildPermissions.ManageMessages)
                        {
                            await Contexto.Message.DeleteAsync();
                        }

                        new EmbedControl().SendMessage(Contexto.Channel, new StringVarsControler(Contexto).SubstituirVariaveis(msg));
                    }
                    else
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("sayErro", "você precisa me falar uma mensagem."), new DadosErro(await StringCatch.GetStringAsync("usoSay", "<mensagem>"), await StringCatch.GetStringAsync("ExemploSay", "@Sora#5614 cade o wallpaper?")));
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync(await StringCatch.GetStringAsync("manageMessages", "Gerenciar Mensagens"));
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("dm", "esse comando só pode ser usado em servidores."));
            }
        }

        public async Task simg()
        {
            if (!Contexto.IsPrivate)
            {
                if (Contexto.Guild.IconUrl != null)
                {
                    string url = $"{Contexto.Guild.IconUrl}?size=2048";
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(Contexto.Guild.Name)
                            .WithDescription(await StringCatch.GetStringAsync("simgTxt", "[Link Direto]({0})", url))
                            .WithImageUrl(url)
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("simgIconErro", "o servidor não tem um ícone."));
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("dm", "esse comando só pode ser usado em servidores."));
            }
        }

        public async Task sugestao()
        {
            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string servidor = "";

            if (!Contexto.IsPrivate)
            {
                servidor = Contexto.Guild.Name;
            }
            else
            {
                servidor = "Privado";
            }

            if (msg != "")
            {
                IMessageChannel canal = await Contexto.Client.GetChannelAsync(556598669500088320) as IMessageChannel;

                await canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Nova sugestão de: {Contexto.User}")
                        .AddField("Sugestão: ", msg)
                        .AddField("Servidor: ", servidor)
                        .WithColor(Color.DarkPurple)
                    .Build());

                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("sugestaoEnviada", "**{0}**, eu sou muito grata por você me dar essa sugestão! Vou usá-la para melhorar e te atender melhor ❤", Contexto.User.ToString()))
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("sugestaoFalar", "você precisa me falar uma sugestão."), new DadosErro(await StringCatch.GetStringAsync("usoSugestao", "<sugestão>"), await StringCatch.GetStringAsync("exemploSugestao", "fazer com que o bot fique mais tempo on")));
            }
        }

        public async Task bug()
        {
            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string servidor;
            if (!Contexto.IsPrivate)
            {
                servidor = Contexto.Guild.Name;
            }
            else
            {
                servidor = "Privado";
            }

            if (msg != "")
            {
                IMessageChannel canal = await Contexto.Client.GetChannelAsync(556598669500088320) as IMessageChannel;

                await canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Novo bug reportado por {Contexto.User}")
                        .AddField("Bug: ", msg)
                        .AddField("Servidor: ", servidor)
                        .WithColor(Color.DarkPurple)
                    .Build());

                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("bugEnviado", "**{0}**, eu sou muito grata por você ter achado esse bug, ajudou muito ❤", Contexto.User.ToString()))
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("bugFalar", "você precisa me falar o bug."), new DadosErro("<bug>", await StringCatch.GetStringAsync("exemploBug", "cadê o status?")));
            }
        }

        public async Task perfil()
        {
            if (!Contexto.IsPrivate)
            {
                string msg = string.Join(" ", Comando, 1, (Comando.Length - 1));
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg);

                IUser user = getUser.Item1;
                if(user == null)
                {
                    user = Contexto.User;
                }

                PontosInterativos pi = new PontosInterativos(new Servidores_Usuarios(new Servidores(Contexto.Guild.Id), new Usuarios(user.Id)));
                Tuple<bool, ulong, PontosInterativos> sucesso_total = await new PontosInterativosDAO().GetPiInfoAsync(pi);
                pi = sucesso_total.Item3;

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

                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(await StringCatch.GetStringAsync("perfilTitle", user.ToString()))
                            .WithThumbnailUrl(user.GetAvatarUrl(size: 2048) ?? user.GetDefaultAvatarUrl())
                            .WithDescription(await StringCatch.GetStringAsync("perfilDesc", "Você tem {0}% dos pontos que faltam pra você subir de nivel.", ((pi.FragmentosPI * 100) / sucesso_total.Item2)))
                            .AddField(await StringCatch.GetStringAsync("perilFieldTitle1", "Seus Pontos:"), await StringCatch.GetStringAsync("perilFieldValue1", pi.FragmentosPI.ToString()), true)
                            .AddField(await StringCatch.GetStringAsync("perilFieldTitle2", "Seu Nivel:"), await StringCatch.GetStringAsync("perilFieldValue2", pi.PI.ToString()), true)
                            .AddField(await StringCatch.GetStringAsync("perilFieldTitle3", "Seu Progresso:"), await StringCatch.GetStringAsync("perilFieldValue3", barra))
                            .WithFooter(await StringCatch.GetStringAsync("perilFooter", "{0}/{1}", pi.FragmentosPI.ToString(), sucesso_total.Item2.ToString()))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("perilDesativado", "os pontos interativos estão desativados nesse servidor."));
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("dm", "esse comando só pode ser usado em servidores."));
            }
        }

        public async Task PIEvent()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser botRepresentacao = await Contexto.Guild.GetCurrentUserAsync() as SocketGuildUser;
                if (botRepresentacao.GuildPermissions.ManageRoles)
                {
                    Servidores server = new Servidores(Id: Contexto.Guild.Id, Nome: Contexto.Guild.Name);
                    Usuarios usuario = new Usuarios(Contexto.User.Id, Contexto.User.ToString(), 0);
                    Servidores_Usuarios servidores_Usuarios = new Servidores_Usuarios(server, usuario);
                    PontosInterativos pontos = new PontosInterativos(servidores_Usuarios, 0);
                    PI pI;
                    Cargos cargos;
                    PontosInterativosDAO dao = new PontosInterativosDAO();
                    Tuple<bool, PontosInterativos, PI, Cargos> res = await dao.AdicionarPontoAsync(pontos);
                    pontos = res.Item2;
                    pI = res.Item3;
                    cargos = res.Item4;
                    if (res.Item1)
                    {
                        StringVarsControler varsControler = new StringVarsControler(Contexto);
                        varsControler.AdicionarComplemento(new StringVarsControler.VarTypes("%pontos%", pontos.PI.ToString()));
                        new EmbedControl().SendMessage(Contexto.Channel, varsControler.SubstituirVariaveis(pI.MsgPIUp));

                    }

                    if (cargos != null)
                    {
                        IRole cargoganho = Contexto.Guild.Roles.ToList().Find(x => x.Id == cargos.Id);
                        if (cargoganho != null)
                        {
                            await ((IGuildUser)Contexto.User).AddRoleAsync(cargoganho);
                        }
                    }
                }
            }
        }
        public async Task whatsify()
        {
            if (Comando.Length != 1)
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.DarkPurple)
                    .WithDescription($"```\n{string.Join(" ", Comando, 1, Comando.Length - 1)}```")
                .Build());
            }
            else 
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("whatsifyIncompleto", "você precisa me falar um texto."), new DadosErro(await StringCatch.GetStringAsync("paramTexto", "<texto>"), await StringCatch.GetStringAsync("whatsifyExemplo", "🙁😕☹️")));
            }
        }
    }
}
