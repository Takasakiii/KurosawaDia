import { IContext } from '@bot/models/context'
import { MessageEmbed, Role } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

type eliminateType = 'ban' | 'softban' | 'kick'

export async function eliminateMember (ctx: IContext, type: eliminateType, message: string): Promise<void> {
    const botRole = ctx.memberClient?.roles.highest
    const authorRole = ctx.memberAuthor?.roles.highest
    const target = ctx.message.mentions.members?.first()
    const targetRole = target?.roles.highest

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
        description: `Você foi ${type} do servidor **${ctx.guild?.name}**.`,
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
    dm?.send(embedMember)

    const log = `Responsável: ${ctx.author.username}#${ctx.author.discriminator} [${(!message ? 'Motivo: ' + message : '')}]`

    switch (type) {
    case 'ban':
        await target?.ban({
            reason: log
        })
        break

    default:
        break
    }

    const embedGuild = new MessageEmbed({
        title: `Prontinhooo! O ${target?.user.username}#${target?.user.discriminator} foi ${type} do servidor 😀`,
        color: embedConfig.colors.black
    })

    await ctx.channel.send(embedGuild)
}
