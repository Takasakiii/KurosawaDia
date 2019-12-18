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
             //aaa
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
                                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("limparchatQuantidadeInvalida", "a quantidade de mensagens informada não é um numero válido."), new DadosErro(await StringCatch.GetStringAsync("limparchatQuantidadeInvalidaArgs", "quantidade usuario"), await StringCatch.GetStringAsync("limparchatQuantidadeInvalidaExemp", "20 @Yummi#1281")));
                                }
                            }
                            else
                            {
                                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("limparchatQuantidadeInvalida", "você precisa por a quantidade de mensagens que deseja apagar."), new DadosErro(await StringCatch.GetStringAsync("limparchatQuantidadeInvalidaArgs", "quantidade usuario"), await StringCatch.GetStringAsync("limparchatQuantidadeInvalidaExemp", "20 @Yummi#1281")));
                            }
                        }
                        else
                        {
                            await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("limparchatBotPermissao", "eu não tenho permissão para apagar mensagens nesse canal 😔"));
                        }
                    }else{
                        await Erro.EnviarFaltaPermissaoAsync(await StringCatch.GetStringAsync("limparchatPermissao", "Gerenciar Mensagens"));
                    }
                }else{
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("limparchatQuantidadeInvalida", "esse comando não pode ser executado no privado."));
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
                                embed.WithDescription(await StringCatch.GetStringAsync("kickPerm", "**{0}**, você não pode expulsar esse usuário.", Contexto.User.ToString()));
                                break;
                            case 2:
                            case 3:
                                perm = author.GuildPermissions.BanMembers;
                                botPerm = bot.GuildPermissions.BanMembers;
                                embed.WithDescription(await StringCatch.GetStringAsync("banPerm", "**{0}**, você não pode banir esse usuário.", Contexto.User.ToString()));
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
                                                .WithDescription(await StringCatch.GetStringAsync("kickExpulso", "Você foi expulso do servidor **{0}**.", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("kickModerador", "Responsável: {0}", Contexto.User);
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetStringAsync("kickExpulso", "Você foi expulso do servidor: **{0}**.", Contexto.Guild.Name))
                                                .AddField(await StringCatch.GetStringAsync("kickMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("kickModeradorMotivo", "Responsável: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.KickAsync(motivo);

                                    embedo.WithDescription(await StringCatch.GetStringAsync("kickTxt", "**{0}**, o membro `{1}` foi expulso do servidor.", Contexto.User.ToString(), user.ToString()));
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 2:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetStringAsync("banBanido", "Você foi banido do servidor **{0}**.", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("banModerador", "Responsável: `{0}`", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetStringAsync("banBanido", "Você foi banido do servidor **{0}**.", Contexto.Guild.Name))
                                                .AddField(await StringCatch.GetStringAsync("banMotivo", "Motivo:"), motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("banModeradorMotivo", "Responsável: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);

                                    embedo.WithDescription(await StringCatch.GetStringAsync("banMembroBanido", "**{0}**, o membro `{1}` foi banido do servidor.", Contexto.User.ToString(), user.ToString()));
                                    await Contexto.Channel.SendMessageAsync(embed: embedo.Build());
                                    break;
                                case 3:
                                    if (motivo == "")
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetStringAsync("softbanExpulso", "Você foi expulso do servidor **{0}** e suas mensagens foram apagadas.", Contexto.Guild.Name))
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("softbanModerador", "Responsável: {0}", Contexto.User.ToString());
                                    }
                                    else
                                    {
                                        await privado.SendMessageAsync(embed: new EmbedBuilder()
                                                .WithDescription(await StringCatch.GetStringAsync("softbanExpulso", "Você foi expulso do servidor **{0}** e suas mensagens foram apagadas.", Contexto.Guild.Name))
                                                .AddField("Motivo:", motivo)
                                                .WithColor(Color.DarkPurple)
                                            .Build());
                                        motivo = await StringCatch.GetStringAsync("softbanModeradorMotivo", "Responsável: {0} || Motivo: {1}", Contexto.User.ToString(), motivo);
                                    }

                                    if (motivo.Length > 512)
                                    {
                                        motivo = motivo.Substring(0, 512);
                                    }

                                    Thread.Sleep(1000);

                                    await user.BanAsync(7, motivo);
                                    await Contexto.Guild.RemoveBanAsync(user);

                                    embedo.WithDescription(await StringCatch.GetStringAsync("softbanMembroExpulso", "**{0}**, o membro {1} foi expulso do servidor e suas mensagens fora apagadas.", Contexto.User.ToString(), user.Mention));
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
                        usoEmbed.WithDescription(await StringCatch.GetStringAsync("moderacaoMembro", "**{0}**, você precisa me informar um membro.", Contexto.User.ToString()));

                        switch (tipo)
                        {
                            case 1:
                                usoEmbed.AddField(await StringCatch.GetStringAsync("usosComando", "Usos do Comando: "), await StringCatch.GetStringAsync("kickUsos", "`{0}kick @membro motivo`\n`{0}kick <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetStringAsync("exemplo", "Exemplos: "), await StringCatch.GetStringAsync("kickExemplos", "`{0}kick @Takasaki#7072 abre o servidor`\n`{0}kick 274289097689006080 abre o servidor`", PrefixoServidor));
                                break;
                            case 2:
                                usoEmbed.AddField(await StringCatch.GetStringAsync("usosComando", "Usos do Comando: "), await StringCatch.GetStringAsync("banUsos", "`{0}ban @membro motivo`\n`{0}ban <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetStringAsync("exemplo", "Exemplos: "), await StringCatch.GetStringAsync("banExemplos", "`{0}ban @Thhrag#2527 vai pra escola`\n`{0}ban 240860729027198977 vai pra escola`",PrefixoServidor));
                                break;
                            case 3:
                                usoEmbed.AddField(await StringCatch.GetStringAsync("usosComando", "Usos do Comando: "), await StringCatch.GetStringAsync("softbanUsos", "`{0}softban @membro motivo`\n`{0}softban <id membro> motivo`", PrefixoServidor));
                                usoEmbed.AddField(await StringCatch.GetStringAsync("exemplo", "Exemplos: "), await StringCatch.GetStringAsync("softbanExemplos", "`{0}softban @Sakurako Oomuro#5964 muito trap larissinha`\n`{0}softban 234097420898664448 muito trap larissinha`", PrefixoServidor));
                                break;
                        }

                        await Contexto.Channel.SendMessageAsync(embed: usoEmbed.Build());
                    }

                }).Start();

            }
            else
            {
                await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription(await StringCatch.GetStringAsync("moderacaoDm", "você só pode usar esse comando em servidores."))
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




