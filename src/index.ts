import { config } from 'dotenv'
import { env, exit } from 'process'
import { KurosawaDia } from '@bot'
import { DatabaseBot } from '@database'
import { configure } from 'i18n'

configure({
    locales: [
        'pt-br'
    ],
    defaultLocale: 'pt-br',
    directory: './src/i18n',
    objectNotation: true
})

config()

init().catch(error => {
    console.error(error)
    exit()
})

async function init () {
    const databaseBot = new DatabaseBot()
    await databaseBot.start()

    const kurosawaDia = new KurosawaDia()
    kurosawaDia.registerCommands()
    kurosawaDia.token = env.bot_token as string
    kurosawaDia.start()
}
