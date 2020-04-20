create procedure GetFuck(
	in _explicit bool
)
begin
	if (_explicit) then
		select * from Fuck order by rand() limit 1;
	else
		select * from Fuck where explicitImage = false order by rand() limit 1;
	end if;
end;

create procedure AddFuck(
	in _usuario bigint,
	in _url varchar(255),
	in _explicit bool
) begin
	insert into Fuck (codigo_usuario , urlImage, explicitImage) values (GetCodUser(_usuario), _url, _explicit );
end;