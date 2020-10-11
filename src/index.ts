import { Client } from 'discord.js'
import config from './configs/bot.json'

const bot = new Client()

bot.on('ready', () => {
    console.log('bot iniciado')
})

bot.on('message', msg => {
    if (msg.content === 'ping') {
        msg.channel.send('pong')
    }
})

bot.login(config.token)
