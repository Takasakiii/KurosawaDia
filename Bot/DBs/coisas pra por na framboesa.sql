insert into Tipos_Cargos values (2, "Cargos por XP (XpRole)");
alter table Cargos add column requesito bigint not null default 0;

#atualizar

create table PontosInterativos (
	cod bigint not null auto_increment,
    Servidores_Usuarios_servidor int not null,
    Servidores_Usuarios_usuario int not null,
    PI bigint not null default 1,
    fragmentosPI bigint not null default 0,
    foreign key (Servidores_Usuarios_servidor, Servidores_Usuarios_usuario) references Servidores_Usuarios(Servidores_codigo_servidor, Usuarios_codigo_usuario),
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
    set _return = (select count(cod) from ConfiguracoesServidores where cod_servidor = _codServidor);
    return _return;
end$$

create procedure criarConfig(
	in _codServidor int
) begin
	if((select verificarConfig(_codServidor)) = 0) then
		insert into ConfiguracoesServidores (cod_servidor) values (_codServidor);
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
    update ConfiguracoesServidores set PIConf = _piconf where cod_servidor = _cod;
    update ConfiguracoesServidores set PIrate = _pirate where cod_servidor = _cod;
    if (_msgPiup <> "") then
		update ConfiguracoesServidores set MsgPIUp = _msgPiup where cod_servidor = _cod;
	else
		update ConfiguracoesServidores set MsgPIUp = NULL where cod_servidor = _cod;
	end if;
end$$


create function verificarPI(
	_codServidor int,
    _codUsuario int
) returns int begin
	declare _return int;
    set _return = (select count(cod) from PontosInterativos where Servidores_Usuarios_servidor = _codServidor and Servidores_Usuarios_usuario = _codUsuario);
    return _return;
end$$

create procedure CriarPI(
	in _codServidor int,
	in _codUsuario int
) begin
	if((select verificarPI(_codServidor, _codUsuario)) = 0) then
		insert into PontosInterativos (Servidores_Usuarios_servidor, Servidores_Usuarios_usuario) values (_codServidor, _codUsuario);
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
    set _multi = (select PIrate from ConfiguracoesServidores where cod_servidor = _codServidor);
    set _fragmento = (select fragmentosPI from PontosInterativos where Servidores_Usuarios_servidor = _codServidor and Servidores_Usuarios_usuario = _codUsuario);
    set _levelAtual = (select PontosInterativos.PI from PontosInterativos where Servidores_Usuarios_servidor = _codServidor and Servidores_Usuarios_usuario = _codUsuario);
    if(_fragmento >= (_levelAtual * (_multi * 10))) then
		update PontosInterativos set PontosInterativos.PI = (PontosInterativos.PI + 1), fragmentosPI = 0 where Servidores_Usuarios_servidor = _codServidor and Servidores_Usuarios_usuario = _codUsuario;
        set _cargoID = (select id from Cargos where cod_Tipos_Cargos = 2 and codigo_Servidores = _codServidor and requesito = _levelAtual);
        select true as Upou, _levelAtual as LevelAtual, MsgPIUp, _cargoID as CargoID from ConfiguracoesServidores where cod_servidor = _codServidor;
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
    if((select verificarConfig (_codServidor)) > 0 and (select PIConf from ConfiguracoesServidores where cod_servidor = _codServidor)) then
		set _codUsuario = (select codigo_usuario from Usuarios where id_usuario = _idUsuario);
		call CriarPI(_codServidor, _codUsuario);
        update PontosInterativos set fragmentosPI = (fragmentosPI + 1) where Servidores_Usuarios_servidor = _codServidor and Servidores_Usuarios_usuario = _codUsuario;
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
    update ConfiguracoesServidores set bemvindoMsg = _bemvindoMsg where cod_servidor = _codServidor;
end$$

create procedure SetGoodBye(
	in _idServidor bigint,
    in _msg text
)begin
	declare _codServidor int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
    call criarConfig(_codServidor);
    update ConfiguracoesServidores set sairMsg = _msg where cod_servidor = _codServidor;
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
	select Canais.cod, Canais.cod_Tipos_Canais, nome, id, Servidores.id_servidor, Servidores.nome_servidor from Canais join Servidores on Servidores.codigo_servidor = Canais.codigo_servidor where Canais.cod_Tipos_Canais = _tipo_canal and Canais.codigo_servidor = (Select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

create procedure GetWelcomeMsg(
	in _idServidor bigint
) begin
	select bemvindoMsg from ConfiguracoesServidores where ConfiguracoesServidores.Cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
end$$

create procedure GetByeMsg(
	in _idServidor bigint
) begin
	select sairMsg from ConfiguracoesServidores where ConfiguracoesServidores.Cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
end$$

create procedure getErrorMessage (
	in _id_servidor bigint
) begin 
	declare _cod_servidor int;
    set _cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
    
    if((select verificarConfig(_cod_servidor)) <> 0) then
		select ConfiguracoesServidores.msgError from ConfiguracoesServidores where ConfiguracoesServidores.cod_servidor = _cod_servidor;
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
    update ConfiguracoesServidores set msgError = _erroMsg where cod_servidor = _codServidor;
end$$

create procedure GetPiInfo (
	in _idUsuario bigint,
    in _idServidor bigint
) begin 
	declare _codServidor int;
    declare _codUsuario int;
	declare _pontos bigint;
    set _codServidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
    set _codUsuario = (select Usuarios.codigo_usuario from Usuarios where Usuarios.id_usuario = _idUsuario);
    
    if((select verificarConfig(_codServidor)) > 0 and (select PIconf from ConfiguracoesServidores where cod_servidor = _codServidor)) then
        set _pontos = ((select PontosInterativos.PI from PontosInterativos where PontosInterativos.Servidores_Usuarios_servidor = _codServidor and PontosInterativos.Servidores_Usuarios_usuario = _codUsuario) * 10 * (select ConfiguracoesServidores.PIrate from ConfiguracoesServidores where ConfiguracoesServidores.cod_servidor =  (select Servidores_Usuarios_servidor from PontosInterativos where PontosInterativos.Servidores_Usuarios_servidor = _codServidor and PontosInterativos.Servidores_Usuarios_usuario = _codUsuario)));
		select PontosInterativos.PI, PontosInterativos.fragmentosPI, _pontos as Total, cod from PontosInterativos where PontosInterativos.Servidores_Usuarios_servidor = _codServidor and PontosInterativos.Servidores_Usuarios_usuario = _codUsuario;
    end if;
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
    nome varchar(255) not null,
    id bigint not null,
    codigo_servidor int not null,
    foreign key (cod_Tipos_Canais) references Tipos_Canais (cod),
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);
