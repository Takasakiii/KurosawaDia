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
using static Bot.Extensions.ErrorExtension;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Comandos
{
    public class Configuracoes : GenericModule
    {
        public Configuracoes(CommandContext contexto, params object[] args) : base(contexto, args)
        { 

        }
        


        public async Task setprefix()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser userGuild = Contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.ManageGuild)
                {
                    string[] comando = Comando;
                    

                    if (comando.Length > 1)
                    {
                        string msg = comando[1];
                        IUserMessage message = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"**{Contexto.User}**, você quer mudar o prefixo?")
                                .WithFooter("Se não, apenas ignore essa mensagem.")
                                .WithColor(Color.DarkPurple)
                            .Build());

                        Emoji emoji = new Emoji("✅");
                        await message.AddReactionAsync(emoji);

                        ReactionControler reaction = new ReactionControler();
                        reaction.GetReaction(message, emoji, Contexto.User, new ReturnMethod(async () =>
                        {
                            Servidores servidor = new Servidores(Contexto.Guild.Id, msg.ToCharArray());

                            Tuple<bool, Servidores> res = await new ServidoresDAO().SetServidorPrefixAsync(servidor);
                            servidor = res.Item2;
                            await message.DeleteAsync();
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{Contexto.User.Username}**, o prefixo do servidor foi alterado de `{PrefixoServidor}` para `{new string(servidor.Prefix)}`.")
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }));
                    }
                    else
                    {
                        await Erro.EnviarErroAsync(", você precisa me falar um prefixo.", new DadosErro("<prefixo>", "!"));
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Gerenciar Servidor");
                }

            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        public async Task piconf()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser usuarioinGuild = Contexto.User as SocketGuildUser;
                if (usuarioinGuild.GuildPermissions.Administrator)
                {
                    SocketGuildUser botRepresentacao = await Contexto.Guild.GetCurrentUserAsync() as SocketGuildUser;
                    if (botRepresentacao.GuildPermissions.ManageRoles)
                    {
                        EmbedBuilder embed = new EmbedBuilder();
                        embed.WithColor(Color.DarkPurple);
                        embed.WithTitle("**Configuração dos Pontos de Interação**");
                        embed.WithDescription("Você deseja ligar os pontos de interação? (Eles servem para medir a interação dos seus membros e setar cargos automaticamente.)");
                        embed.AddField("Opções válidas:", "s - Sim / Ligar\nn - Não / Desligar");
                        IMessage pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                        SubCommandControler sub = new SubCommandControler();
                        IMessage msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                        if (msgresposta != null)
                        {
                            bool ativado;
                            double rate = 2;
                            string msg = "";
                            if (msgresposta.Content == "s" || msgresposta.Content == "n")
                            {

                                if (msgresposta.Content == "s")
                                {
                                    ativado = true;
                                    embed.WithDescription("Qual é o multiplicador de Pontos de Interação que deseja usar? (Este multiplicador determina como será medido a interação dos membros.) [Recomendamos o multiplicador 2.]");
                                    embed.Fields.Clear();
                                    embed.AddField("Opções válidas:", "Qualquer número a partir de 1.0");
                                    pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                                    sub = new SubCommandControler();
                                    msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                                    if (msgresposta != null && double.TryParse(msgresposta.Content, out rate))
                                    {
                                        if (rate > 1)
                                        {
                                            embed.WithDescription("Digite a mensagem que você quer que eu mostre quando alguém conseguir um Ponto de Interação. Se você não deseja ter uma mensagem, apenas digite `%desativar%`.");
                                            embed.Fields.Clear();
                                            embed.AddField("Opções válidas:", "Qualquer tipo de texto, podendo usar até Embeds compativeis com a Nadeko Bot e variáveis como %user% e %pontos%.");
                                            pergunta = await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                                            sub = new SubCommandControler();
                                            msgresposta = await sub.GetCommand(pergunta, Contexto.User);
                                            msg = msgresposta.Content;
                                        }
                                        else
                                        {
                                            await RotaFail();
                                        }
                                    }
                                    else
                                    {
                                        await RotaFail();
                                    }
                                }
                                else
                                {
                                    ativado = false;
                                }
                                PI pimodel = new PI(ativado, rate, (msg == "%desativar%") ? "" : msg);
                                if (await new ConfiguracoesServidorDAO().SalvarPIConfigAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), pimodel)))
                                {
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithColor(Color.Green)
                                            .WithTitle("Ok, farei tudo conforme o pedido 😃")
                                        .Build());
                                }
                                else
                                {
                                    await Erro.EnviarErroAsync("desculpe, mas houve um problema ao tentar salvar suas preferências. Se for urgente contate meus criadores que eles vão te dar todo o suporte 😔");
                                }
                            }
                            else
                            {
                                await RotaFail();
                            }
                        }
                    }
                    else
                    {
                        await Erro.EnviarFaltaPermissaoAsync("Gerenciar Cargos");
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        private async Task TimeOut()
        {
            await Erro.EnviarErroAsync("o tempo acabou 😶");
            return;
        }

        private async Task RotaFail()
        {
            await Erro.EnviarErroAsync("desculpe, mas você terá que me falar um valor dentro do \"Opções Validas\", senão eu não poderei te ajudar 😔");
        }

        public async Task welcomech()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    string id = "";
                    string[] comando = Comando;
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    foreach (char letra in msg)
                    {
                        if (ulong.TryParse(letra.ToString(), out ulong result))
                        {
                            id += result;
                        }
                    }
                    IChannel canal = null;
                    try
                    {
                        canal = await Contexto.Guild.GetChannelAsync(Convert.ToUInt64(id));
                    }
                    catch
                    {
                        canal = Contexto.Channel;
                    }

                    if (canal != null)
                    {
                        Canais canalModel = new Canais(canal.Id, new Servidores(Contexto.Guild.Id), TiposCanais.bemvindoCh, canal.Name);
                        if (await new CanaisDAO().AddChAsync(canalModel))
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{Contexto.User.Username}**, as mensagens de boas-vindas serão enviadas no canal `#{canalModel.NomeCanal}`.")
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{Contexto.User.Username}**, eu não consegui definir esse canal para mandar as boas-vindas.")
                                    .WithColor(Color.Red)
                                .Build());
                        }
                    }
                    else
                    {
                        await Erro.EnviarErroAsync("eu não encontrei esse canal no servidor.");
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        public async Task byech()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    string id = "";
                    string[] comando = Comando;
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    foreach (char letra in msg)
                    {
                        if (ulong.TryParse(letra.ToString(), out ulong result))
                        {
                            id += result;
                        }
                    }
                    IChannel canal = null;
                    try
                    {
                        canal = await Contexto.Guild.GetChannelAsync(Convert.ToUInt64(id));
                    }
                    catch
                    {
                        canal = Contexto.Channel;
                    }

                    if (canal != null)
                    {
                        Canais canalModel = new Canais(canal.Id, new Servidores(Contexto.Guild.Id), TiposCanais.sairCh, canal.Name);
                        if (await new CanaisDAO().AddChAsync(canalModel))
                        {
                            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"**{Contexto.User.Username}**, as mensagens de saída serão enviadas no canal `#{canalModel.NomeCanal}`.")
                                    .WithColor(Color.DarkPurple)
                                 .Build());
                        }
                        else
                        {
                            await Erro.EnviarErroAsync("eu não consegui definir esse canal para mandar as mensagens de saida.");
                        }
                    }
                    else
                    {
                        await Erro.EnviarErroAsync("eu não encontrei esse canal no servidor.");
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        public async Task picargo()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser userGuild = Contexto.User as SocketGuildUser;
                if (userGuild.GuildPermissions.Administrator)
                {
                    string[] comandoargs = Comando;
                    string prefix = PrefixoServidor;
                    DadosErro dadosErro = new DadosErro("<quantidade de PI requerido (se o valor for menor ou igual a 0 o mesmo será removido)> <mome do cargo>", "3 Membros`");

                    if (comandoargs.Length > 2)
                    {
                        string nomerole = string.Join(" ", comandoargs, 2, comandoargs.Length - 2);
                        List<IRole> cargos = Contexto.Guild.Roles.ToList();
                        IRole cargoSelecionado = null;
                        if (ulong.TryParse(nomerole, out ulong id))
                        {
                            cargoSelecionado = cargos.Find(x => x.Id == id);
                        }
                        else
                        {
                            cargoSelecionado = cargos.Find(x => x.Name == nomerole);
                        }

                        if (cargoSelecionado == null)
                        {
                            await Erro.EnviarErroAsync("o cargo não pôde ser encontrado. Por favor, verifique se você digitou o nome/id do cargo corretamente.");
                        }
                        else
                        {
                            if (long.TryParse(comandoargs[1], out long requisito)) {
                                Servidores servidor = new Servidores(Contexto.Guild.Id, Contexto.Guild.Name);
                                Cargos cargoCadastro = new Cargos(Cargos.Tipos_Cargos.XpRole, Convert.ToUInt64(cargoSelecionado.Id), cargoSelecionado.Name, requisito, servidor);
                                CargosDAO dao = new CargosDAO();
                                CargosDAO.Operacao operacaoRetorno = await dao.AdicionarAtualizarCargoAsync(cargoCadastro);
                                if (operacaoRetorno != CargosDAO.Operacao.Incompleta) {
                                    string operacao = (operacaoRetorno == CargosDAO.Operacao.Insert) ? "adicionado" : (operacaoRetorno == CargosDAO.Operacao.Update) ? "atualizado" : "removido";
                                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithColor(Color.Green)
                                        .WithTitle($"**{Contexto.User.Username}**, o cargo `{cargoSelecionado.Name}` foi {operacao} com sucesso 😃")
                                        .Build());
                                }
                                else {
                                    await Erro.EnviarErroAsync("desculpe, mas não consegui adicionar o cargo 😔", dadosErro);
                                }
                            }
                            else {
                                await Erro.EnviarErroAsync("a quantidade de PI está inválida. Por favor digite somente números inteiros.", dadosErro);
                            }
                        }
                    }
                    else
                    {
                        await Erro.EnviarErroAsync("você precisa adicionar os parâmetros do comando.", dadosErro);
                    }
                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }


        }

        public async Task welcomemsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                               .WithTitle("Configurar a mensagem de boas-vindas.")
                               .WithDescription("Você quer ligar a mensagem de boas vindas no seu servidor?")
                               .AddField("Opções válidas:", "s - Sim / Ligar\nn - Não / Desligar")
                               .WithColor(Color.DarkPurple)
                           .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        string msg = "";
                        if (msgresposta.Content == "s")
                        {
                            embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle("Configurar a mensagem de boas-vindas")
                                    .WithDescription("Digite a mensagem que você quer que eu mostre quando alguém entrar no servidor, se você não quer ter uma mensagem digite ``%desativar%``.")
                                    .AddField("Opções válidas:", "Qualquer tipo de texto, podendo usar até Embeds compatíveis com a Nadeko Bot e variáveis como %user%.")
                                    .WithColor(Color.DarkPurple)
                                .Build());

                            sub = new SubCommandControler();
                            msgresposta = await sub.GetCommand(embed, Contexto.User);

                            msg = msgresposta.Content;
                        }
                        else
                        {
                            msg = "%desativar%";
                        }
                        BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setBemvindo((msg == "%desativar%") ? "" : msg);
                        await new ConfiguracoesServidorDAO().SetWelcomeMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), vindoGoodByeMsg));

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle("Ok, farei tudo conforme o pedido 😃")
                            .Build());

                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        public async Task byemsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle("Configurar a mensagem de saida")
                                .WithDescription("Você quer ligar a mensagem de quando alguém sai do servidor?")
                                .AddField("Opções Validas:", "s - Sim / Ligar\nn - Não / Desligar")
                                .WithColor(Color.DarkPurple)
                            .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        string msg = "";
                        if (msgresposta.Content == "s")
                        {
                            embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle("Configurar a mensagem de saida")
                                    .WithDescription("Digite a mensagem que você quer que eu mostre quando alguém sai do servidor, se você não quer ter uma mensagem digite ``%desativar%``.")
                                    .AddField("Opções Validas:", "Qualquer tipo de texto, podendo usar até Embeds compatíveis com a Nadeko Bot e variáveis como %user%.")
                                    .WithColor(Color.DarkPurple)
                                .Build());

                            sub = new SubCommandControler();
                            msgresposta = await sub.GetCommand(embed, Contexto.User);

                            msg = msgresposta.Content;
                        }
                        else
                        {
                            msg = "%desativar%";
                        }
                        BemVindoGoodByeMsg vindoGoodByeMsg = new BemVindoGoodByeMsg().setSair((msg == "%desativar%") ? "" : msg);
                        await new ConfiguracoesServidorDAO().SetByeMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), vindoGoodByeMsg));

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle("Ok, farei tudo conforme o pedido 😃")
                            .Build());

                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }

        public async Task erromsg()
        {
            if (!Contexto.IsPrivate)
            {
                SocketGuildUser guildUser = Contexto.User as SocketGuildUser;
                if (guildUser.GuildPermissions.Administrator)
                {
                    IMessage embed = await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle("Configurar a mensagem de erro")
                                .WithDescription("Você quer que eu envie uma mensagem de erro quando alguém tenta usar algum comando que eu não tenho?")
                                .AddField("Opções válidas:", "s - Sim / Ligar\nn - Não / Desligar")
                                .WithColor(Color.DarkPurple)
                            .Build());

                    SubCommandControler sub = new SubCommandControler();
                    IMessage msgresposta = await sub.GetCommand(embed, Contexto.User);

                    if (msgresposta.Content == "s" || msgresposta.Content == "n")
                    {
                        bool erroMsg = false;
                        if (msgresposta.Content == "s")
                        {
                            erroMsg = true;
                        }
                        await new ConfiguracoesServidorDAO().SetErroMsgAsync(new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), new ErroMsg(erroMsg)));
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithColor(Color.Green)
                                .WithTitle("Ok, farei tudo conforme o pedido 😃")
                            .Build());
                    }
                    else
                    {
                        await RotaFail();
                    }

                }
                else
                {
                    await Erro.EnviarFaltaPermissaoAsync("Administrador");
                }
            }
            else
            {
                await Erro.EnviarErroAsync("esse comando só pode ser usado em servidores.");
            }
        }
    }
}
