import { config } from 'dotenv'
import { env } from 'process'
import { configure } from 'i18n'
import kurosawaDia from '@bot'
import serverSocket from '@server'
import 'reflect-metadata'

configure({
    locales: [
        'pt-br',
        'en-us',
        'conf-lang'
    ],
    defaultLocale: 'en-us',
    directory: './src/i18n',
    objectNotation: true,
    syncFiles: true
})

config()

init().catch(error => {
    console.error(error)
})

async function init () {
    serverSocket.url = env.SERVER_SOCKET_URL as string
    serverSocket.start()

    kurosawaDia.token = env.BOT_TOKEN as string
    kurosawaDia.registerCommands()
    kurosawaDia.start()
}
