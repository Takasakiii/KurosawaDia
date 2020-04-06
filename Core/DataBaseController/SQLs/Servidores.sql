CREATE PROCEDURE GetServidor(
	IN _servidor bigint
)BEGIN
	SELECT * FROM Servidores where id_servidor = _servidor;
end;