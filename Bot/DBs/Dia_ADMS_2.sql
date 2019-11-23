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
	else
		update AdmsBot set permissao = _permissao where codigo_Usuario = result;
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