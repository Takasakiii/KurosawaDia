create procedure LerAdms(
	in _usuario bigint
)begin
	SELECT * from AdmsBot where usuario = GetCodUser(_usuario);
end;

create procedure AtualizarAdm(
	in _usuario bigint,
	in _permissao tinyint
)begin
	declare _cod_usuario bigint;
	set _cod_usuario = (select GetCodUser(_usuario));
	if ((select count(cod) from AdmsBot where usuario = _cod_usuario) = 0) then
		insert into AdmsBot (usuario, permissao) values (_cod_usuario, _permissao);
	else
		update AdmsBot set permissao = _permissao where usuario = _cod_usuario;
	end if;
end;