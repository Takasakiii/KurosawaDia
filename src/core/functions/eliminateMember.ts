import { IContext } from '@bot/models/context'
import { MessageEmbed, Role } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'
import { __ } from 'i18n'

type eliminateType = 'ban' | 'softban' | 'kick'

export async function eliminateMember (ctx: IContext, type: eliminateType, message: string): Promise<void> {
    const botRole = ctx.memberClient?.roles.highest
    const authorRole = ctx.memberAuthor?.roles.highest
    const target = ctx.message.mentions.members?.first()
    const targetRole = target?.roles.highest

    if (!target) {
        return
    }

    if (!botRole || !authorRole || !targetRole) {
        ctx.channel.send('problema nas tags')
        return
    }

    if (Role.comparePositions(botRole, targetRole) <= 0) {
        ctx.channel.send('bot sem perm')
        return
    }

    if (Role.comparePositions(authorRole, targetRole) <= 0) {
        ctx.channel.send('vc sem perm')
        return
    }

    const embedMember = new MessageEmbed({
        title: '**Buuuu buuuu desu waaaa!!!!!**',
        description: `Você foi ${__('function.eliminate.member.' + type)} do servidor **${ctx.guild?.name}**.`,
        image: {
            url: 'https://i.imgur.com/bwifre6.jpg'
        },
        color: embedConfig.colors.black
    })

    if (message) {
        embedMember.addField('Motivo:', message)
    }

    embedMember.addField('Responsável:', `${ctx.memberAuthor?.displayName}`)

    const dm = await target?.createDM()
    await dm?.send(embedMember)

    const reason = `Responsável: ${ctx.author.username}#${ctx.author.discriminator} [${(!message ? 'Motivo: ' + message : '')}]`

    switch (type) {
    case 'ban':
        await target.ban({
            reason: reason
        })
        break
    case 'kick':
        await target.kick(reason)
        break
    case 'softban':
        await target.ban({
            days: 1,
            reason: reason
        })

        await ctx.guild?.members.unban(target.user)
        break
    default:
        break
    }

    const embedGuild = new MessageEmbed({
        title: `Prontinhooo! O ${target?.user.username}#${target?.user.discriminator} foi ${__('function.eliminate.member.' + type)} do servidor 😀`,
        color: embedConfig.colors.black
    })

    await ctx.channel.send(embedGuild)
}
