using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;

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
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                string[] resposta_pergunta = msg.Split('|');

                if (resposta_pergunta[0] != "" && resposta_pergunta[1] != "")
                {
                    ACRs acr = new ACRs();
                    acr.SetAcr(trigger: resposta_pergunta[0].Trim(), resposta: resposta_pergunta[1].Trim(), id_servidor: context.Guild.Id);
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
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if(msg != "")
                {
                    try
                    {
                        ulong codigo = Convert.ToUInt64(msg);
                        ACRs acr = new ACRs();
                        acr.SetCod(codigo, context.Guild.Id);

                        if(new ACRsDAO().DeletarAcr(acr))
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
                
            }
            else
            {
                embed.WithDescription("Esse comando so pode ser usado em servidores");
                embed.WithColor(Color.Red);
            }

            context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
