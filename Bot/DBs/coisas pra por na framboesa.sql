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
	cod bigint not null auto_increment,
    cod_servidor int not null,
    idioma int not null default 0,
    PIConf bool not null default false,
    PIrate double not null default 2.0,
    msgError bool not null default true,
    DiaAPI bool not null default true,
    MsgPIUp text,
    bemvindoMsg text,
    sairMsg text,
    foreign key (cod_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);

create table Tipos_Canais (
	cod bigint not null,
    Descricao varchar (255) not null unique,
    primary key (cod)
);

#Set dos canais yay
insert into Tipos_Canais values (0, "Bem Vindo (bemvindoCh)");
insert into Tipos_Canais values (1, "Sair (sairCh)");

create table Canais (
	cod bigint not null auto_increment,
    cod_Tipos_Canais bigint not null,
    canal varchar(255) not null,
    id bigint not null,
    codigo_servidor int not null,
    foreign key (cod_Tipos_Canais) references Tipos_Canais (cod),
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);

delimiter $$
create function verificarConfig(
		_codServidor int
) returns int begin
	declare _return int;
    set _return = (select count(cod) from configuracoesservidores where cod_servidor = _codServidor);
    return _return;
end$$

create procedure criarConfig(
	in _codServidor int
) begin
	if((select verificarConfig(_codServidor)) = 0) then
		insert into configuracoesservidores (cod_servidor) values (_codServidor);
	end if;
end$$

create procedure configurePI(
	in _idServidor bigint,
    in _piconf bool,
    in _pirate double,
    in _msgPiup text
) begin
	declare _cod int;
    set _cod = (select codigo_servidor from Servidores where id_servidor = _idServidor);
    call criarConfig(_cod);
    update configuracoesservidores set PIConf = _piconf where cod_servidor = _cod;
    update configuracoesservidores set PIrate = _pirate where cod_servidor = _cod;
    if (_msgPiup <> NULL) then
		update configuracoesservidores set MsgPIUp = _msgPiup where cod_servidor = _cod;
	end if;
end$$

create procedure AdcCh (
	in _tipo_canal int,
	in _canal varchar (255),
    in _id_canal bigint,
    in _id_servidor bigint
) begin
	if(select count(Canais.cod) from Canais where Canais.id  = _id_canal and Canais.codigo_servidor = (select Servidores.codigo_Servidor from Servidores where Servidores.id_servidor = _id_servidor)) = 0 then
		insert into Canais (cod_Tipos_Canais, canal, id, codigo_servidor) values (_tipo_canal, _canal, _id_canal, (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor));
    end if;
end $$

create procedure GetCh (
	in _tipo_canal bigint,
    in _id_servidor bigint
) begin 
	select Canais.cod, Canais.cod_Tipos_Canais, canal, id, servidores.id_servidor, Servidores.nome_servidor from Canais join servidores on Servidores.codigo_servidor = Canais.codigo_servidor where Canais.cod_Tipos_Canais = _tipo_canal and Canais.codigo_servidor = (Select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$