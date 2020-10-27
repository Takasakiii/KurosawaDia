import { Command } from '@/core/models/commands'
import { IContext } from '@/core/models/context'
import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

export default class Sobre extends Command {
    name: string
    alias: string[]

    constructor () {
        super()
        this.name = 'sobre'
        this.alias = [
            'apresentacao',
            'apresentação'
        ]
    }

    async execCommand (ctx: IContext): Promise<void> {
        const embed = new MessageEmbed({
            title: 'Será um enorme prazer te ajudar :yum:',
            description: 'Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e também ajudo as pessoas com algumas coisinhas no Discord :wink:\nSe você usar `ajuda` no chat, vai aparecer tudo que eu posso fazer atualmente (isso não é demais :grin:)\nSério, estou muito ansiosa para passar um tempo com você e também te ajudar XD\nSe você tem ideias de mais coisas que eu possa fazer, por favor mande uma sugestão com o `sugestao`\nSe você quiser saber mais sobre mim, me convidar para seu servidor, ou até entrar em meu servidor de suporte, use o comando `info`\n\nE como a Mari fala, Let\'s Go!!',
            image: {
                url: 'https://i.imgur.com/PC5QDiX.png'
            },
            footer: {
                iconURL: 'http://i.imgur.com/Cm8grM4.png',
                text: 'Kurosawa Dia é um projeto feito com amor e carinho pelos seus desenvolvedores!'
            },
            color: embedConfig.colors.purple
        })

        ctx.channel.send(embed)
    }
}
