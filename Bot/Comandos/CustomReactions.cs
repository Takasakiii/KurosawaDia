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

                        string resposta = "", pergunta = "";

                        if(resposta_pergunta[0].Trim().Length > 1024)
                        {
                            pergunta = $"{resposta_pergunta[0].Trim().Substring(0, 1020)}...";
                        }
                        else
                        {
                            pergunta = resposta_pergunta[0].Trim();
                        }

                        if (resposta_pergunta[1].Trim().Length > 1024)
                        {
                            resposta = $"{resposta_pergunta[0].Trim().Substring(0, 1020)}...";
                        }
                        else
                        {
                            resposta = resposta_pergunta[1].Trim();
                        }

                        embed.WithDescription(StringCatch.GetString("acrCriadaOk", "**{0}** a reação customizada foi criada com sucesso", context.User.ToString()));
                        embed.AddField(StringCatch.GetString("trigger", "Trigger: "), pergunta);
                        embed.AddField(StringCatch.GetString("resposta", "Reposta: "), resposta);
                        embed.AddField(StringCatch.GetString("codigo", "Codigo: "), codigo);
                    }
                    else
                    {
                        embed.WithTitle(StringCatch.GetString("acrErro", "Para adicionaru uma reação customizada você precisa me falar o trigger e a resposta da reação customizada"));
                        embed.AddField(StringCatch.GetString("usoCmd", "Uso do comando: "), StringCatch.GetString("usoAcr", "`{0}acr trigger | resposta`", (string)args[0]));
                        embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploAcr", "`{0}acr upei | boa corno`", (string)args[0]));
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("acrSemPerm", "**{0}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder adicionar uma Reação Customizada nesse servidor 😕", context.User.ToString()));
                    embed.WithColor(Color.Red);
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("acrDm", "Esse comando so pode ser usado em servidores"));
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
                                embed.WithDescription(StringCatch.GetString("dcrOk", "**{0}** a reação customizada com o codigo: `{1}` foi deletada do servidor", context.User.ToString(), codigo));
                            }
                            else
                            {
                                embed.WithDescription(StringCatch.GetString("dcrNenhuma", "**{0}** não foi possivel deletar uma reação customizada com esse codigo", context.User.ToString()));
                            }

                        }
                        catch
                        {
                            embed.WithDescription(StringCatch.GetString("dcrNumero", "**{0}** isso não é um numero", context.User.ToString()));
                            embed.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        embed.WithTitle(StringCatch.GetString("dcrSemCodio", "Você me precisa falar o codigo da reação customizada para que eu possa deletar ela"));
                        embed.AddField(StringCatch.GetString("usoCmd", "Uso do Comando: "), StringCatch.GetString("usoDcr", "`{0}dcr <codigo>`",(string)args[0]));
                        embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploDcr", "`{0}dcr 1`", (string)args[0]));
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("dcrSemPerm", "**{0}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder remover uma Reação Customizada nesse servidor 😕", context.User.ToString()));
                    embed.WithColor(Color.Red);
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("dcrDm", "Esse comando so pode ser usado em servidores"));
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
                    embed.WithDescription(StringCatch.GetString("lcrNenhuma", "**{0}** o servidor não tem nenhuma reação customizada", context.User.ToString()));
                    embed.WithColor(Color.Red);
                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("lcrDm", "Esse comando so pode ser usado em servidores"));
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

                string trigger = "";

                if(temp.trigger.Length > 25)
                {
                    trigger = $"{temp.trigger.Substring(0, 25)}...";
                }
                else
                {
                    trigger = temp.trigger;
                }

                respIds += $"`#{temp.codigo}`\n";
                respTriggers += $"{trigger}\n";
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
                    .WithTitle(StringCatch.GetString("lcrTxt", "Lista das Reações Customizadas:"))
                    .AddField(StringCatch.GetString("lcrCods", "Codigos: "), retornoStrings.Item1, true)
                    .AddField(StringCatch.GetString("lcrTriggers", "Triggers: "), retornoStrings.Item2, true)
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
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(AnteriorPagina, contexto, args), addEmoji: false);
            }
            if (pProximo)
            {
                Emoji emoji = new Emoji("➡");
                msg.AddReactionAsync(emoji);
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(ProximaPagina, contexto, args), addEmoji: false);
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

        public void TriggerACR (CommandContext contexto, Servidores servidor)
        {
            ACRs aCRs = new ACRs();
            aCRs.SetTrigger(contexto.Message.Content, servidor);
            aCRs = new ACRsDAO().ResponderAcr(aCRs);
            if (aCRs.resposta != null)
            {
                contexto.Channel.SendMessageAsync(aCRs.resposta);
            }      
        }
    }
}
