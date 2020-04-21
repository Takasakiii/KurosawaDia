create procedure GetCanal(
	in _servidor bigint,
	in _tipo tinyint
)begin
	select * from Canais  where codigo_servidor = _servidor and TipoCanal = _tipo limit 1;
end;

create procedure AddCanal(
	in _tipo tinyint,
	in _nome varchar(255),
	in _id bigint,
	in _servidor bigint
)begin
	if((select count(cod) from Canais where codigo_servidor = _servidor and TipoCanal = _tipo) = 0) then 
		insert into Canais (codigo_servidor, id, nome, TipoCanal) values (_servidor, _id, _nome, _tipo);
	else
		delete from Canais where codigo_servidor = _servidor and TipoCanal = _tipo;
	end if;
end;

