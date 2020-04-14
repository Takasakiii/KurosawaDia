using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Image", "🖼")]
    [Description("Este módulo possui imagens fofinhas para agraciar seu computador.")]
    public class Image
    {
        [Command("cat")]
        [Description("Nyann")]
        public async Task Cat(CommandContext ctx)
        {
            string url = await new ImageExtension().GetCat();
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "Meow",
                Description = $"[Link Direto]({url})",
                ImageUrl = url,
                Color = DiscordColor.Turquoise
            });
        }

        [Command("dog")]
        [Description("LuckShiba")]
        public async Task Dog(CommandContext ctx)
        {
            Random random = new Random();

            if (random.Next(100) != 24)
            {
                string url = await new ImageExtension().GetDog();
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "Woof",
                    Description = $"[Link Direto]({url})",
                    ImageUrl = url,
                    Color = DiscordColor.Turquoise
                });
            }
            else
            {
                DiscordUser user = await ctx.Client.GetUserAsync(355750436424384524);
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "Se ta procurando um cachorinho ? Me adote",
                    Description = $"Me adote no discord **{user.Username}#{user.Discriminator}** Woof Woof",
                    ThumbnailUrl = user.AvatarUrl,
                    Color = DiscordColor.Turquoise
                });
            }
        }

        [Command("loli")]
        [Description("Manda uma imagem para que você seja preso")]
        public async Task Loli(CommandContext ctx)
        {
            if ((byte)(await new ServidoresDAO().Get(new Servidores { ID = ctx.Guild.Id })).Espercial < (byte)TiposServidores.LolisEdition)
                throw new Exception();

            string url = await new ImageExtension().GetLoli();
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder 
            {
                Title = "S-Senpai",
                Description = $"[Link Direto]({url})",
                ImageUrl = url,
                Color = DiscordColor.Turquoise
            });
        }

        [Command("lolibomb")]
        [Description("Manda varias imagens para você ser preso imediatamente")]
        public async Task LoliBomb(CommandContext ctx)
        {
            if ((byte)(await new ServidoresDAO().Get(new Servidores { ID = ctx.Guild.Id })).Espercial < (byte)TiposServidores.LolisEdition)
                throw new Exception();

            await ctx.RespondAsync(await new ImageExtension().GetLoliBomb());
        }
    }
}
