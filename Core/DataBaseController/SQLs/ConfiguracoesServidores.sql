create procedure SetServerConfig(
	in _servidor bigint,
	in _config int,
	in _value text
) begin
	declare _codServ bigint;
	set _codServ = (SELECT GetCodServidor(_servidor));
	if((select count(cod) from ConfiguracoesServidoresAplicada where servidor = _codServ and configuracoes = _config) = 0) then
		insert into ConfiguracoesServidoresAplicada (servidor, configuracoes, valor) values (_codServ, _config, _value);
	else
		UPDATE ConfiguracoesServidoresAplicada set valor = _value where configuracoes = _config and servidor = _codServ;
	end if;
end;


create procedure GetServerConfig(
	in _servidor bigint,
	in _config int
)begin
	select * from ConfiguracoesServidoresAplicada where servidor = GetCodServidor(_servidor ) and configuracoes = _config;
end;

