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

        private enum TipoDeModeracao
        {
            kick = 0,
            ban = 1,
            softban = 2,
            semperm = 3
        }

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



        private async Task GerenciadorModeracao(TipoDeModeracao tipo)
        {
            if (Contexto.IsPrivate)
            {
                await Erro.EnviarErroAsync("desculpe mas esse comando só funciona em servidores");
                return;
            }


            if (Comando.Length < 2)
            {
                await Erro.EnviarErroAsync("você precisa me informar quem você quer punir", new DadosErro("@membro motivo", "@Yummi#2728 fala mal da joaninha não!"));
                return;
            }

            SocketGuildUser autorComando = Contexto.User as SocketGuildUser;
            SocketGuildUser bot = await Contexto.Guild.GetUserAsync(Contexto.Client.CurrentUser.Id) as SocketGuildUser;

            TipoDeModeracao botPerm = (bot.GuildPermissions.BanMembers) ? TipoDeModeracao.ban : (bot.GuildPermissions.KickMembers) ? TipoDeModeracao.kick : TipoDeModeracao.semperm;

            if (botPerm == TipoDeModeracao.semperm)
            {
                await Erro.EnviarErroAsync("sinto muito mas não possuo permissão para realizar essa tarefa");
                return;
            }

            TipoDeModeracao userPerm = (autorComando.GuildPermissions.BanMembers) ? TipoDeModeracao.ban : (autorComando.GuildPermissions.KickMembers) ? TipoDeModeracao.kick : TipoDeModeracao.semperm;

            if (userPerm == TipoDeModeracao.semperm)
            {
                await Erro.EnviarErroAsync("você não possui permissão para punir alguem desse servidor");
                return;
            }

            Tuple<IUser, string> userExtensionResult = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), string.Join(" ", Comando, 1, (Comando.Length - 1)));

            if (userExtensionResult.Item1 == null)
            {
                await Erro.EnviarErroAsync("desculpe mas não conseguir achar o usuario que você queria punir", new DadosErro("@membro motivo", "@LuckShiba#0001 vai trabalhar vagabundo!!!"));
                return;
            }

            SocketGuildUser usuarioMensionado = userExtensionResult.Item1 as SocketGuildUser;


            if (autorComando.Hierarchy <= usuarioMensionado.Hierarchy || usuarioMensionado == bot || bot.Hierarchy <= usuarioMensionado.Hierarchy)
            {
                await Erro.EnviarErroAsync("desculpe mas não posso punir alguem que tenha cargo mais alto");
                return;
            }

            string msgMotivo = userExtensionResult.Item2;
            if (msgMotivo.Length > 509)
            {
                msgMotivo = msgMotivo.Substring(0, 509) + "...";
            }

            IDMChannel privadoUsuario = await usuarioMensionado.GetOrCreateDMChannelAsync();

            string tipoModeracao = "";
            switch (tipo)
            {
                case TipoDeModeracao.kick:
                    tipoModeracao = "expulso";
                    break;
                case TipoDeModeracao.ban:
                    tipoModeracao = "banido";
                    break;
                case TipoDeModeracao.softban:
                    tipoModeracao = "removido";
                    break;
            }

            EmbedBuilder embedPrivado = new EmbedBuilder();
            embedPrivado.WithTitle("**Buuuu Buuuu Desuaaaa!!!!!**");
            embedPrivado.WithColor(Color.DarkPurple);
            embedPrivado.WithImageUrl("https://i.imgur.com/bwifre6.jpg");
            embedPrivado.WithDescription(string.Format("Você foi {0} do servidor **{1}**", tipoModeracao, Contexto.Guild.Name));

            if (!string.IsNullOrEmpty(msgMotivo))
            {
                embedPrivado.AddField("Motivo:", msgMotivo);
            }

            embedPrivado.AddField("Responsavel:", Contexto.User.ToString());

            try
            {
                await privadoUsuario.SendMessageAsync(embed: embedPrivado.Build());
            }
            catch (Exception e)
            {
                await LogEmiter.EnviarLogAsync(e);
            }
            await Task.Delay(1000);
            msgMotivo = string.Format("Responsavel: {0} {1}", Contexto.User.ToString(), (!string.IsNullOrEmpty(msgMotivo)) ? "| Motivo: {msgMotivo}" : "");
            switch (tipo)
            {
                case TipoDeModeracao.ban:
                    await usuarioMensionado.BanAsync(7, msgMotivo);
                    break;
                case TipoDeModeracao.kick:
                    await usuarioMensionado.KickAsync(msgMotivo);
                    break;
                case TipoDeModeracao.softban:
                    await usuarioMensionado.BanAsync(7, msgMotivo);
                    await Contexto.Guild.RemoveBanAsync(usuarioMensionado);
                    break;
            }

            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithDescription($"Prontinhooo o {usuarioMensionado} foi {tipoModeracao} do servidor 😀")
                .WithColor(Color.Green)
                .Build());
        }

        

         

        public async Task kick()
        {
            await GerenciadorModeracao(TipoDeModeracao.kick);
        }
        public async Task ban()
        {
            await GerenciadorModeracao(TipoDeModeracao.ban); 
        }
        public async Task softban()
        {
            await GerenciadorModeracao(TipoDeModeracao.softban);
        }
    }
}




