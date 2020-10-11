import dotenv from 'dotenv'
import { env } from 'process'
import KurosawaDia from './bot'

dotenv.config()

const kurosawaDia = new KurosawaDia(env.bot_token as string)
kurosawaDia.start()
