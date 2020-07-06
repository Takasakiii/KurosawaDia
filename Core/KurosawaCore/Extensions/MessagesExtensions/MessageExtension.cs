using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KurosawaCore.Extensions.MessagesExtensions
{
    internal class MessageExtension
    {
        internal DiscordMessage Message { get; private set; }

        internal bool Success
        {
            get
            {
                return Message != null;
            }
        }

        internal MessageExtension(CommandContext contexto)
        {
            Message = contexto.Message;
        }

        internal MessageExtension(DiscordMessage message)
        {
            Message = message;
        }

        internal MessageExtension(CommandContext contexto, string message)
        {
            DiscordMessageConverter converter = new DiscordMessageConverter();
            if (converter.TryConvert(message, contexto, out DiscordMessage result))
            {
                Message = result;
            }
            else
            {
                Regex regex = new Regex(@".channels.[0-9]{18}.[0-9]{18}.(?<id>[0-9]{18})");
                Match data = regex.Match(message);
                if (data.Success)
                {
                    if(converter.TryConvert(data.Groups["id"].Value, contexto, out DiscordMessage res))
                    {
                        Message = res;
                    }
                }
                
            }
                
        }

        
    }
}
