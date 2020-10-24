import { env } from 'process'
import { BaseEntity, Column, Entity, OneToMany, PrimaryGeneratedColumn } from 'typeorm'
import { Idol } from './idol'

@Entity('guilds')
export class Guild extends BaseEntity {
    @PrimaryGeneratedColumn('increment', {
        type: 'bigint',
        unsigned: true
    })
    id!: number

    @Column('varchar', {
        length: 20,
        nullable: false
    })
    discordId!: string

    @Column('varchar', {
        nullable: false,
        length: 13,
        default: env.bot_prefix as string
    })
    prefix!: string

    @OneToMany(() => Idol, x => x.guild)
    idols!: Idol[]
}
