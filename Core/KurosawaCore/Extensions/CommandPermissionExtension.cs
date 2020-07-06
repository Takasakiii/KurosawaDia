using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace KurosawaCore.Extensions
{
    internal static class CommandPermissionExtension
    {
        internal static bool HasPermissions(this CommandContext context, Permissions permissions)
        {
            if (context.Member.IsOwner || context.Channel.PermissionsFor(context.Member).HasPermission(Permissions.Administrator | permissions))
            {
                return true;
            }

            return false;
        }

        internal static bool HasPermissions(this CommandContext context)
        {
            if (context.Member.IsOwner || context.Channel.PermissionsFor(context.Member).HasPermission(Permissions.Administrator))
            {
                return true;
            }

            return false;
        }
    }
}
