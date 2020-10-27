import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'
import infos from '@configs/infos.json'
import { Alias, Name } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@Name('info')
@Alias('convite', 'ping')
export default class Info extends Command {
    constructor () {
        super()
        this.info = {
            description: 'Contém informações de suporte e algumas coisinhas pessoais.',
            module: 'Help'
        }
    }

    async execCommand (ctx: IContext): Promise<void> {
        const embed = new MessageEmbed({
            title: '**Dia\'s book:**',
            description: 'Espero que não faça nada estranho com minhas informações! (Tô zoando kkkkkk :stuck_out_tongue_closed_eyes:)',
            thumbnail: {
                url: 'https://i.imgur.com/L8PxTrT.jpg'
            },
            image: {
                url: 'https://i.imgur.com/qGb6xtG.jpg'
            },
            color: embedConfig.colors.purple,
            fields: [
                {
                    name: 'Sobre mim:',
                    value: '__Nome__: Kurosawa Dia (Dia-chan)\n__Aniversário__: 1° de Janeiro (Quero presentes!)\n__Ocupação__: Estudante e traficante/idol nas horas vagas'
                },
                {
                    name: 'As pessoas que fazem tudo isso ser possível:',
                    value: 'Takasaki#7072\nYummi#2708\nLuckShiba#6614\nVulcan#4805\n𝙆𝙪𝙧𝙪𝙢𝙞 𝙏𝙤𝙠𝙞𝙨𝙖𝙠𝙞#7872\n\nE é claro você que acredita em meu potencial :orange_heart:'
                },
                {
                    name: 'Links úteis:',
                    value: `[Me adicione em seu servidor](${infos.invite})\n[Entre no meu servidor para dar suporte ao projeto](${infos.suport})\n[Vote em mim no DiscordBotList para que eu possa ajudar mais pessoas](${infos.topgg})\n[Vote em mim no Bots para Discord para que eu possa ajudar mais pessoas](${infos.bpd})`
                },
                {
                    name: 'Informações chatas:',
                    value: `__Ping__: ${ctx.client.ws.ping}\n__Servidores__: ${ctx.client.guilds.cache.size}\n__Versão__: ${infos.version.numb} (${infos.version.name})`
                }
            ]
        })

        ctx.channel.send(embed)
    }
}
