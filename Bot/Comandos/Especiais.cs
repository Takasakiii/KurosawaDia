using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Discord;
using Discord.Commands;
using System;
using static Bot.DataBase.MainDB.Modelos.Servidores;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Especiais : Configuracoes
    {
        public void insult(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.permissoes == Permissoes.ServidorPika)
                    {
                        string[] comando = (string[])args[1];
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                        context.Message.DeleteAsync();
                        Tuple<IUser, string> user = new UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                        string[] insultos = {
                        "você eh mais gordo q a mãe do gordo",
                        "você eh mais depresso q o th"
                    };
                        Random rand = new Random();
                        int i = rand.Next(insultos.Length);

                        context.Channel.SendMessageAsync($"{user.Item1.Mention} {insultos[i]}");
                    }
                }
            }
        }
    }
}
