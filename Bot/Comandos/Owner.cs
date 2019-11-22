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
        
        public Owner(CommandContext contexto, params object[] args) : base(contexto, args)
        {

        }

        public async Task ping()
        {
            if ((await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                DiscordShardedClient client = Contexto.Client as DiscordShardedClient;

                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(await StringCatch.GetStringAsync("respostaPing", "Meu ping é {0} 🏓", client.Latency)) //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                    .Build());

            }
            else
            {
                Servidores servidor = new Servidores(0, PrefixoServidor.ToCharArray());
                await new Ajuda(Contexto, PrefixoServidor, Comando, Erro).MessageEventExceptions(new NullReferenceException(), servidor);
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
                                .WithDescription(await StringCatch.GetStringAsync("setEspecialSetado", "O servidor: `{0}` ganhou a permissão: `{1}`", servi.Name, (PermissoesServidores)Convert.ToInt32(comando[2])))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("setEspecialNaoFoi", "Não foi possivel atualizar as permmissões do servidor"))
                            .WithColor(Color.DarkPurple)
                         .Build());
                    }
                }
                catch
                {
                    string opcs = "";
                    for(int i = 0; i <= (int)PermissoesServidores.LolisEdition; i++)
                    {
                        PermissoesServidores perms = (PermissoesServidores)i;
                        opcs += $"{i} =-= {perms.ToString()}\n";
                    }

                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("setEspecialErro", "Meu caro vc n digitou o cmd do jeito certo"))
                        .AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando: "), await StringCatch.GetStringAsync("setEspecialUso", "`{0}setespecial <id servidor> <tipo>`", PrefixoServidor))
                        .AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo: "), await StringCatch.GetStringAsync("setEspecialExemplo", "`{0}setespecial 556580866198077451 1`", PrefixoServidor))
                        .AddField(await StringCatch.GetStringAsync("setespecialTiposTitle", "Tipos: "), await StringCatch.GetStringAsync("setespecialTipos", "{0}", opcs))
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
                            embed.WithTitle(await StringCatch.GetStringAsync("sendMsgEnviada", "Mensagem enviada parao canal: #{0}", canal.Name));
                            embed.WithFooter(await StringCatch.GetStringAsync("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetStringAsync("sendCanalErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", Contexto.User.ToString(), canal.Name));
                            embed.WithFooter(await StringCatch.GetStringAsync("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("sendErroCanal", "**{0}** eu não encontrei esse canal 😔", Contexto.User.ToString()));
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
                                    embed.WithTitle(await StringCatch.GetStringAsync("sendServidorEnviada", "Mensagem enviada parao canal: #{0}", (canais[i] as IMessageChannel).Name));
                                    embed.WithFooter(await StringCatch.GetStringAsync("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                    embed.WithDescription(id_msg[1]);
                                }
                                catch
                                {
                                    embed.WithColor(Color.Red);
                                    embed.WithDescription(await StringCatch.GetStringAsync("sendServerErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", Contexto.User.ToString(), (canais[i] as IMessageChannel).Name));
                                    embed.WithFooter(await StringCatch.GetStringAsync("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                }
                                parar = true;
                            }
                        }
                        if (i == canais.Count && !parar)
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetStringAsync("sendServidorSemPermissao", "O servidor não possui canais de texto cuja eu possa mandar essa mensagem senpai 😔"));
                            embed.WithFooter(await StringCatch.GetStringAsync("sendServidorServer", "Servidor: {0}", servidor.Name));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("sendServidorSemCanais", "O servidor não possui canais de texto senpai 😔"));
                        embed.WithFooter(await StringCatch.GetStringAsync("sendServidorServer", "Servidor: {0}", servidor.Name));
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
                            embed.WithTitle(await StringCatch.GetStringAsync("sendMsgEnviada", "Mensagem enviada para: {0}", user));
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(await StringCatch.GetStringAsync("sendPvBloqueado", "**{0}** o privado da gasosa: {1} esta bloqueado 😔", Contexto.User.ToString(), user.Mention.ToString()));
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("sendErroUsuario", "**{0}** eu não encontrei essa gasosa 😔", Contexto.User.ToString()));
                    }

                }
                else
                {
                    embed.WithColor(Color.Red);
                    embed.WithTitle(await StringCatch.GetStringAsync("sendErro", "O meu caro isso n eh uma das opções"));
                    embed.WithDescription(await StringCatch.GetStringAsync("sendErroOpcs", "`c`: Enviar no canal que tem o id q vc pegou; \n`s`: Envia em algum canal do servidor com o id q vc mandou; \n`u`: Envia pro usuario com o id q vc mandou."));
                    embed.AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoSend", "`{0}send opc | msg`", PrefixoServidor));
                    embed.AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo:"), await StringCatch.GetStringAsync("exemploSend", "`{0}send c 588997126126698497 | para de salva print gay`", PrefixoServidor));
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
                try
                {
                    string id = null;
                    foreach (char tmp in Comando[1])
                    {
                        if (ulong.TryParse(tmp.ToString(), out ulong result))
                        {
                            id += result;
                        }
                    }

                    IUser user = await Contexto.Client.GetUserAsync(Convert.ToUInt64(id));
                    PermissoesAdms perm = (PermissoesAdms)Convert.ToInt32(Comando[2]);
                    if (perm == PermissoesAdms.Donas || perm == PermissoesAdms.Nada)
                    {
                        await new AdmsDAO().SetAdmAsync(new Adms(new Usuarios(Convert.ToUInt64(user.Id))).SetPerms(perm));

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("setadmOk", "**{0}** o usuario: ``{1}`` ganhou a permissão: ``{2}``", Contexto.User.ToString(), user.ToString(), perm))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        string opcs = "";
                        for (int i = 0; i <= (int)PermissoesAdms.Donas; i++)
                        {
                            PermissoesAdms tipos = (PermissoesAdms)i;
                            opcs += $"{i} =-= {tipos.ToString()}\n";
                        }

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("setadmSemPerm", "meu caro essa permissão não foi encontrada"))
                                .AddField(await StringCatch.GetStringAsync("setadmTipos", "Tipos: "), await StringCatch.GetStringAsync("setadmTipos2", "{0}", opcs))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                catch
                {
                    string opcs = "";
                    for (int i = 0; i <= (int)PermissoesAdms.Donas; i++)
                    {
                        PermissoesAdms tipos = (PermissoesAdms)i;
                        opcs += $"{i} =-= {tipos.ToString()}\n";
                    }

                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("setadmSemUsuario", "meu querido n achei essa pessoa"))
                            .AddField(await StringCatch.GetStringAsync("usosComando", "Usos do Comando: "), await StringCatch.GetStringAsync("setadmUsos", "`{0}setadm @pessoa <tipo>`\n`{0}ban <id membro> <setadm>`", PrefixoServidor))
                            .AddField(await StringCatch.GetStringAsync("exemplo", "Exemplos: "), await StringCatch.GetStringAsync("setadmExemplos", "`{0}setadm @Kud#4464 1`\n`{0}ban 333313177129582594 1`", PrefixoServidor))
                            .AddField(await StringCatch.GetStringAsync("setadmTiposTitle", "Tipos: "), await StringCatch.GetStringAsync("setadmTipos", "{0}", opcs))
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

