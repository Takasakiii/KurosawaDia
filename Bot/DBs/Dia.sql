create database Pitas_Kurosawa;
use Pitas_Kurosawa;

SET GLOBAL max_connections = 1000;

create table Servidores (
	codigo_servidor int not null auto_increment,
	id_servidor bigint not null unique key,
    nome_servidor varchar(255) not null,
    especial_servidor bool not null default false,
    prefix_servidor varchar(25),
    primary key (codigo_servidor)
);
alter table Servidores modify column  especial_servidor int not null default 0;

create table ACRS (
	codigo_acr bigint not null auto_increment,
    trigger_acr text not null,
    resposta_acr text not null,
    codigo_servidor int not null,
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (codigo_acr)
);

create table Usuarios (
	codigo_usuario int not null auto_increment,
	id_usuario bigint not null unique key,
    nome_usuario varchar(255) not null,
    primary key (codigo_usuario)
);

create table Insultos(
	cod bigint not null auto_increment,
    codigo_usuario int not null,
    insulto text not null,
    foreign key (codigo_usuario) references Usuarios(codigo_usuario),
    primary key (cod)
);

create table Servidores_Usuarios (
	Servidores_codigo_servidor int not null,
    Usuarios_codigo_usuario int not null,
    foreign key (Servidores_codigo_servidor) references Servidores (codigo_servidor),
    foreign key (Usuarios_codigo_usuario) references Usuarios (codigo_usuario),
    primary key (Servidores_codigo_servidor, Usuarios_codigo_usuario)
);

create table Tipos_Cargos(
	cod bigint not null,
    Descricao varchar (255) not null unique, 
    primary key (cod)
);

#Setup Tipos_Cargos
insert into Tipos_Cargos values (1, "Permissao Extendida (Ajudante de Idol)");
insert into Tipos_Cargos values (2, "Cargos por XP (XpRole)");

create table Cargos(
	cod bigint not null auto_increment,
    cod_Tipos_Cargos bigint not null,
    cargo varchar (255) not null,
    id bigint not null,
    codigo_Servidores int not null,
    foreign key (cod_Tipos_Cargos) references Tipos_Cargos (cod),
    foreign key (codigo_Servidores) references Servidores (codigo_Servidor),
    primary key (cod)
);
alter table Cargos add column requesito bigint not null default 0;

create table AdmsBot(
	cod bigint not null auto_increment,
    codigo_Usuario int not null,
    permissao int not null,
    foreign key (codigo_Usuario) references Usuarios (codigo_usuario),
    primary key (cod)
);

CREATE TABLE Fuck (
  cod bigint NOT NULL AUTO_INCREMENT,
  codigo_usuario int NOT NULL,
  urlImage varchar(255) NOT NULL,
  explicitImage bool NOT NULL,
  PRIMARY KEY (cod),
  KEY codigo_usuario (codigo_usuario),
  FOREIGN KEY (codigo_usuario) REFERENCES Usuarios (codigo_usuario)
);

create table PontosInterativos (
	cod bigint not null auto_increment,
    servidores_usuarios_servidor int not null,
    servidores_usuarios_usuario int not null,
    PI bigint not null default 1,
    fragmentosPI bigint not null default 0,
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

SET GLOBAL log_bin_trust_function_creators = 1;

delimiter $$
create function verificarCadastro (
		_id_servidor bigint,
		_id_usuario bigint
	) returns int begin 
    declare resultado int;
        if (select count(id_servidor) from Servidores where id_servidor = _id_servidor) = 0 then
		set resultado = 1;
	elseif (select count(id_usuario) from Usuarios where id_usuario = _id_usuario) = 0 then
		set resultado = 2;
	elseif (select count(Servidores_codigo_servidor) from Servidores_Usuarios where Servidores_codigo_servidor = (Select codigo_servidor from Servidores where id_servidor = _id_servidor) and Usuarios_codigo_usuario = (select codigo_usuario from Usuarios where id_usuario = _id_usuario)) = 0 then
		set resultado = 3;
	else
		set resultado = 0;
	end if;
    return resultado;
end$$

create procedure inserirServidor_Usuario (
		in _id_servidor bigint,
        in _nome_servidor varchar(255),
        in _id_usuario bigint,
        in _nome_usuario varchar(255)
	) begin 
	declare opc int;
    set opc = (select verificarCadastro(_id_servidor, _id_usuario));
    while (opc <> 0) do
		if opc = 1 then
			insert into Servidores (id_servidor, nome_servidor) values (_id_servidor, _nome_servidor);
		elseif opc = 2 then
			insert into Usuarios (id_usuario, nome_usuario) values (_id_usuario, _nome_usuario);
		elseif opc = 3 then
			insert into Servidores_Usuarios (Servidores_codigo_servidor, Usuarios_codigo_usuario)  values ((Select codigo_servidor from Servidores where id_servidor = _id_servidor), (select codigo_usuario from Usuarios where id_usuario = _id_usuario));
		end if;
        set opc = (select verificarCadastro(_id_servidor, _id_usuario));
	end while;
end$$

create procedure buscarPrefix (
	in _id_servidor bigint
) begin 
	select Servidores.prefix_servidor from Servidores where Servidores.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

create procedure atualizarPrefix (
	in _id_servidor bigint,
    in _prefix_servidor varchar(25)
) begin 
	declare codigo int;
    set codigo = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
	update Servidores set Servidores.prefix_servidor = _prefix_servidor where Servidores.codigo_servidor = codigo;
    
    select Servidores.prefix_servidor from Servidores where Servidores.id_servidor = _id_servidor;
end$$

create procedure criarAcr(
	in _trigger_acr text,
    in _resposta_acr text,
    in _id_servidor bigint
) begin
	insert into ACRS (trigger_acr, resposta_acr, codigo_servidor) values (_trigger_acr, _resposta_acr, ((select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor)));
    select ACRS.codigo_acr from ACRS where ACRS.trigger_acr = _trigger_acr and ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor) order by ACRS.codigo_acr desc limit 1;
