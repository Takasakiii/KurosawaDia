create procedure GetCanal(
	in _servidor bigint,
	in _tipo tinyint
)begin
	select * from Canais where codigo_servidor = GetCodServidor(_servidor) and TipoCanal = _tipo;
end;

create procedure AddCanal(
	in _tipo tinyint,
	in _nome varchar(255),
	in _id bigint,
	in _servidor bigint
)begin
	declare _codServ bigint;
	set _codServ = (select GetCodServidor(_servidor));
	if((select count(cod) from Canais where codigo_servidor = _codServ and TipoCanal = _tipo) = 0) then 
		insert into Canais (codigo_servidor, id, nome, TipoCanal) values (_codServ, _id, _nome, _tipo);
	else
		delete from Canais where codigo_servidor = _codServ and TipoCanal = _tipo;
	end if;
end;
