import { BaseEntity, Column, Entity, OneToMany, PrimaryGeneratedColumn } from 'typeorm'
import { Idol } from './idol'
import config from '@/configs/config.json'

@Entity('guilds')
export class Guild extends BaseEntity {
    @PrimaryGeneratedColumn('increment', {
        type: 'bigint',
        unsigned: true
    })
    id!: number

    @Column('varchar', {
        nullable: false,
        length: 20
    })
    discordId!: string

    @Column('varchar', {
        nullable: false,
        length: 13,
        default: config.bot.prefix
    })
    prefix!: string

    @OneToMany(() => Idol, x => x.guild)
    idols!: Idol[]
}
