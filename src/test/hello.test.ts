import supertest from 'supertest'
import { serverBot } from '../server'

describe('hello world', () => {
    it('hello', async () => {
        const result = await supertest(serverBot.app).get('/')
        expect(result.text).toEqual('hello world')
        expect(result.status).toEqual(200)
    })
})
