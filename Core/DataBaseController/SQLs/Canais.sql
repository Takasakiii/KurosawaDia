create procedure GetCanal(
	in _servidor bigint,
	in _tipo tinyint
)begin
	select * from Canais where codigo_servidor = GetCodServidor(_servidor) and TipoCanal = _tipo;
end;