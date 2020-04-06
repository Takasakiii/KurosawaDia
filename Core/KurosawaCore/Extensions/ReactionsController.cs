using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal sealed class ReactionsController<Item> where Item : class
    {
        private struct ReactionAguardada
        {
            public DiscordEmoji Emoji { get; set; }
            public DiscordUser Autor { get; set; }
            public DiscordMessage Msg { get; set; }
            public Tuple<MethodInfo, object> FunctionResultante { get; set; }
            public DateTime AdicionadoEm { get; set; }
            public object[] Args { get; set; }
        }

        private static List<ReactionAguardada> BufferReacoes = new List<ReactionAguardada>();
        private Item Contexto;

        internal ReactionsController(Item contexto)
        {
            Contexto = contexto;

            dynamic conversionitem = Convert.ChangeType(contexto, typeof(Item));
            DiscordClient cliente = conversionitem.Client;

            cliente.MessageReactionAdded += ClienteDiscord_MessageReactionAdded;
            cliente.MessageReactionRemoved += Cliente_MessageReactionRemoved;
        }

        private async Task Cliente_MessageReactionRemoved(MessageReactionRemoveEventArgs e)
        {
            await Action(e.User, e.Emoji, e.Message);
        }

        internal void AddReactionEvent(DiscordMessage msg, Tuple<MethodInfo, object> exec, DiscordEmoji emoji = null, DiscordUser autor = null, params object[] args)
        {
            ReactionAguardada reacao = new ReactionAguardada
            {
                Emoji = emoji,
                Msg = msg,
                Autor = autor,
                FunctionResultante = exec,
                AdicionadoEm = DateTime.Now,
                Args = args
            };
            BufferReacoes.Add(reacao);
        }

        internal Tuple<MethodInfo, object> ConvertToMethodInfo<ArgType> (Func<Item, ArgType, Task> funcao) where ArgType : class
        {
            return Tuple.Create(funcao.Method, funcao.Target);
        }

        internal Tuple<MethodInfo, object> ConvertToMethodInfo(Func<Item, Task> funcao)
        {
            return Tuple.Create(funcao.Method, funcao.Target);
        }

        private async Task ClienteDiscord_MessageReactionAdded(MessageReactionAddEventArgs e)
        {
            await Action(e.User, e.Emoji, e.Message); 
        }

        private Task Action(DiscordUser user, DiscordEmoji emoji, DiscordMessage msg)
        {
            BufferReacoes.RemoveAll(x => x.AdicionadoEm >= x.AdicionadoEm.AddMinutes(5));
            int index = -1;
            for (int i = 0; i < BufferReacoes.Count; i++)
            {
                if (BufferReacoes[i].Msg == msg)
                {
                    bool validado = true;
                    if (BufferReacoes[i].Autor != null && BufferReacoes[i].Autor != user)
                        validado = false;
                    if (BufferReacoes[i].Emoji != null && BufferReacoes[i].Emoji != emoji)
                        validado = false;
                    if (validado)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index >= 0)
            {
                List<object> obj = new List<object>();
                obj.Add(Contexto);
                obj.AddRange(BufferReacoes[index].Args);
                Task tarefa = (Task)BufferReacoes[index].FunctionResultante.Item1.Invoke(BufferReacoes[index].FunctionResultante.Item2, obj.ToArray());
                BufferReacoes.RemoveAt(index);
                return tarefa;
            }
            return Task.CompletedTask;
        }


    }
}
