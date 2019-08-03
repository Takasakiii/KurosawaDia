insert into Tipos_Cargos values (2, "Cargos por XP (XpRole)");
alter table Cargos add column requesito bigint not null default 0;
create table PontosInterativos (
	cod bigint not null auto_increment,
    servidores_usuarios_servidor int not null,
    servidores_usuarios_usuario int not null,
    PI bigint not null,
    fragmentosPI bigint not null,
    foreign key (servidores_usuarios_servidor, servidores_usuarios_usuario) references servidores_usuarios(Servidores_codigo_servidor, Usuarios_codigo_usuario),
    primary key (cod)
);

create table ConfiguracoesServidores(
	cod bigint not null,
    cod_servidor int not null,
    idioma int not null default 0,
    PIrate double not null default 2.0,
    msgError bool not null default true,
    DiaAPI bool not null default true,
    MsgPIUp text not null,
    bemvindoMsg text,
    sairMsg text,
    foreign key (cod_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);