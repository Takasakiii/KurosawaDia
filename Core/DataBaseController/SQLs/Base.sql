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


create procedure CadastrarUsuarioServidor(
	in _servidor bigint,
	in _usuario bigint,
	in _nome_s varchar(255),
	in _nome_u varchar(255)
) begin
	insert ignore into Servidores (id_servidor, nome_servidor) values (_servidor, _nome_s);
	insert ignore into Usuarios (id_usuario, nome_usuario) values (_usuario, _nome_u);
	insert ignore into Servidores_Usuarios values ((select GetCodServidor(_servidor)), (select GetCodUser(_usuario)));
	call GetServidor(_servidor); 
end;
