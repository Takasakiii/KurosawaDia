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

namespace Bot.Comandos
{
    public class CustomReactions : GenericModule
    {
        public CustomReactions(CommandContext contexto, string prefixo, string[] comando) : base(contexto, prefixo, comando)
        {

        }

        public void acr()
        {
            new BotCadastro(() =>
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(color: Color.DarkPurple);

                if (!Contexto.IsPrivate)
                {
                    SocketGuildUser usuario = Contexto.User as SocketGuildUser;
                    IRole cargo = (usuario as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Ajudante de Idol");

                    if (usuario.GuildPermissions.ManageGuild || usuario.Roles.Contains(cargo))
                    {
                        string[] comando = (string[])args[1];
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                        string[] resposta_pergunta = msg.Split('|');

                        if (resposta_pergunta.Length >= 2 && !string.IsNullOrEmpty(resposta_pergunta[0]) && !string.IsNullOrEmpty(resposta_pergunta[1]))
                        {
                            ReacoesCustomizadas cr = new ReacoesCustomizadas(resposta_pergunta[0].Trim(), resposta_pergunta[1].Trim(), new Servidores(Contexto.Guild.Id), Contexto.Guild.Id);
                            new ReacoesCustomizadasDAO().CriarAcr(ref cr);

                            string resposta = "", pergunta = "";

                            if (resposta_pergunta[0].Trim().Length > 1024)
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

                            embed.WithDescription(StringCatch.GetString("acrCriadaOk", "**{0}** a reação customizada foi criada com sucesso", Contexto.User.ToString()));
                            embed.AddField(StringCatch.GetString("trigger", "Trigger: "), pergunta);
                            embed.AddField(StringCatch.GetString("resposta", "Reposta: "), resposta);
                            embed.AddField(StringCatch.GetString("codigo", "Codigo: "), cr.Cod);
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
                        embed.WithDescription(StringCatch.GetString("acrSemPerm", "**{0}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder adicionar uma Reação Customizada nesse servidor 😕", Contexto.User.ToString()));
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("acrDm", "Esse comando so pode ser usado em servidores"));
                    embed.WithColor(Color.Red);
                }

                Contexto.Channel.SendMessageAsync(embed: embed.Build());
            }, Contexto).EsperarOkDb();
        }

        public void dcr()
        {
            new BotCadastro(() =>
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);

                if (!Contexto.IsPrivate)
                {
                    SocketGuildUser usuario = Contexto.User as SocketGuildUser;
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
                                ReacoesCustomizadas acr = new ReacoesCustomizadas(codigo);
                                acr.SetServidor(new Servidores(Contexto.Guild.Id));

                                if (new ReacoesCustomizadasDAO().DeletarAcr(acr))
                                {
                                    embed.WithDescription(StringCatch.GetString("dcrOk", "**{0}** a reação customizada com o codigo: `{1}` foi deletada do servidor", Contexto.User.ToString(), codigo));
                                }
                                else
                                {
                                    embed.WithDescription(StringCatch.GetString("dcrNenhuma", "**{0}** não foi possivel deletar uma reação customizada com esse codigo", Contexto.User.ToString()));
                                }

                            }
                            catch
                            {
                                embed.WithDescription(StringCatch.GetString("dcrNumero", "**{0}** isso não é um numero", Contexto.User.ToString()));
                                embed.WithColor(Color.Red);
                            }
                        }
                        else
                        {
                            embed.WithTitle(StringCatch.GetString("dcrSemCodio", "Você me precisa falar o codigo da reação customizada para que eu possa deletar ela"));
                            embed.AddField(StringCatch.GetString("usoCmd", "Uso do Comando: "), StringCatch.GetString("usoDcr", "`{0}dcr <codigo>`", (string)args[0]));
                            embed.AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("exemploDcr", "`{0}dcr 1`", (string)args[0]));
                            embed.WithColor(Color.Red);
                        }
                    }
                    else
                    {
                        embed.WithDescription(StringCatch.GetString("dcrSemPerm", "**{0}** Você não possui permissão de `Gerenciar Servidor` ou o cargo `Ajudante de Idol` para poder remover uma Reação Customizada nesse servidor 😕", Contexto.User.ToString()));
                        embed.WithColor(Color.Red);
                    }
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("dcrDm", "Esse comando so pode ser usado em servidores"));
                    embed.WithColor(Color.Red);
                }

                Contexto.Channel.SendMessageAsync(embed: embed.Build());
            }, Contexto).EsperarOkDb();
        }

        public void lcr()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            if (!Contexto.IsPrivate)
            {
                ReacoesCustomizadas acr = new ReacoesCustomizadas();
                acr.SetServidor(new Servidores(Contexto.Guild.Id));
                ReacoesCustomizadasDAO dao = new ReacoesCustomizadasDAO();
                List<ReacoesCustomizadas> listaRetorno = dao.ListarAcr(acr);

                if (listaRetorno.Count != 0)
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

                    Menu(Contexto, args);
                }
                else
                {
                    embed.WithDescription(StringCatch.GetString("lcrNenhuma", "**{0}** o servidor não tem nenhuma reação customizada", Contexto.User.ToString()));
                    embed.WithColor(Color.Red);
                    Contexto.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
            else
            {
                embed.WithDescription(StringCatch.GetString("lcrDm", "Esse comando so pode ser usado em servidores"));
                embed.WithColor(Color.Red);
                Contexto.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

        private Tuple<string, string> CriarPagina(List<ReacoesCustomizadas> listaRetorno, int paginaAtual)
        {
            string respIds = "";
            string respTriggers = "";
            for (int i = paginaAtual * 10; i < listaRetorno.Count && i < ((paginaAtual * 10) + 10); i++)
            {
                ReacoesCustomizadas temp = listaRetorno[i];

                string trigger = "";

                if (temp.Trigger.Length > 25)
                {
                    trigger = $"{temp.Trigger.Substring(0, 25)}...";
                }
                else
                {
                    trigger = temp.Trigger;
                }

                respIds += $"`#{temp.Cod}`\n";
                respTriggers += $"{trigger}\n";
            }

            return Tuple.Create(respIds, respTriggers);
        }

        private void Menu(CommandContext contexto, object[] args)
        {
            int[] restricoes = (int[])((List<object>)args[2])[0];
            var retornoStrings = CriarPagina((List<ReacoesCustomizadas>)((List<object>)args[2])[1], restricoes[0]);
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
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(AnteriorPagina));
            }
            if (pProximo)
            {
                Emoji emoji = new Emoji("➡");
                msg.AddReactionAsync(emoji);
                controler.GetReaction(msg, emoji, contexto.User, new ReturnMethod(ProximaPagina));
            }
        }

        private void ProximaPagina()
        {
            ((List<object>)args[2])[4] = 1;
            AjustesDeDados(Contexto, args);
        }

        private void AnteriorPagina()
        {
            ((List<object>)args[2])[4] = 2;
            AjustesDeDados(Contexto, args);
        }

        private void AjustesDeDados(CommandContext contexto, object[] args)
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

        public void TriggerACR(CommandContext context, Servidores servidor)
        {
            ReacoesCustomizadas aCRs = new ReacoesCustomizadas();
            aCRs.SetTrigger(context.Message.Content, servidor);
            new ReacoesCustomizadasDAO().ResponderAcr(ref aCRs);
            if (aCRs.Resposta != null)
            {
                StringVarsControler varsControler = new StringVarsControler(context);
                new EmbedControl().SendMessage(context.Channel, varsControler.SubstituirVariaveis(aCRs.Resposta));
            }
        }
    }
}
