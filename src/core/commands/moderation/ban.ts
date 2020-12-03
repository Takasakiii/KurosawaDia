import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { BanOptions, MessageEmbed, Role } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'

@CommandName('ban')
@CommandInfo({
    description: 'ban',
    module: 'moderation'
})
export default class Ban extends Command {
    async validPermission (ctx: Context): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['BAN_MEMBERS'])) {
            throw new BotPermissionError(['BAN_MEMBERS'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['BAN_MEMBERS'])) {
            throw new ClientPermissionError(['BAN_MEMBERS'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: Context): Promise<void> {
        const author = ctx.memberAuthor?.roles.highest as Role
        const target = ctx.getMember(ctx.args[0])

        if (!target) {
            return
        }

        const targetRole = target?.roles.highest as Role

        if (author?.position < targetRole.position) {
            return
        }

        const bot = ctx.memberClient?.roles.highest as Role

        if (bot.position < targetRole.position) {
            return
        }

        let options: BanOptions | undefined
        const reason = ctx.args.slice(1).join(' ')

        if (ctx.args[1]) {
            options = {}
            options.reason = reason
        }

        const embed = new MessageEmbed({
            title: '**Buuuu buuuu desu waaaa!!!!!**',
            description: __({
                phrase: 'command.ban.embeds.warning.description',
                locale: ctx.guildConfig.lang
            }),
            image: {
                url: 'https://i.imgur.com/bwifre6.jpg'
            },
            color: embedConfig.colors.black
        })

        embed.addField(
            __({
                phrase: 'command.ban.embeds.warning.responsible',
                locale: ctx.guildConfig.lang
            }),
            ctx.memberAuthor?.displayName
        )

        if (ctx.args[1]) {
            embed.addField(
                __({
                    phrase: 'command.ban.embeds.warning.reason',
                    locale: ctx.guildConfig.lang
                }),
                reason
            )
        }

        const dm = await target?.createDM()
        await dm?.send(embed)

        await target?.ban(options)

        const embedConclusion = new MessageEmbed({
            description: __({
                phrase: 'command.ban.embeds.conclusion.description',
                locale: ctx.guildConfig.lang
            }, {
                author: ctx.author.username,
                server: ctx.guild?.name as string
            }),
            color: embedConfig.colors.black
        })

        if (ctx.args[1]) {
            embedConclusion.addField(
                __({
                    phrase: 'command.ban.embeds.conclusion.reason',
                    locale: ctx.guildConfig.lang
                }),
                reason
            )
        }

        ctx.channel.send(embedConclusion)
    }
}
