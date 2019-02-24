﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Nucleo.Extensions
{
    public static class MessageExtensions
    {
        public static Task<IUserMessage> SendConfirmAsync(this CommandContext context, string text) {
            return context.Channel.SendMessageAsync("", embed: new EmbedBuilder().WithOkColor().WithDescription($"**{context.User}** {text}").Build());
        }

        public static Task<IUserMessage> SendErrorAsync(this CommandContext context, string text) {
            return context.Channel.SendMessageAsync("", embed: new EmbedBuilder().WithErrorColor().WithDescription($"**{context.User}** {text}").Build());
        }
        //generalizar pode ser legal :D (os dois metodos acima)
        public static IMessage DeleteAfter(this IUserMessage msg, int seconds)
        {
            Task.Run(async () => 
            {
                await Task.Delay(seconds * 1000).ConfigureAwait(false);
                try { await msg.DeleteAsync().ConfigureAwait(false); }
                catch { } // <- Exeption erronea sem tratamento (erro grave)
            });
            return msg;
        }

        public static SocketUser GetUser(this CommandContext context, DiscordSocketClient client, string[] comando)
        {
            string UserId = ""; //Falta um modelo pra Usuario 

            try
            {
                try
                {
                    UserId = client.GetUser(Convert.ToUInt64(comando[1])).Id.ToString();
                }
                catch
                {
                    UserId = context.Message.MentionedUserIds.GetFirst();
                }
            }
            catch
            {
                UserId = context.User.Id.ToString();
            }

           return client.GetUser(Convert.ToUInt64(UserId));
            
            //se vc trabalha com userid em uint64 em maioria pq esta em string (grave erro de otimização)
        }
    }
}