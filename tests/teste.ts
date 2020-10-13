import { serverBot } from '../src/server'
import superteste from 'supertest'

describe('teste', () => {
    it('teste', async () => {
        const result = await superteste(serverBot).get('/')
        expect(result.text).toEqual('hello')
        expect(result.status).toEqual(200)
    })
})
