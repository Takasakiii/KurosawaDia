CREATE PROCEDURE GetServidor(
	IN _servidor bigint
)BEGIN
	SELECT * FROM Servidores where id_servidor = _servidor;
end;

create procedure AtualizarServidor(
	in _servidor bigint,
	in _prefix varchar(25),
	in _especial tinyint
) begin
	if(_prefix <> "") then
		update Servidores set prefix_servidor = _prefix;
	end if;
	if(_especial <> 0) then
		update Servidores set especial_servidor = _especial;
	end if;
end;

