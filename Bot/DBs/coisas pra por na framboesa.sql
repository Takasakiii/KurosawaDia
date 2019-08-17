insert into Tipos_Cargos values (2, "Cargos por XP (XpRole)");
alter table Cargos add column requesito bigint not null default 0;
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
    if (_msgPiup <> "") then
		update configuracoesservidores set MsgPIUp = _msgPiup where cod_servidor = _cod;
	else
		update configuracoesservidores set MsgPIUp = NULL where cod_servidor = _cod;
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
    declare _cargoID bigint;
    set _multi = (select PIrate from configuracoesservidores where cod_servidor = _codServidor);
    set _fragmento = (select fragmentosPI from pontosinterativos where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario);
    set _levelAtual = (select pontosinterativos.PI from pontosinterativos where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario);
    if(_fragmento >= (_levelAtual * (_multi * 10))) then
		update pontosinterativos set pontosinterativos.PI = (pontosinterativos.PI + 1), fragmentosPI = 0 where servidores_usuarios_servidor = _codServidor and servidores_usuarios_usuario = _codUsuario;
        set _cargoID = (select id from Cargos where cod_Tipos_Cargos = 2 and codigo_Servidores = _codServidor and requesito = _levelAtual);
        select true as Upou, _levelAtual as LevelAtual, MsgPIUp, _cargoID as CargoID from configuracoesservidores where cod_servidor = _codServidor;
	else
		select false as Upou;
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

create function verificarCargo(
	_idCargo bigint,
    _codServidor bigint
) returns int begin
	declare _retorno int;
    set _retorno = (select count(cod) from Cargos where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor);
    return _retorno;
end$$


create procedure AdicionarAtualizarCargoIP(
	in _cargo varchar(255),
    in _idCargo bigint,
    in _idServidor bigint,
    in _IPLevel bigint
) begin
	declare _codServidor int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
	if (_IPLevel > 0)then
		if((select verificarCargo(_idCargo, _codServidor)) = 0 ) then
			insert into Cargos (cod_Tipos_Cargos, cargo, id, codigo_Servidores, requesito) values (2, _cargo, _idCargo, _codServidor, _IPLevel);
			select 1 as tipoOperacao;
		else
			update Cargos set requesito = _IPLevel where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor;
			select 2 as tipoOperacao;
		end if;
	else
		delete from Cargos where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor;
        select 3 as tipoOperacao;
	end if;
end$$

create procedure SetWelcomeMsg(
	in _idServidor bigint,
    in _bemvindoMsg text
)begin
	declare _codServidor int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
    call criarConfig(_codServidor);
    update configuracoesservidores set bemvindoMsg = _bemvindoMsg where cod_servidor = _codServidor;
end$$

create procedure SetGoodBye(
	in _idServidor bigint,
    in _msg text
)begin
	declare _codServidor int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
    call criarConfig(_codServidor);
    update configuracoesservidores set sairMsg = _msg where cod_servidor = _codServidor;
end$$
    


#pitas

create function verificarCh(
	 _tipo_canal int,
     _cod_servidor int
) returns int begin
	declare _return int;
    set _return = (select count(Canais.cod) from Canais where Canais.codigo_servidor = _cod_servidor and Canais.cod_Tipos_Canais = _tipo_canal);
    return _return;
end$$

create procedure AdcCh (
	in _tipo_canal int,
	in _nome varchar (255),
    in _id_canal bigint,
    in _cod_servidor bigint
) begin
	if((select verificarCh(_tipo_canal, _cod_servidor) = 0)) then 
		insert into Canais (cod_tipos_Canais, nome, id, codigo_servidor) values (_tipo_canal, _nome, _id_canal, _cod_servidor);
    end if;
end $$

create procedure setCh(
	in _tipo_canal int,
    in _nome varchar (255),
    in _id_canal bigint,
    in _id_servidor bigint
) begin 
	declare cod_servidor int;
    set cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
	call AdcCh(_tipo_canal, _nome, _id_canal, cod_servidor);
    update Canais set id = _id_canal, nome = _nome where Canais.codigo_servidor = cod_servidor and Canais.cod_Tipos_Canais = _tipo_canal;
    
    if((select Canais.id from Canais where Canais.codigo_servidor = cod_servidor and Canais.cod_Tipos_Canais = _tipo_canal) = _id_canal) then
		select true as result;
    else
		select false as result;
    end if;
end$$

create procedure GetCh (
	in _tipo_canal bigint,
    in _id_servidor bigint
) begin 
	select Canais.cod, Canais.cod_Tipos_Canais, nome, id, servidores.id_servidor, Servidores.nome_servidor from Canais join servidores on Servidores.codigo_servidor = Canais.codigo_servidor where Canais.cod_Tipos_Canais = _tipo_canal and Canais.codigo_servidor = (Select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

create procedure GetWelcomeMsg(
	in _idServidor bigint
) begin
	select bemvindoMsg from configuracoesservidores where configuracoesservidores.Cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
end$$

create procedure GetByeMsg(
	in _idServidor bigint
) begin
	select sairMsg from configuracoesservidores where configuracoesservidores.Cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
end$$

create procedure getErrorMessage (
	in _id_servidor bigint
) begin 
	declare _cod_servidor int;
    set _cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
    
    if((select verificarConfig(_cod_servidor)) <> 0) then
		select configuracoesservidores.msgError from ConfiguracoesServidores where ConfiguracoesServidores.cod_servidor = _cod_servidor;
    else
		select true as msgError;
    end if;
end$$

create procedure SetErroMsg (
	in _idServidor bigint,
    in _erroMsg bool
) begin
	declare _codServidor int;
	set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
	call criarConfig(_codServidor);
    update configuracoesservidores set msgError = _erroMsg where cod_servidor = _codServidor;
end$$

delimiter ;

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
