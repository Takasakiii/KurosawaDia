using Discord;
using Discord.Commands;
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
        public static Task<IUserMessage> SendConfirmAsync(this IMessageChannel ch, string text) {
            return ch.SendMessageAsync("", embed: new EmbedBuilder().WithOkColor().WithDescription(text).Build());
        }

        public static Task<IUserMessage> SendErrorAsync(this IMessageChannel ch, string text) {
            return ch.SendMessageAsync("", embed: new EmbedBuilder().WithErrorColor().WithDescription(text).Build());
        }
    }
}
