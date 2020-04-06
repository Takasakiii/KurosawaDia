CREATE function GetCodUser(
	_usuario bigint
)returns bigint begin
	return (select codigo_usuario from Usuarios where id_usuario = _usuario);
end;

CREATE  function GetCodServidor(
	_servidor bigint
) returns bigint begin
	return (select codigo_servidor from Servidores where id_servidor = _servidor);
end;



CREATE function CheckUsuarioServidor(
	_servidor bigint,
	_usuario bigint
)returns tinyint begin
	declare _res tinyint;
	if((select count(codigo_servidor) from Servidores where id_servidor = _servidor) = 0) then
		set _res = 3;
	elseif((select count(codigo_usuario) from Usuarios where id_usuario = _usuario) = 0) then
		set _res = 2;
	elseif((select count(Usuarios_codigo_usuario) from Servidores_Usuarios where Servidores_codigo_servidor = GetCodServidor(_servidor) and Usuarios_codigo_usuario = GetCodUser(_usuario)) = 0) then
		set _res = 1;
	else
		set _res = 0;
	end if;
	return _res;
end;

create procedure CadastrarUsuarioServidor(
	in _servidor bigint,
	in _usuario bigint,
	in _nome_s varchar(255),
	in _nome_u varchar(255)
) begin
	declare _r tinyint;
	set _r = 3;
	while(_r <> 0) do
		set _r = (select CheckUsuarioServidor(_servidor, _usuario));
		if(_r = 3) then
			insert into Servidores (id_servidor, nome_servidor) values (_servidor, _nome_s);
		elseif(_r = 2) then
			insert into Usuarios (id_usuario, nome_usuario) values (_usuario, _nome_u);
		elseif(_r = 1) then
			insert into Servidores_Usuarios values ((select GetCodServidor(_servidor)), (select GetCodUser(_usuario)));
		end if;
	end while;
end;


#SELECT b FROM a 
#WHERE 'aaaaaaaaaashibaputinha' LIKE
# CONCAT('%', b, '%');