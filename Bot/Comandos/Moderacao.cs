using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Comandos
{
    public class Moderacao : GenericModule
    {
        public Moderacao(CommandContext contexto, string prefixo, string[] comando) : base(contexto, prefixo, comando)
        {
                /*
             *  pra que server
             *  tantos codigos
             *  se a vida
             *  não é programada
             *  e as melhores coisas
             *  não tem logica
            */
            /*
             *  Pra que serve
             *  o dinheiro 
             *  se a melhores coisas
             *  são feitas de graça
             *      - Takasaki 2019 
             *      (so vc pegar mina kkkkkkkkkkkkk ou ter amigos XD)
             */
            /* 
             * 
             * (essa eh minha filosofia não en nada você que cria o significado de tudo)
             * 
             */
             //Quem inventa é o inventor, segue o esperado pq vc não é inventor caralho
        }

        private async Task moderacao(int tipo) 
        {
            if (!Contexto.IsPrivate)
            {
                new Thread(async () =>
                {
                    string[] comando = Comando;
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg);

                    if (getUser.Item1 != null)
                    {
                        SocketGuildUser user = getUser.Item1 as SocketGuildUser;
                        SocketGuildUser author = Contexto.User as SocketGuildUser;
                        SocketGuildUser bot = await Contexto.Guild.GetUserAsync(Contexto.Client.CurrentUser.Id) as SocketGuildUser;

                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithColor(Color.Red);

                        bool perm = false, botPerm = false;
                        switch (tipo)
                        {
                            case 1:
                                perm = author.GuildPermissions.KickMembers;
                                botPerm = bot.GuildPermissions.KickMembers;
                                embed.WithDescription(await StringCatch.GetString("kickPerm", "**{0}** você não pode expulsar esse usuario", Contexto.User.ToString()));
                                break;
                            case 2:
                            case 3:
                                perm = author.GuildPermissions.BanMembers;
                                botPerm = bot.GuildPermissions.BanMembers;
                                embed.WithDescription(await StringCatch.GetString("banPerm", "**{0}** você não pode banir esse usuario", Contexto.User.ToString()));
                                break;
                        }

                        if (author.Hierarchy > user.Hierarchy && user != bot && bot.Hierarchy > user.Hierarchy && perm && botPerm)
                        {
                            string motivo = getUser.Item2;
                            if (motivo.Length > 1024)
                            {
                                motivo = motivo.Substring(0, 1024);
                            }

                            IDMChannel privado = await user.GetOrCreateDMChannelAsync();

                            EmbedBuilder embedo = new EmbedBuilder();
                            embedo.WithColor(Color.DarkPurple);

                            switch (tipo)
                            {
                                case 1:
                                    if(motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("kickExpulso", "você foi expulso do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("kickModerador", "Moderador: {0}", Contexto.User);
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("kickExpulso", "você foi expulso do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField(await StringCatch.GetString("kickMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("kickModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.KickAsync(motivo);

                                    embedo.WithDescription(await StringCatch.GetString("kickTxt", "**{0}** o membro {1} foi expulso do servidor", Contexto.User.ToString(), user.Mention));
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 2:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("banBanido", "você foi banido do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("banModerador", "Moderador: {0}", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("banBanido", "você foi banido do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField(await StringCatch.GetString("banMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("banModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);

                                    embedo.WithDescription(await StringCatch.GetString("banMembroBanido", "**{0}** o membro {1} foi banido do servidor", Contexto.User.ToString(), user.Mention));
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 3:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("softbanExpulso", "você foi banido temporariamente do servidor: **{0}**", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("softbanModerador", "Moderador: {0}", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetString("softbanExpulso", "você foi banido temporariamente do servidor: **{0}**", Contexto.Guild.Name))
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetString("softbanModeradorMotivo", "Moderador: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);
                                    await Contexto.Guild.RemoveBanAsync(user);

                                    embedo.WithDescription(await StringCatch.GetString("softbanMembroExpulso", "**{0}** o membro {1} foi banido temporariamente do servidor", Contexto.User.ToString(), user.Mention));
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                            }

                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                        }
                    }
                    else
                    {
                        EmbedBuilder usoEmbed = new EmbedBuilder();
                        usoEmbed.WithColor(Color.Red);
                        usoEmbed.WithDescription(await StringCatch.GetString("moderacaoMembro", "**{0}** você precisa mencionar um membro", Contexto.User.ToString()));

                        switch (tipo)
                        {
                            case 1:
                                usoEmbed.AddField(await StringCatch.GetString("usosComando", "Usos do Comando: "), await StringCatch.GetString("kickUsos", "`{0}kick @membro motivo`\n`{0}kick <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetString("exemplo", "Exemplos: "), await StringCatch.GetString("kickExemplos", "`{0}kick @Takasaki#7072 abre o servidor`\n`{0}kick 274289097689006080 abre o servidor`", PrefixoServidor));
                                break;
                            case 2:
                                usoEmbed.AddField(await StringCatch.GetString("usosComando", "Usos do Comando: "), await StringCatch.GetString("banUsos", "`{0}ban @membro motivo`\n`{0}ban <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetString("exemplo", "Exemplos: "), await StringCatch.GetString("banExemplos", "`{0}ban @Thhrag#2527 vai pra escola`\n`{0}ban 240860729027198977 vai pra escola`",PrefixoServidor));
                                break;
                            case 3:
                                usoEmbed.AddField(await StringCatch.GetString("usosComando", "Usos do Comando: "), await StringCatch.GetString("softbanUsos", "`{0}softban @membro motivo`\n`{0}softban <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetString("exemplo", "Exemplos: "), await StringCatch.GetString("softbanExemplos", "`{0}softban @Sakurako Oomuro#5964 muito trap larissinha`\n`{0}softban 234097420898664448 muito trap larissinha`", PrefixoServidor));
                                break;
                        }

                        await Contexto.Channel.SendMessageAsync(embed: usoEmbed.Build());
                    }

                }).Start();

            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetString("moderacaoDm", "Você so pode usar esse comando em servidores"))
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public async Task kick()
        {
            await moderacao(1); 
        }
        public async Task ban()
        {
            await moderacao(2); 
        }
        public async Task softban()
        {
            await moderacao(3);
        }
    }
}




