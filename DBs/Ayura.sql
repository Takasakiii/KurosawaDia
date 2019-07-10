create database Pitas_Kurosawa;
use Pitas_Kurosawa;

create table Servidores (
	codigo_servidor int not null auto_increment,
	id_servidor bigint not null unique key,
    nome_servidor varchar(255) not null,
    especial_servidor boolean not null default false,
    prefix_servidor varchar(25) not null default "'",
    primary key (codigo_servidor)
);

create table ACRS (
	codigo_acr int not null auto_increment,
    trigger_acr text not null,
    resposta_acr text not null,
    codigo_servidor int not null,
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (codigo_acr)
);

create table Usuarios (
	codigo_usuario int not null auto_increment,
	id_usuario bigint not null unique key,
    nome_usuario varchar(255) not null,
    primary key (codigo_usuario)
);

create table Servidores_Usuarios (
	Servidores_codigo_servidor int not null,
    Usuarios_codigo_usuario int not null,
    foreign key (Servidores_codigo_servidor) references Servidores (codigo_servidor),
    foreign key (Usuarios_codigo_usuario) references Usuarios (codigo_usuario),
    primary key (Servidores_codigo_servidor, Usuarios_codigo_usuario)
);

delimiter $$
create function verificarCadastro (
		_id_servidor bigint,
		_id_usuario bigint
	) returns int begin 
    declare resultado int;
        if (select count(id_servidor) from Servidores where id_servidor = _id_servidor) = 0 then
		set resultado = 1;
	elseif (select count(id_usuario) from Usuarios where id_usuario = _id_usuario) = 0 then
		set resultado = 2;
	elseif (select count(Servidores_codigo_servidor) from Servidores_Usuarios where Servidores_codigo_servidor = (Select codigo_servidor from Servidores where id_servidor = _id_servidor) and Usuarios_codigo_usuario = (select codigo_usuario from Usuarios where id_usuario = _id_usuario)) = 0 then
		set resultado = 3;
	else
		set resultado = 0;
	end if;
    return resultado;
end$$

create procedure inserirServidor_Usuario (
		in _id_servidor bigint,
        in _nome_servidor varchar(255),
        in _id_usuario bigint,
        in _nome_usuario varchar(255)
	) begin 
	declare opc int;
    set opc = (select verificarCadastro(_id_servidor, _id_usuario));
    while (opc <> 0) do
		if opc = 1 then
			insert into Servidores (id_servidor, nome_servidor) values (_id_servidor, _nome_servidor);
		elseif opc = 2 then
			insert into Usuarios (id_usuario, nome_usuario) values (_id_usuario, _nome_usuario);
		elseif opc = 3 then
			insert into Servidores_Usuarios (Servidores_codigo_servidor, Usuarios_codigo_usuario)  values ((Select codigo_servidor from Servidores where id_servidor = _id_servidor), (select codigo_usuario from Usuarios where id_usuario = _id_usuario));
		end if;
        set opc = (select verificarCadastro(_id_servidor, _id_usuario));
	end while;
end$$

create procedure buscarPrefix (
	in _id_servidor bigint
) begin 
	select Servidores.prefix_servidor from Servidores where Servidores.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

create procedure atualizarPrefix (
	in _id_servidor bigint,
    in _prefix_servidor varchar(25)
) begin 
	declare codigo int;
    set codigo = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
	update Servidores set Servidores.prefix_servidor = _prefix_servidor where Servidores.codigo_servidor = codigo;
end$$

create procedure criarAcr(
	in _trigger_acr text,
    in _resposta_acr text,
    in _id_servidor bigint
) begin
	insert into ACRS (trigger_acr, resposta_acr, codigo_servidor) values (_trigger_acr, _resposta_acr, ((select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor)));
end$$

create procedure deletarAcr(
	in _codigo_acr int
) begin 
	delete from ACRS where ACRS.codigo_acr = _codigo_acr;
end$$

create procedure responderAcr(
	in _trigger_acr text,
    in _id_servidor bigint
) begin
	select ACRS.resposta_acr from ACRS where ACRS.trigger_acr = _trigger_acr and _id_servidor = (select Servidores.id_servidor from Servidores where Servidores.codigo_servidor = (select ACRS.codigo_servidor from ACRS where ACRS.trigger_acr = _trigger_acr order by rand() limit 1)) order by rand() limit 1;
end$$

create procedure listarAcr(
	in _id_servidor bigint
) begin
	select * from ACRS where ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where id_servidor = _id_servidor);
end$$

delimiter ;
call atualizarPrefix(556580866198077451, "'");

call criarAcr("oi", "boa noite", 556580866198077451);
call deletarAcr(4);
call responderAcr("aa", 556580866198077451);
call listarAcr(556580866198077451);

select * from ACRS where