end$$

create procedure deletarAcr(
	in _codigo_acr bigint,
    in _id_servidor bigint
) begin 
	if(select count(_codigo_acr) from ACRS where ACRS.codigo_acr = _codigo_acr and ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor)) = 1 then
		delete from ACRS where ACRS.codigo_acr = _codigo_acr;
        select true as Result;
	else
		select false as Result;
	end if;
end$$

create procedure responderAcr(
	in _trigger_acr text,
    in _id_servidor bigint
) begin
	select ACRS.resposta_acr from ACRS where ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor) and ACRS.trigger_acr = _trigger_acr order by rand() limit 1;
end$$

create procedure listarAcr(
	in _id_servidor bigint
) begin
	select * from ACRS where ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where id_servidor = _id_servidor);
end$$

create procedure AdcAjudanteIdol (
	in _cargo varchar (255),
    in _id_Cargo bigint,
    in _id_Servidor bigint
) begin
	if(select count(Cargos.cod) from Cargos where Cargos.id = _id_Cargo and Cargos.cod_Tipos_Cargos = (select Servidores.codigo_Servidor from Servidores where servidores.id_servidor = _id_Servidor)) = 0 then
		insert into Cargos (cod_Tipos_Cargos, cargo, id, codigo_Servidores) values (1, _cargo, _id_Cargo, (select codigo_Servidor from Servidores where id_servidor = _id_Servidor));
	end if;
end$$

create procedure MostrarAsTetas()
begin
	select Servidores.nome_servidor, Usuarios.nome_usuario from Servidores_Usuarios join Servidores on Servidores.codigo_servidor = Servidores_Usuarios.Servidores_codigo_servidor join Usuarios on Usuarios.codigo_usuario = Servidores_Usuarios.Usuarios_codigo_usuario;
end$$

create procedure AdicionarAdm(
	in _id_Usuario bigint,
    in _permissao int
) begin
	declare result int;
    set result = (select codigo_usuario from Usuarios where id_usuario = _id_Usuario);
	if(select count(cod) from AdmsBot where codigo_Usuario = result) = 0 then
		insert into AdmsBot(codigo_Usuario, permissao) values (result, _permissao);
        select true as Result;
	else
		select false as Result;
	end if;
end$$

create procedure GetAdm (
	in _id_Usuario bigint
) begin
	declare _codUsuario int;
    set _codUsuario = (select codigo_usuario from Usuarios where id_usuario = _id_Usuario);
    if(select count(cod) from AdmsBot where codigo_Usuario = _codUsuario) = 1 then
		select true as Result, permissao from AdmsBot where codigo_Usuario = _codUsuario;
	else
		select false as Result;
	end if;
end$$

create procedure AdicionarInsulto(
	in _idUsuario bigint,
    in _insulto text
) begin
	declare _codUsuario int;
    set _codUsuario = (select codigo_usuario from Usuarios where id_usuario = _idUsuario);
	insert into Insultos (codigo_usuario, insulto) values (_codUsuario, _insulto); 
end$$

create procedure PegarInsulto()
begin
	select Insultos.insulto, Insultos.cod ,Usuarios.id_usuario, Usuarios.nome_usuario from Insultos join Usuarios on Usuarios.codigo_usuario = Insultos.codigo_usuario order by rand() limit 1;
