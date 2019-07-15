using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Comandos
{
    public class CustomReactions : Owner
    {
        public void acr(CommandContext context, object[] args)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(color: Color.DarkPurple);

            if (!context.IsPrivate)
            {
                SocketGuildUser usuario = context.User as SocketGuildUser;
                IRole cargo = (usuario as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Ajudante de Idol");

                if (usuario.GuildPermissions.ManageGuild || usuario.Roles.Contains(cargo))
                {
                    string[] comando = (string[])args[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                    string[] resposta_pergunta = msg.Split('|');

                    if (resposta_pergunta[0] != "" && resposta_pergunta[1] != "")
                    {
                        ACRs acr = new ACRs();
                        acr.SetAcr(trigger: resposta_pergunta[0].Trim(), resposta: resposta_pergunta[1].Trim(), new Servidores(context.Guild.Id), context.Guild.Id);
                        ulong codigo = new ACRsDAO().CriarAcr(acr);

                        embed.WithDescription($"**{context.User}** a reação customizada foi criada com sucesso");
                        embed.AddField("Trigger: ", resposta_pergunta[0].Trim());
                        embed.AddField("Reposta: ", resposta_pergunta[1].Trim());
                        embed.AddField("Codigo: ", codigo);
                    }
                    else
                    {
                        embed.WithTitle("Para adicionaru ma acr você precisa me falar o trigger e a resposta da acr");
                        embed.AddField("Uso do comando: ", $"`{(string)args[0]}acr trigger | resposta`");
                        embed.AddField("Exemplo: ", $"`{(string)args[0]}acr upei | boa corno`");
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription($"**{context.User}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder adicionar uma Reação Customizada nesse servidor 😕");
                    embed.WithColor(Color.Red);
                }
            }
            else
            {
                embed.WithDescription("Esse comando so pode ser usado em servidores");
                embed.WithColor(Color.Red);
            }

            context.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void dcr(CommandContext context, object[] args)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            if (!context.IsPrivate)
            {
                SocketGuildUser usuario = context.User as SocketGuildUser;
                IRole cargo = (usuario as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Ajudante de Idol");

                if (usuario.GuildPermissions.ManageGuild || usuario.Roles.Contains(cargo))
                {
                    string[] comando = (string[])args[1];
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                    if (msg != "")
                    {
                        try
                        {
                            ulong codigo = Convert.ToUInt64(msg);
                            ACRs acr = new ACRs();
                            acr.SetCod(codigo, new Servidores(context.Guild.Id));

                            if (new ACRsDAO().DeletarAcr(acr))
                            {
                                embed.WithDescription($"**{context.User}** a acr com o id: `{codigo}` foi deletada do servidor");
                            }
                            else
                            {
                                embed.WithDescription($"**{context.User}** não foi encontrada nenhuma acr com esse id no servidor");
                            }

                        }
                        catch
                        {
                            embed.WithDescription("onii-chan esse não é um numero valido");
                            embed.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        embed.WithTitle("Você me precisa falar o codigo da acr para que eu possa deletar ela");
                        embed.AddField("Uso do Comando: ", $"`{(string)args[0]}dcr <codigo>`");
                        embed.AddField("Exemplo: ", $"`{(string)args[0]}dcr 1`");
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription($"**{context.User}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder remover uma Reação Customizada nesse servidor 😕");
                    embed.WithColor(Color.Red);
                }
            }
            else
            {
                embed.WithDescription("Esse comando so pode ser usado em servidores");
                embed.WithColor(Color.Red);
            }

            context.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void lcr(CommandContext context, object[] args)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            if (!context.IsPrivate)
            {
                ACRs acr = new ACRs();
                acr.SetServidor(new Servidores(context.Guild.Id));
                ACRsDAO dao = new ACRsDAO();
                List<ACRs> listaRetorno = dao.ListarAcr(acr);
                
                if(listaRetorno.Count != 0)
                {

                    int[] restricoes = new int[2];
                    restricoes[0] = 0;
                    restricoes[1] = listaRetorno.Count / 10 + ((listaRetorno.Count % 10 > 0) ? 1 : 0);
                    //Declaracao da memoria extra que esse comando requer
                    ((List<object>)args[2]).Add(restricoes); //id 00 
                    ((List<object>)args[2]).Add(listaRetorno); //id 01
                    ((List<object>)args[2]).Add(1); //id 02 - Armazena a msg
                    ((List<object>)args[2]).Add(1); //id 03 - Armazena o controlador de reacoes
                    ((List<object>)args[2]).Add(1); //id 04 - Armazena o tipo de acao (next ou fowarding)

                    Menu(context, args);
                }
                else
                {
                    embed.WithDescription($"**{context.User}** o servidor não tem nenhuma acr");
                    embed.WithColor(Color.Red);
                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
            else
            {
                embed.WithDescription("Esse comando so pode ser usado em servidores");
                embed.WithColor(Color.Red);
                context.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

        private Tuple<string, string> CriarPagina(List<ACRs> listaRetorno, int paginaAtual)
        {
            string respIds = "";
            string respTriggers = "";
            for (int i = paginaAtual * 10; i < listaRetorno.Count && i < ((paginaAtual* 10) + 10); i++)
            {
                ACRs temp = listaRetorno[i];
                respIds += $"`#{temp.codigo}`\n";
                respTriggers += $"{temp.trigger}\n";
            }

            return Tuple.Create(respIds, respTriggers);
        }

        private void Menu (CommandContext contexto, object[] args)
        {
            int[] restricoes = (int[])((List<object>)args[2])[0];
            var retornoStrings = CriarPagina((List<ACRs>)((List<object>)args[2])[1], restricoes[0]);
            IUserMessage msg = null;
            if (retornoStrings.Item1 != "")
            {
                 msg = contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Reações Personalizadas")
                    .AddField("Codigos: ", retornoStrings.Item1, true)
                    .AddField("Triggers: ", retornoStrings.Item2, true)
                    .WithFooter($"{restricoes[0] + 1} / {restricoes[1]}")
                    .WithColor(Color.DarkPurple)
                    .Build()).GetAwaiter().GetResult();
                
            }

            bool pProximo = false;
            bool pAnterior = false;

            if (restricoes[1] != 1)
            {
                if (restricoes[0] == 0 && restricoes[0] < restricoes[1])
                {
                    pProximo = true;
                }
                else
                {
                    if ((restricoes[0] + 1) != restricoes[1])
                    {
                        pProximo = true;
                        pAnterior = true;
                    }
                    else
                    {
                        pAnterior = true;
                    }
                }
            }

            ((List<object>)args[2])[2] = msg;
            ReactionControler controler = new ReactionControler();
            ((List<object>)args[2])[3] = controler;
            if (pAnterior)
            {
                Emoji emoji = new Emoji("⬅");
                msg.AddReactionAsync(emoji);
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(AnteriorPagina, contexto, args));
            }
            if (pProximo)
            {
                Emoji emoji = new Emoji("➡");
                msg.AddReactionAsync(emoji);
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(ProximaPagina, contexto, args));
            }
        }

        private void ProximaPagina(CommandContext contexto, object[] args)
        {
            ((List<object>)args[2])[4] = 1;
            AjustesDeDados(contexto, args);
        }

        private void AnteriorPagina(CommandContext contexto, object[] args)
        {
            ((List<object>)args[2])[4] = 2;
            AjustesDeDados(contexto, args);
        }

        private void AjustesDeDados(CommandContext contexto, object[]args)
        {
            int tipo = (int)((List<object>)args[2])[4];
            int[] restricoes = (int[])((List<object>)args[2])[0];

            if (tipo == 1)
            {
                restricoes[0]++;
            }
            else
            {
                restricoes[0]--;
            }

            ((List<object>)args[2])[0] = restricoes;
            ((IUserMessage)((List<object>)args[2])[2]).DeleteAsync();
            ((ReactionControler)((List<object>)args[2])[3]).DesligarReaction();
            Menu(contexto, args);
        }
    }
}
