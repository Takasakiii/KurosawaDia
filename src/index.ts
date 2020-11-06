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
    serverSocket.url = env.server_socket_url as string
    serverSocket.start()

    kurosawaDia.token = env.bot_token as string
    kurosawaDia.registerCommands()
    kurosawaDia.start()
}
