delimiter ;
create table PontosInterativos (
	cod bigint not null auto_increment,
    servidores_usuarios_servidor int not null,
    servidores_usuarios_usuario int not null,
    PI bigint not null default 1,
    fragmentosPI bigint not null default 0,
    foreign key (servidores_usuarios_servidor, servidores_usuarios_usuario) references servidores_usuarios(Servidores_codigo_servidor, Usuarios_codigo_usuario),
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

create procedure GetPiInfo (
	in _idUsuario bigint,
    in _idServidor bigint
) begin 
	declare _codServidor int;
    declare _codUsuario int;
	declare _pontos bigint;
    set _codServidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _idServidor);
    set _codUsuario = (select Usuarios.codigo_usuario from Usuarios where Usuarios.id_usuario = _idUsuario);
    
    if((select verificarConfig(_codServidor)) > 0 and (select PIconf from configuracoesservidores where cod_servidor = _codServidor)) then
        set _pontos = ((select pontosinterativos.PI from pontosinterativos where pontosinterativos.servidores_usuarios_servidor = _codServidor and pontosinterativos.servidores_usuarios_usuario = _codUsuario) * 10 * (select configuracoesservidores.PIrate from configuracoesservidores where configuracoesservidores.cod_servidor =  (select servidores_usuarios_servidor from pontosinterativos where pontosinterativos.servidores_usuarios_servidor = _codServidor and pontosinterativos.servidores_usuarios_usuario = _codUsuario)));
		select pontosinterativos.PI, pontosinterativos.fragmentosPI, _pontos as Total from pontosinterativos where pontosinterativos.servidores_usuarios_servidor = _codServidor and pontosinterativos.servidores_usuarios_usuario = _codUsuario;
    end if;
end$$


