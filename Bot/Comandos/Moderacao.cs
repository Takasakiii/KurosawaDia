using System.Collections.Generic;
using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using System;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using static Bot.Extensions.ErrorExtension;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Comandos
{
    public class Moderacao : GenericModule
    {
        public Moderacao(CommandContext contexto, params object[] args) : base(contexto, args)
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
             /*
             * 
             */
            
        }


        public Task limparchat(){
            new Thread(async() => {
                if(!Contexto.IsPrivate){
                    SocketGuildUser author = Contexto.User as SocketGuildUser;
                    if(author.GuildPermissions.ManageMessages){
                        if((await Contexto.Guild.GetCurrentUserAsync()).GetPermissions(Contexto.Channel as SocketGuildChannel).ManageMessages) {
                            if(Comando.Length > 1){
                                Tuple <IUser, string> resUser = null;
                                if(Comando.Length > 2){
                                    resUser = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), Comando[2]);
                                }
                                try{
                                    uint quantidade = Convert.ToUInt32(Comando[1]);
                                    List<IMessage> mensagens = null;
                                    if(resUser != null){
                                        List<IMessage> construtor = new List<IMessage>();
                                        IMessage msgRef = Contexto.Message;
                                        while(construtor.Count < quantidade){
                                            List<IMessage> temp = (await Contexto.Channel.GetMessagesAsync(msgRef, Direction.Before).FlattenAsync()).ToList();
                                            temp = temp.FindAll(x => x.Author == resUser.Item1);
                                            if (temp.Count == 0) break;
                                            msgRef = temp[temp.Count - 1];
                                            construtor.AddRange(temp);
                                        }
                                        mensagens = construtor;
                                    }else {
                                        mensagens = (await Contexto.Channel.GetMessagesAsync(limit: Convert.ToInt32(quantidade)).FlattenAsync()).ToList();
                                    }
                                    await Contexto.Message.DeleteAsync();
                                    await((ITextChannel)Contexto.Channel).DeleteMessagesAsync(mensagens);
                                }catch{
                                    await Erro.EnviarErroAsync("a quantidade de mensagens informada não é um numero válido.", new DadosErro("quantidade usuario", "20 @Yummi#1281"));
                                }
                            }
                            else
                            {
                                await Erro.EnviarErroAsync("você precisa por a quantidade de mensagens que deseja apagar.", new DadosErro("quantidade usuario", "20 @Yummi#1281"));
                            }
                        }
                        else
                        {
                            await Erro.EnviarErroAsync("eu não tenho permissão para apagar mensagens nesse canal 😔");
                        }
                    }else{
                        await Erro.EnviarFaltaPermissaoAsync("Gerenciar Mensagens");
                    }
                }else{
                    await Erro.EnviarErroAsync("esse comando não pode ser executado no privado.");
                }
            }).Start();
            return Task.CompletedTask;
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
                                embed.WithDescription($"**{Contexto.User}**, você não pode expulsar esse usuário.");
                                break;
                            case 2:
                            case 3:
                                perm = author.GuildPermissions.BanMembers;
                                botPerm = bot.GuildPermissions.BanMembers;
                                embed.WithDescription($"**{Contexto.User}**, você não pode banir esse usuário.");
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
                                                .WithDescription($"Você foi expulso do servidor **{Contexto.Guild.Name}**.")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Responsável: {Contexto.User}";
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"Você foi expulso do servidor: **{Contexto.Guild.Name}**.")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = "Responsável: {Contexto.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.KickAsync(motivo);

                                    embedo.WithDescription($"**{Contexto.User}**, o membro `{user.Mention}` foi expulso do servidor.");
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 2:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"Você foi banido do servidor **{Contexto.Guild.Name}**.")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = $"Responsável: `{Contexto.User}`";
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"Você foi banido do servidor **{Contexto.Guild.Name}**.")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = "Responsável: {Contexto.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);

                                    embedo.WithDescription("**{Contexto.User}**, o membro {user.Mention} foi banido do servidor.");
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 3:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"Você foi expulso do servidor **{Contexto.Guild.Name}** e suas mensagens foram apagadas.")
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = "Responsável: {Contexto.User}";
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription($"Você foi expulso do servidor **{Contexto.Guild.Name}** e suas mensagens foram apagadas.")
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = "Responsável: {Contexto.User} || Motivo: {motivo}";
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);
                                    await Contexto.Guild.RemoveBanAsync(user);

                                    embedo.WithDescription($"**{Contexto.User}**, o membro {user.Mention} foi expulso do servidor e suas mensagens fora apagadas.");
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
                        usoEmbed.WithDescription($"**{Contexto.User}**, você precisa me informar um membro.");

                        switch (tipo)
                        {
                            case 1:
                                usoEmbed.AddField("Usos do Comando: ", $"`{PrefixoServidor}kick @membro motivo`\n`{PrefixoServidor}kick <id membro> motivo`");
                                usoEmbed.AddField("Exemplos: ", $"`{PrefixoServidor}kick @Takasaki#7072 abre o servidor`\n`{PrefixoServidor}kick 274289097689006080 abre o servidor`");
                                break;
                            case 2:
                                usoEmbed.AddField("Usos do Comando: ", $"`{PrefixoServidor}ban @membro motivo`\n`{PrefixoServidor}ban <id membro> motivo`");
                                usoEmbed.AddField("Exemplos: ", $"`{PrefixoServidor}ban @Thhrag#2527 vai pra escola`\n`{PrefixoServidor}ban 240860729027198977 vai pra escola`");
                                break;
                            case 3:
                                usoEmbed.AddField("Usos do Comando: ", $"`{PrefixoServidor}softban @membro motivo`\n`{PrefixoServidor}softban <id membro> motivo`");
                                usoEmbed.AddField("Exemplos: ", $"`{PrefixoServidor}softban @Sakurako Oomuro#5964 muito trap larissinha`\n`{PrefixoServidor}softban 234097420898664448 muito trap larissinha`");
                                break;
                        }

                        await Contexto.Channel.SendMessageAsync(embed: usoEmbed.Build());
                    }

                }).Start();

            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("Você só pode usar esse comando em servidores.")
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




