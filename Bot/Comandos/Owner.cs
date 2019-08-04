using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using static Bot.DataBase.MainDB.Modelos.Adms;
using static Bot.DataBase.MainDB.Modelos.Servidores;
using System.Linq;
using System.Collections.Generic;

namespace Bot.Comandos
{
    public class Owner : Nsfw
    {
        public void ping(CommandContext context, object[] args)
        {
            Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
            Adms adm = new Adms(usuario);

            if (new AdmsDAO().GetAdm(ref adm))
            {
                if (adm.permissoes == PermissoesAdm.Donas)
                {
                    DiscordSocketClient client = context.Client as DiscordSocketClient;

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(StringCatch.GetString("respostaPing", "Meu ping é {0}", client.Latency)) //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                        .Build());
                }
            }
        }

        public void setespecial(CommandContext context, object[] args)
        {
            Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
            Adms adm = new Adms(usuario);

            if (new AdmsDAO().GetAdm(ref adm))
            {
                if (adm.permissoes == PermissoesAdm.Donas)
                {
                    try
                    {
                        string[] comando = (string[])args[1];
                        Servidores servidor = new Servidores(Convert.ToUInt64(comando[1]));
                        servidor.SetPermissao((Permissoes)Convert.ToInt32(comando[2]));

                        if (new ServidoresDAO().SetEspecial(servidor))
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("setEspecialSetado", "A permissão do seridor foi setada yay"))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                        else
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(StringCatch.GetString("setEspecialNaoFoi", "Não foi possivel atualizar as permmissões do servidor"))
                                .WithColor(Color.DarkPurple)
                             .Build());
                        }
                    }
                    catch
                    {
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(StringCatch.GetString("setEspecialErro", "Meu caro vc n digitou o cmd do jeito certo"))
                            .WithColor(Color.DarkPurple)
                         .Build());
                    }
                }
            }
        }

        public void send(CommandContext context, object[] args)
        {
            Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
            Adms adm = new Adms(usuario);

            if (new AdmsDAO().GetAdm(ref adm))
            {
                if (adm.permissoes == PermissoesAdm.Donas)
                {
                    string[] comando = (string[])args[1];
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
                        IChannel canal = context.Client.GetChannelAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();

                        if (canal != null)
                        {
                            try
                            {
                                new EmbedControl().SendMessage((canal as IMessageChannel), id_msg[1]);
                                embed.WithTitle(StringCatch.GetString("sendMsgEnviada", "Mensagem enviada parao canal: #{0}", canal.Name));
                                embed.WithFooter(StringCatch.GetString("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                                embed.WithDescription(id_msg[1]);
                            }
                            catch
                            {
                                embed.WithColor(Color.Red);
                                embed.WithDescription(StringCatch.GetString("sendCanalErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", context.User.ToString(), canal.Name));
                                embed.WithFooter(StringCatch.GetString("sendMsgServidor", "Servidor: {0}", (canal as ITextChannel).Guild.Name));
                            }
                        }
                        else
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(StringCatch.GetString("sendErroCanal", "**{0}** eu não encontrei esse canal 😔", context.User.ToString()));
                        }
                    }
                    else if (id_msg[0][0] == 's')
                    {
                        IGuild servidor = context.Client.GetGuildAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();
                        List<IGuildChannel> canais = servidor.GetChannelsAsync().GetAwaiter().GetResult().ToList();
                        if(canais.Count > 0)
                        {
                            bool parar = false;
                            int i;
                            for (i = 0; i < canais.Count && !parar; i++)
                            {
                                ChannelPermissions permissoes = (context.User as IGuildUser).GetPermissions(canais[i]);
                                if (permissoes.SendMessages)
                                {
                                    try
                                    {
                                        new EmbedControl().SendMessage((canais[i] as IMessageChannel), id_msg[1]);
                                        embed.WithTitle(StringCatch.GetString("sendServidorEnviada", "Mensagem enviada parao canal: #{0}", (canais[i] as IMessageChannel).Name));
                                        embed.WithFooter(StringCatch.GetString("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                        embed.WithDescription(id_msg[1]);
                                    }
                                    catch
                                    {
                                        embed.WithColor(Color.Red);
                                        embed.WithDescription(StringCatch.GetString("sendServerErro", "**{0}** eu não consegui enviar a msg no canal: #{0} 😔", context.User.ToString(), (canais[i] as IMessageChannel).Name));
                                        embed.WithFooter(StringCatch.GetString("sendServidorServer", "Servidor: {0}", (canais[i] as ITextChannel).Guild.Name));
                                    }
                                    parar = true;
                                }
                            }
                            if(i == canais.Count && !parar)
                            {
                                embed.WithColor(Color.Red);
                                embed.WithDescription(StringCatch.GetString("sendServidorSemPermissao", "O servidor não possui canais de texto cuja eu possa mandar essa mensagem senpai 😔"));
                                embed.WithFooter(StringCatch.GetString("sendServidorServer", "Servidor: {0}", servidor.Name));
                            }
                        } 
                        else
                        { 
                            embed.WithColor(Color.Red);
                            embed.WithDescription(StringCatch.GetString("sendServidorSemCanais", "O servidor não possui canais de texto senpai 😔"));
                            embed.WithFooter(StringCatch.GetString("sendServidorServer", "Servidor: {0}", servidor.Name));
                        }
                    }
                    else if (id_msg[0][0] == 'u')
                    {
                        IUser user = context.Client.GetUserAsync(Convert.ToUInt64(id)).GetAwaiter().GetResult();

                        if (user != null)
                        {
                            try
                            {
                                new EmbedControl().SendMessage(user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult(), id_msg[1]);
                                embed.WithTitle(StringCatch.GetString("sendMsgEnviada", "Mensagem enviada para: {0}", user));
                                embed.WithDescription(id_msg[1]);
                            }
                            catch
                            {
                                embed.WithColor(Color.Red);
                                embed.WithDescription(StringCatch.GetString("sendPvBloqueado", "**{0}** o privado da gasosa: {1} esta bloqueado 😔", context.User.ToString(), user.Mention.ToString()));
                            }
                        }
                        else
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription(StringCatch.GetString("sendErroUsuario", "**{0}** eu não encontrei essa gasosa 😔", context.User.ToString()));
                        }

                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithTitle(StringCatch.GetString("sendErro", "O meu caro isso n eh uma das opções"));
                        embed.WithDescription(StringCatch.GetString("sendErroOpcs", "`c`: Enviar no canal que tem o id q vc pegou; \n`s`: Envia em algum canal do servidor com o id q vc mandou; \n`u`: Envia pro usuario com o id q vc mandou."));
                        embed.AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoSend", "`{0}send opc | msg`", (string)args[0]));
                        embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("exemploSend", "`{0}send c 588997126126698497 | para de salva print gay`", (string)args[0]));
                    }

                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
        }
    }
}

