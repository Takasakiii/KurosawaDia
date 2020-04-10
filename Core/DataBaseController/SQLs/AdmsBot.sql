create procedure LerAdms(
	in _usuario bigint
)begin
	SELECT * from AdmsBot where cod = GetCodUser(_usuario);
end;