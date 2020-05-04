using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;

namespace KurosawaCore.Extensions
{
    internal sealed class PermissionExtension
    {
        private long Permissoes;
        internal PermissionExtension(DiscordChannel canal, DiscordMember membro)
        {
            if (membro.IsOwner)
            {
                Permissoes = (long)Permissions.Administrator;
            }
            else
            {
                Permissoes = (long)canal.PermissionsFor(membro);
            }
        }

        internal bool ValidarPermissoes(params Permissions[] permissoes)
        {
            long count = 0;
            if (!Array.Exists(permissoes, x => x == Permissions.Administrator))
                count += (long)Permissions.Administrator;
            foreach (Permissions temp in permissoes)
                count += (long)temp;
            return (Permissoes & count) > 0;
        }

        internal static bool ValidarPermissoes(CommandContext context, params Permissions[] permissoes)
        {
            return new PermissionExtension(context.Channel, context.Member).ValidarPermissoes(permissoes);
        }
    }
}
