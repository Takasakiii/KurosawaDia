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
