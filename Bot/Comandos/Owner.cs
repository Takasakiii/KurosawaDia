using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Adms;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Owner : GenericModule
    {

        public Owner(CommandContext contexto, string prefixo, string[] comando) : base(contexto, prefixo, comando)
        {

        }

        public async Task ping()
        {
            if ((await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                DiscordShardedClient client = Contexto.Client as DiscordShardedClient;

                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(await StringCatch.GetString("respostaPing", "Meu ping é {0}", client.Latency)) //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                    .Build());

            }
            else
            {
                Servidores servidor = new Servidores(0, PrefixoServidor.ToCharArray());
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }

        public async Task setespecial()
        {
            if ((await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                try
                {
                    string[] comando = Comando;
                    Servidores servidor = new Servidores(Convert.ToUInt64(comando[1]), (PermissoesServidores)Convert.ToInt32(comando[2]));

                    if (await new ServidoresDAO().SetEspecialAsync(servidor))
                    {
                        IGuild servi = Contexto.Client.GetGuildAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult();
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetString("setEspecialSetado", "O servidor: `{0}` ganhou a permissão: `{1}`", servi.Name, (PermissoesServidores)Convert.ToInt32(comando[2])))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("setEspecialNaoFoi", "Não foi possivel atualizar as permmissões do servidor"))
                            .WithColor(Color.DarkPurple)
                         .Build());
                    }
                }
                catch
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("setEspecialErro", "Meu caro vc n digitou o cmd do jeito certo"))
                        .WithColor(Color.DarkPurple)
                     .Build());
                }
            }
            else
            {
                Servidores servidor = new Servidores(0, PrefixoServidor.ToCharArray());
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }

        public async Task send()
        {
            if ((await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {

                string[] comando = Comando;
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                string[] id_msg = msg.Split('|');

                string id = null;
                foreach (char tmp in id_msg[0])
                {
                    if (ulong.TryParse(tmp.ToString(), out ulong result))
                    {
                        id += result;
                    }
                }

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);
                if (id_msg[0][0] == 'c')
                {
                    IChannel canal = await Contexto.Client.GetChannelAsync(Convert.ToUInt64(id));

                    if (canal != null)
                    {
                        try
                        {
                            new EmbedControl().SendMessage((canal as IMessageChannel), id_msg[1]);
                            embed.WithTitle(await StringCatch.GetString("sendMsgEnviada", "Mensagem enviada parao canal: #{0}", canal.Name));
                            embed.WithFooter(await StringCatch.GetString("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetString("sendCanalErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", Contexto.User.ToString(), canal.Name));
                            embed.WithFooter(await StringCatch.GetString("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetString("sendErroCanal", "**{0}** eu não encontrei esse canal 😔", Contexto.User.ToString()));
                    }
                }
                else if (id_msg[0][0] == 's')
                {
                    IGuild servidor = await Contexto.Client.GetGuildAsync(Convert.ToUInt64(id));
                    List<IGuildChannel> canais = (await servidor.GetChannelsAsync()).ToList();
                    if (canais.Count > 0)
                    {
                        bool parar = false;
                        int i;
                        for (i = 0; i < canais.Count && !parar; i++)
                        {
                            ChannelPermissions permissoes = (Contexto.User as IGuildUser).GetPermissions(canais[i]);
                            if (permissoes.SendMessages)
                            {
                                try
                                {
                                    new EmbedControl().SendMessage((canais[i] as IMessageChannel), id_msg[1]);
                                    embed.WithTitle(await StringCatch.GetString("sendServidorEnviada", "Mensagem enviada parao canal: #{0}", (canais[i] as IMessageChannel).Name));
                                    embed.WithFooter(await StringCatch.GetString("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                    embed.WithDescription(id_msg[1]);
                                }
                                catch
                                {
                                    embed.WithColor(Color.Red);
                                    embed.WithDescription(await StringCatch.GetString("sendServerErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", Contexto.User.ToString(), (canais[i] as IMessageChannel).Name));
                                    embed.WithFooter(await StringCatch.GetString("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                }
                                parar = true;
                            }
                        }
                        if (i == canais.Count && !parar)
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetString("sendServidorSemPermissao", "O servidor não possui canais de texto cuja eu possa mandar essa mensagem senpai 😔"));
                            embed.WithFooter(await StringCatch.GetString("sendServidorServer", "Servidor: {0}", servidor.Name));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetString("sendServidorSemCanais", "O servidor não possui canais de texto senpai 😔"));
                        embed.WithFooter(await StringCatch.GetString("sendServidorServer", "Servidor: {0}", servidor.Name));
                    }
                }
                else if (id_msg[0][0] == 'u')
                {
                    IUser user = await Contexto.Client.GetUserAsync(Convert.ToUInt64(id));

                    if (user != null)
                    {
                        try
                        {
                            new EmbedControl().SendMessage(await user.GetOrCreateDMChannelAsync(), id_msg[1]);
                            embed.WithTitle(await StringCatch.GetString("sendMsgEnviada", "Mensagem enviada para: {0}", user));
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetString("sendPvBloqueado", "**{0}** o privado da gasosa: {1} esta bloqueado 😔", Contexto.User.ToString(), user.Mention.ToString()));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetString("sendErroUsuario", "**{0}** eu não encontrei essa gasosa 😔", Contexto.User.ToString()));
                    }

                }
                else
                {
                    embed.WithColor(Color.Red);
                    embed.WithTitle(await StringCatch.GetString("sendErro", "O meu caro isso n eh uma das opções"));
                    embed.WithDescription(await StringCatch.GetString("sendErroOpcs", "`c`: Enviar no canal que tem o id q vc pegou; \n`s`: Envia em algum canal do servidor com o id q vc mandou; \n`u`: Envia pro usuario com o id q vc mandou."));
                    embed.AddField(await StringCatch.GetString("usoCmd", "Uso do Comando:"), await StringCatch.GetString("usoSend", "`{0}send opc | msg`", PrefixoServidor));
                    embed.AddField(await StringCatch.GetString("exemploCmd", "Exemplo:"), await StringCatch.GetString("exemploSend", "`{0}send c 588997126126698497 | para de salva print gay`", PrefixoServidor));
                }

                await Contexto.Channel.SendMessageAsync(embed: embed.Build());

            }
            else
            {
                Servidores servidor = new Servidores(0, PrefixoServidor.ToCharArray());
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }

        public async Task setadm()
        {
            Tuple<bool, PermissoesAdms> perms = await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id));
            if (perms.Item1 && perms.Item2 == PermissoesAdms.Donas)
            {
                string[] comando = Comando;

                string id = null;
                foreach (char tmp in comando[1])
                {
                    if (ulong.TryParse(tmp.ToString(), out ulong result))
                    {
                        id += result;
                    }
                }

                IUser user = await Contexto.Client.GetUserAsync(Convert.ToUInt64(id));
                if (user != null)
                {
                    PermissoesAdms perm = (PermissoesAdms)Convert.ToInt32(comando[2]);
                    await new AdmsDAO().SetAdmAsync(new Adms(new Usuarios(Convert.ToUInt64(user.Id))).SetPerms(perm));

                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("setadmOk", "**{0}** o usuario: ``{1}`` ganhou a permissão: ``{2}``", Contexto.User.ToString(), user.ToString(), perm))
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetString("setadmSemUsuario", "meu querido n achei essa pessoa"))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                Servidores servidor = new Servidores(0, PrefixoServidor.ToCharArray());
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }
    }
}

