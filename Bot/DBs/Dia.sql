/*
   __  ___               _                ____  ____    __         
  /  |/  / ___ _  ____  (_)  ___  ___ _  / __/ / __ \  / /         
 / /|_/ / / _ `/ / __/ / /  / _ \/ _ `/ _\ \  / /_/ / / /__        
/_/  /_/  \_,_/ /_/   /_/  /_//_/\_,_/ /___/  \___\_\/____/ by Takasaki    


Arquivos unidos na ordem: Dia_BaseTables_1.sql Dia_MonitoringProcedures_2.sql Dia_Insultos_2.sql Dia_Fuck_2.sql Dia_ConfigServer_2.sql Dia_Cargos_2.sql Dia_Canais_2.sql Dia_ADMS_2.sql Dia_Acr_2.sql Dia_PI_3.sql  
*/




#Dia_BaseTables_1.sql
delimiter ; 

delimiter ;

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

create table Usuarios (
	codigo_usuario int not null auto_increment,
	id_usuario bigint not null unique key,
    nome_usuario varchar(255) not null,
    primary key (codigo_usuario)
);

create table Servidores_Usuarios (
	Servidores_codigo_servidor int not null,
    Usuarios_codigo_usuario int not null,
    foreign key (Servidores_codigo_servidor) references Servidores (codigo_servidor),
    foreign key (Usuarios_codigo_usuario) references Usuarios (codigo_usuario),
    primary key (Servidores_codigo_servidor, Usuarios_codigo_usuario)
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

create procedure DefinirTipoServidor(
	in _id bigint,
    in _tipoServidor int
) begin
	update Servidores set especial_servidor = _tipoServidor where id_servidor = _id;
    if((select Servidores.especial_servidor from Servidores where Servidores.id_servidor = _id) = _tipoServidor) then
		select true as Result;
	else
		select false as Result;
	end if;
end $$

create procedure GetPermissoes(
	in _id_servidor bigint
) begin 
	select Servidores.especial_servidor from Servidores where Servidores.id_servidor = _id_servidor;
end$$


#Dia_MonitoringProcedures_2.sql
delimiter ; 

delimiter $$
create procedure MostrarAsTetas()
begin
	select Servidores.nome_servidor, Usuarios.nome_usuario from Servidores_Usuarios join Servidores on Servidores.codigo_servidor = Servidores_Usuarios.Servidores_codigo_servidor join Usuarios on Usuarios.codigo_usuario = Servidores_Usuarios.Usuarios_codigo_usuario;
end$$

create procedure MandaOsNude (
    in _codigo_usuario bigint
) begin 
    select Servidores.codigo_servidor, Servidores.nome_servidor, Servidores.id_servidor, " " as separador, Usuarios.codigo_usuario, Usuarios.nome_usuario, Usuarios.id_usuario from Servidores_Usuarios join Servidores on Servidores.codigo_servidor = Servidores_Usuarios.Servidores_codigo_servidor join Usuarios on Usuarios.codigo_usuario = Servidores_Usuarios.Usuarios_codigo_usuario where Usuarios.codigo_usuario = _codigo_usuario;
end$$



#Dia_Insultos_2.sql
delimiter ; 

delimiter ;
create table Insultos(
	cod bigint not null auto_increment,
    codigo_usuario int not null,
    insulto text not null,
    foreign key (codigo_usuario) references Usuarios(codigo_usuario),
    primary key (cod)
);

delimiter $$

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



#Dia_Fuck_2.sql
delimiter ; 

delimiter ;
CREATE TABLE Fuck (
  cod bigint NOT NULL AUTO_INCREMENT,
  codigo_usuario int NOT NULL,
  urlImage varchar(255) NOT NULL,
  explicitImage bool NOT NULL,
  PRIMARY KEY (cod),
  KEY codigo_usuario (codigo_usuario),
  FOREIGN KEY (codigo_usuario) REFERENCES Usuarios (codigo_usuario)
);

delimiter $$

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



#Dia_ConfigServer_2.sql
delimiter ; 

delimiter ;
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


#Dia_Cargos_2.sql
delimiter ; 

delimiter ;
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
    foreign key (codigo_Servidores) references Servidores (codigo_servidor),
    primary key (cod)
);
alter table Cargos add column requesito bigint not null default 0;

delimiter $$

create procedure AdcAjudanteIdol (
	in _cargo varchar (255),
    in _id_Cargo bigint,
    in _id_Servidor bigint
) begin
	if(select count(Cargos.cod) from Cargos where Cargos.id = _id_Cargo and Cargos.cod_Tipos_Cargos = (select Servidores.codigo_Servidor from Servidores where servidores.id_servidor = _id_Servidor)) = 0 then
		insert into Cargos (cod_Tipos_Cargos, cargo, id, codigo_Servidores) values (1, _cargo, _id_Cargo, (select codigo_Servidor from Servidores where id_servidor = _id_Servidor));
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




#Dia_Canais_2.sql
delimiter ; 

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

delimiter $$

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


#Dia_ADMS_2.sql
delimiter ; 

delimiter ;
create table AdmsBot(
	cod bigint not null auto_increment,
    codigo_Usuario int not null,
    permissao int not null,
    foreign key (codigo_Usuario) references Usuarios (codigo_usuario),
    primary key (cod)
);

delimiter $$

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


#Dia_Acr_2.sql
delimiter ; 

delimiter ;
create table ACRS (
	codigo_acr bigint not null auto_increment,
    trigger_acr text not null,
    resposta_acr text not null,
    codigo_servidor int not null,
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (codigo_acr)
);

delimiter $$

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


#Dia_PI_3.sql
delimiter ; 

delimiter ;
create table PontosInterativos (
	cod bigint not null auto_increment,
    Servidores_Usuarios_servidor int not null,
    Servidores_Usuarios_usuario int not null,
    PI bigint not null default 1,
    fragmentosPI bigint not null default 0,
    foreign key (Servidores_Usuarios_servidor, Servidores_Usuarios_usuario) references Servidores_Usuarios(Servidores_codigo_servidor, Usuarios_codigo_usuario),
    primary key (cod)
);

delimiter $$
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