end$$

create procedure AdicionarImgFuck(
	in _idUsuario bigint,
    in _img text,
    in _explicit bool
) begin
	declare _codUsuario int;
    set _codUsuario = (select Usuarios.codigo_usuario from Usuarios where Usuarios.id_usuario = _idUsuario);
    insert into Fuck (Fuck.codigo_usuario, Fuck.urlImage, Fuck.explicitImage) values (_codUsuario, _img, _explicit);
end$$

create procedure GetFuckImg (
	in _explicit bool
) begin 
select Fuck.cod, Fuck.urlImage, Fuck.explicitImage, Usuarios.id_usuario, Usuarios.nome_usuario from Fuck join Usuarios on Usuarios.codigo_usuario = Fuck.codigo_usuario where Fuck.explicitImage = _explicit or Fuck.explicitImage = false order by rand() limit 1;
end$$

create procedure GetPermissoes(
	in _id_servidor bigint
) begin 
	select Servidores.especial_servidor from Servidores where Servidores.id_servidor = _id_servidor;
end$$

create procedure DefinirTipoServidor(
	in _id bigint,
    in _tipoServidor int
) begin
	update Servidores set especial_servidor = _tipoServidor where id_servidor = _id;
    if((select Servidores.especial_servidor from Servidores where Servidores.id_servidor = _id) != 0) then
		select true as Result;
	else
		select false as Result;
	end if;
end $$

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

create function verificarPI(
	_codServidor int,
    _codUsuario int
) returns int begin
	declare _return int;
    set _return = (select count(cod) from pontosinterativos where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario);
    return _return;
end$$

create procedure CriarPI(
	in _codServidor int,
	in _codUsuario int
) begin
	if((select verificarPI(_codServidor, _codUsuario)) = 0) then
		insert into pontosinterativos (servidores_usuarios_servidor, servidores_usuarios_usuario) values (_codServidor, _codUsuario);
	end if;
end$$
	
create procedure LevelUP(
	in _codServidor int,
    in _codUsuario int
)begin
	declare _multi double;
    declare _fragmento bigint;
    declare _levelAtual int;
    set _multi = (select PIrate from configuracoesservidores where cod_servidor = _codServidor);
    set _fragmento = (select fragmentosPI from pontosinterativos where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario);
    set _levelAtual = (select pontosinterativos.PI from pontosinterativos where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario);
    if(_fragmento >= (_levelAtual * (_multi * 10))) then
		update pontosinterativos set pontosinterativos.PI = (pontosinterativos.PI + 1), fragmentosPI = 0 where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario;
	end if;
end$$
    
create procedure AddPI(
	in _idServidor bigint,
    in _idUsuario bigint
) begin
	declare _codServidor int;
    declare _codUsuario int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
    if((select verificarConfig (_codServidor)) > 0 and (select PIConf from configuracoesservidores where cod_servidor = _codServidor)) then
		set _codUsuario = (select codigo_usuario from Usuarios where id_usuario = _idUsuario);
		call CriarPI(_codServidor, _codUsuario);
        update pontosinterativos set fragmentosPI = (fragmentosPI + 1) where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario;
        call LevelUP(_codServidor, _codUsuario);
	end if;
end$$



delimiter ;


call AddPI(556580866198077451, 274289097689006080);

call AdicionarImgFuck(368280970102833153, "https://i.imgur.com/rtG8cwh.gif", true);
call GetFuckImg(true);

call atualizarPrefix(556580866198077451, "!");

call DefinirTipoServidor(556580866198077451, 1);

call criarAcr("gado", "thhhrag", 518069575896793109);
call AdcAjudanteIdol("pitas viado", 32, 556580866198077451);
select count(Cargos.cod) from Cargos where Cargos.cargo = "oi" and Cargos.cod_Tipos_Cargos = (select Servidores.codigo_Servidor from Servidores where servidores.id_servidor = 91);
((select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = 518069575896793109));
call deletarAcr(10, 518069575896793109);
call responderAcr("oi", 549064112651370506);
select ACRS.resposta_acr from ACRS where ACRS.trigger_acr = "oi" and 549064112651370506 = (select Servidores.id_servidor from Servidores where Servidores.codigo_servidor = (select ACRS.codigo_servidor from ACRS where ACRS.trigger_acr ="oi" order by rand() limit 1)) order by rand() limit 1;
select ACRS.codigo_servidor from ACRS where ACRS.trigger_acr ="oi" order by rand() limit 1;
call listarAcr(556580866198077451);
call procurarAcr("aaaaa");
call AdicionarAdm(368280970102833153, 0);
select verificarConfig(556580866198077451);