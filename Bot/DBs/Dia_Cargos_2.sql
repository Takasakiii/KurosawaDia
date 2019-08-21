delimiter ;
create table Tipos_Cargos(
	cod bigint not null,
    Descricao varchar (255) not null unique, 
    primary key (cod)
);

#Setup Tipos_Cargos
insert into Tipos_Cargos values (1, "Permissao Extendida (Ajudante de Idol)");
insert into Tipos_Cargos values (2, "Cargos por XP (XpRole)");

create table Cargos(
	cod bigint not null auto_increment,
    cod_Tipos_Cargos bigint not null,
    cargo varchar (255) not null,
    id bigint not null,
    codigo_Servidores int not null,
    foreign key (cod_Tipos_Cargos) references Tipos_Cargos (cod),
    foreign key (codigo_Servidores) references Servidores (codigo_servidor),
    primary key (cod)
);
alter table Cargos add column requesito bigint not null default 0;

delimiter $$

create procedure AdcAjudanteIdol (
	in _cargo varchar (255),
    in _id_Cargo bigint,
    in _id_Servidor bigint
) begin
	if(select count(Cargos.cod) from Cargos where Cargos.id = _id_Cargo and Cargos.cod_Tipos_Cargos = (select Servidores.codigo_Servidor from Servidores where servidores.id_servidor = _id_Servidor)) = 0 then
		insert into Cargos (cod_Tipos_Cargos, cargo, id, codigo_Servidores) values (1, _cargo, _id_Cargo, (select codigo_Servidor from Servidores where id_servidor = _id_Servidor));
	end if;
end$$

create function verificarCargo(
	_idCargo bigint,
    _codServidor bigint
) returns int begin
	declare _retorno int;
    set _retorno = (select count(cod) from Cargos where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor);
    return _retorno;
end$$


create procedure AdicionarAtualizarCargoIP(
	in _cargo varchar(255),
    in _idCargo bigint,
    in _idServidor bigint,
    in _IPLevel bigint
) begin
	declare _codServidor int;
    set _codServidor = (select codigo_servidor from Servidores where id_servidor = _idServidor);
	if (_IPLevel > 0)then
		if((select verificarCargo(_idCargo, _codServidor)) = 0 ) then
			insert into Cargos (cod_Tipos_Cargos, cargo, id, codigo_Servidores, requesito) values (2, _cargo, _idCargo, _codServidor, _IPLevel);
			select 1 as tipoOperacao;
		else
			update Cargos set requesito = _IPLevel where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor;
			select 2 as tipoOperacao;
		end if;
	else
		delete from Cargos where cod_Tipos_Cargos = 2 and id = _idCargo and codigo_Servidores = _codServidor;
        select 3 as tipoOperacao;
	end if;
end$$


