using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading;

namespace Bot.Comandos
{
    public class Moderacao : Utility
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
        private void moderacao(int tipo, CommandContext context, object[] args) // piuwiiiiii
        {
            if (!context.IsPrivate)
            {
                new Thread(() =>
                {
                    string[] comando = (string[])args[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                    if (getUser.Item1 != null)
                    {
                        SocketGuildUser user = getUser.Item1 as SocketGuildUser;
                        SocketGuildUser author = context.User as SocketGuildUser;
                        SocketGuildUser bot = context.Guild.GetUserAsync(context.Client.CurrentUser.Id).GetAwaiter().GetResult() as SocketGuildUser;

                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithColor(Color.Red);

                        bool perm = false, botPerm = false;
                        switch (tipo)
                        {
                            case 1:
                                perm = author.GuildPermissions.KickMembers;
                                botPerm = bot.GuildPermissions.KickMembers;
                                embed.WithDescription($"**{context.User}** você não pode expulsar esse usuario");
                                break;
                            case 2:
                            case 3:
                                perm = author.GuildPermissions.BanMembers;
                                botPerm = bot.GuildPermissions.BanMembers;
                                embed.WithDescription($"**{context.User}** você não pode banir esse usuario");
                                break;
                        }

                        if (author.Hierarchy > user.Hierarchy && user != bot && bot.Hierarchy > user.Hierarchy && perm && botPerm)
                        {
                            string motivo = getUser.Item2;
                            if (motivo.Length > 1024)
                            {
                                motivo = motivo.Substring(0, 1024);
                            }

                            IDMChannel privado = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();

                            EmbedBuilder embedo = new EmbedBuilder();
                            embedo.WithColor(Color.DarkPurple);

                            switch (tipo)
                            {
                                case 1:
                                    if(motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi expulso do servidor: **{context.Guild.Name}**")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User}";
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi expulso do servidor: **{context.Guild.Name}**")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.KickAsync(motivo);

                                    embedo.WithDescription($"**{context.User}** o membro {user.Mention} foi expulso do servidor");
                                    context.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 2:
                                    if (motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi banido do servidor: **{context.Guild.Name}**")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User}";
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi banido do servidor: **{context.Guild.Name}**")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.BanAsync(7, motivo);

                                    embedo.WithDescription($"**{context.User}** o membro {user.Mention} foi banido do servidor");
                                    context.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 3:
                                    if (motivo == "")
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi banido temporariamente do servidor: **{context.Guild.Name}**")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User}";
                                    }
                                    else
                                    {
                                        privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"você foi banido temporariamente do servidor: **{context.Guild.Name}**")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Moderador: {context.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    user.BanAsync(7, motivo);
                                    context.Guild.RemoveBanAsync(user);

                                    embedo.WithDescription($"**{context.User}** o membro {user.Mention} foi banido temporariamente do servidor");
                                    context.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                            }

                        }
                        else
                        {
                            context.Channel.SendMessageAsync(embed: embed.Build());
                        }
                    }
                    else
                    {
                        EmbedBuilder usoEmbed = new EmbedBuilder();
                        usoEmbed.WithColor(Color.Red);
                        usoEmbed.WithDescription($"**{context.User}** você precisa mencionar um membro");

                        switch (tipo)
                        {
                            case 1:
                                usoEmbed.AddField("Usos do Comando: ", $"`{(string)args[0]}kick @membro motivo`\n`{(string)args[0]}kick <id membro> motivo`\n");
                                usoEmbed.AddField("Exemplos: ", $"`{(string)args[0]}kick @Takasaki#7072 abre o servidor`\n`{(string)args[0]}kick 274289097689006080 abre o servidor`");
                                break;
                            case 2:
                                usoEmbed.AddField("Usos do Comando: ", $"`{(string)args[0]}ban @membro motivo`\n`{(string)args[0]}ban <id membro> motivo`");
                                usoEmbed.AddField("Exemplos: ", $"`{(string)args[0]}ban @Thhrag#2527 vai pra escola`\n`{(string)args[0]}ban 240860729027198977 vai pra escola`");
                                break;
                            case 3:
                                usoEmbed.AddField("Usos do Comando: ", $"`{(string)args[0]}softban @membro motivo`\n`{(string)args[0]}softban <id membro> motivo`");
                                usoEmbed.AddField("Exemplos: ", $"`{(string)args[0]}softban @Sakurako Oomuro#5964 muito trap larissinha`\n`{(string)args[0]}softban 234097420898664448 muito trap larissinha`");
                                break;
                        }

                        context.Channel.SendMessageAsync(embed: usoEmbed.Build());
                    }

                }).Start();

            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("você so pode usar esse comando em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void kick(CommandContext context, object[] args)
        {
            moderacao(1, context, args); // la vai ele o trenzinho
        }
        public void ban(CommandContext context, object[] args)
        {
            moderacao(2, context, args); //¯\_(ツ)_/¯
        }
        public void softban(CommandContext context, object[] args)
        {
            moderacao(3, context, args); //¯\_(ツ)_/¯
        }
    }
}

//the game


