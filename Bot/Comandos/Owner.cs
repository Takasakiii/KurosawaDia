using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using static Bot.DataBase.MainDB.Modelos.Adms;
using static Bot.DataBase.MainDB.Modelos.Servidores;

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

                        if(new ServidoresDAO().SetEspecial(servidor))
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

                    if(id_msg[0].StartsWith("c "))
                    {
                        IChannel canal = context.Client.GetChannelAsync(Convert.ToUInt64(comando[2])).GetAwaiter().GetResult();

                        try
                        {
                            var embedo = new EmbedControl().CriarEmbedJson(id_msg[1]);
                            (canal as IMessageChannel).SendMessageAsync(embedo.Item1, embed: embedo.Item2);
                        }
                        catch
                        {
                            (canal as IMessageChannel).SendMessageAsync(id_msg[1]);
                        }               
                        
                    } else if (id_msg[0].StartsWith("s "))
                    {
                        IGuild servidor = context.Client.GetGuildAsync(Convert.ToUInt64(comando[2])).GetAwaiter().GetResult();
                        System.Collections.Generic.List<IGuildChannel> canais = servidor.GetChannelsAsync().GetAwaiter().GetResult().ToList();
                        bool parar = false;
                        for(int i =0; i < canais.Count && !parar; i++)
                        {
                            ChannelPermissions permissoes = (context.User as IGuildUser).GetPermissions(canais[i]);
                            if (permissoes.SendMessages)
                            {
                                try
                                {
                                    var embedo = new EmbedControl().CriarEmbedJson(id_msg[1]);
                                    (canais[i] as IMessageChannel).SendMessageAsync(embedo.Item1, embed: embedo.Item2);
                                }
                                catch
                                {
                                    (canais[i] as IMessageChannel).SendMessageAsync(id_msg[1]);
                                }
                                parar = true;
                            }
                        }
                        
                        
                    }
                }
            }
        }
    }
}
