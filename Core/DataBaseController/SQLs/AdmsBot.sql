create procedure LerAdms(
	in _usuario bigint
)begin
	SELECT a.* from AdmsBot a join Usuarios u on u.codigo_usuario = a.usuario where u.id_usuario = _usuario LIMIT 1;
end;

create procedure AtualizarAdm(
	in _usuario bigint,
	in _permissao tinyint
)begin
	insert into AdmsBot (usuario, permissao) values ((select GetCodUser(_usuario )), _permissao ) ON DUPLICATE KEY UPDATE permissao = _permissao;
end;
