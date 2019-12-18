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
        private bool Adm = false;
        private PermissoesAdms Permissoes = PermissoesAdms.Nada;

        public Owner(CommandContext contexto, params object[] args) : base(contexto, args)
        {
            VerificarOwner().Wait();
        }

        private async Task VerificarOwner()
        {
            Tuple<bool, PermissoesAdms> Perms = await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id));
            if (Perms.Item1)
            {
                Adm = true;
                Permissoes = Perms.Item2; 
            }
        }

        public async Task ping()
        {
            if (Adm)
            {
                DiscordShardedClient client = Contexto.Client as DiscordShardedClient;

                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription($"Meu ping é {client.Latency} 🏓") //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                    .Build());

            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task setespecial()
        {
            if (Adm)
            {
                try
                {
                    string[] comando = Comando;
                    Servidores servidor = new Servidores(Convert.ToUInt64(comando[1]), (PermissoesServidores)Convert.ToInt32(comando[2]));

                    if (await new ServidoresDAO().SetEspecialAsync(servidor))
                    {
                        IGuild servi = Contexto.Client.GetGuildAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult();
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"O servidor `{servi.Name}` ganhou a permissão `{(PermissoesServidores)Convert.ToInt32(comando[2])}`.")
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        await Erro.EnviarErroAsync("não foi possivel atualizar as permmissões do servidor.");
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
                        .WithDescription("Meu caro, você não usou o comando do jeito certo 😔")
                        .AddField("Uso do Comando: ", $"`{PrefixoServidor}setespecial <id servidor> <tipo>`")
                        .AddField("Exemplo: ", $"`{PrefixoServidor}setespecial 556580866198077451 1`")
                        .AddField("Tipos: ", opcs)
                        .WithColor(Color.DarkPurple)
                     .Build());
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task send()
        {
            if (Adm)
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
                            embed.WithTitle($"Mensagem enviada para o canal `#{canal.Name}`.");
                            embed.WithFooter($"Servidor: {(canal as ITextChannel).Guild.Name}");
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription($"**{Contexto.User}**, eu não consegui enviar a mensagem no canal `#{canal.Name}` 😔");
                            embed.WithFooter($"Servidor: {(canal as ITextChannel).Guild.Name}");
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription($"**{Contexto.User}**, eu não encontrei esse canal 😔");
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
                                    embed.WithTitle($"Mensagem enviada para o canal `#{(canais[i] as IMessageChannel).Name}`.");
                                    embed.WithFooter($"Servidor: {(canais[i] as ITextChannel).Guild.Name}");
                                    embed.WithDescription(id_msg[1]);
                                }
                                catch
                                {
                                    embed.WithColor(Color.Red);
                                    embed.WithDescription($"**{Contexto.User}**, eu não consegui enviar a mensagem no canal `#{(canais[i] as IMessageChannel).Name}` 😔");
                                    embed.WithFooter($"Servidor: {(canais[i] as ITextChannel).Guild.Name}");
                                }
                                parar = true;
                            }
                        }
                        if (i == canais.Count && !parar)
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription("O servidor não possui canais de texto cuja eu possa mandar essa mensagem 😔");
                            embed.WithFooter($"Servidor: {servidor.Name}");
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription("O servidor não possui canais de texto 😔");
                        embed.WithFooter($"Servidor: {servidor.Name}");
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
                            embed.WithTitle($"Mensagem enviada para {user}.");
                            embed.WithDescription(id_msg[1]);
                        }
                        catch
                        {
                            embed.WithColor(Color.Red);
                            embed.WithDescription($"**{Contexto.User}**, o privado do(a) {user.Mention} está bloqueado 😔");
                        }
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription($"**{Contexto.User}**, eu não encontrei esse usuário 😔");
                    }

                }
                else
                {
                    embed.WithColor(Color.Red);
                    embed.WithTitle("Opção inválida");
                    embed.WithDescription("`c`: Envia no canal que tem o ID que você mandou; \n`s`: Envia em algum canal do servidor com o ID que você mandou; \n`u`: Envia para o usuário com o ID que você mandou.");
                    embed.AddField("Uso do Comando:", $"`{PrefixoServidor}send opc | msg`");
                    embed.AddField("Exemplo:", $"`{PrefixoServidor}send c 588997126126698497 | para de salva print gay`");
                }

                await Contexto.Channel.SendMessageAsync(embed: embed.Build());

            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task setadm()
        {
            if (Adm && Permissoes == PermissoesAdms.Donas)
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
                                .WithDescription($"**{Contexto.User}**, o usuário: ``{user}`` ganhou a permissão ``{perm}``.")
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
                                .WithDescription("Essa permissão não foi encontrada.")
                                .AddField("Tipos: ", opcs)
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
                            .WithDescription("Não encontrei esse usuário.")
                            .AddField("Usos do Comando: ", $"`{PrefixoServidor}setadm @pessoa <tipo>`\n`{PrefixoServidor}ban <id membro> <setadm>`")
                            .AddField("Exemplos: ", $"`{PrefixoServidor}setadm @Kud#4464 1`\n`{PrefixoServidor}ban 333313177129582594 1`")
                            .AddField("Tipos: ", opcs)
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}

