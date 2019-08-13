delimiter ;
create table Tipos_Canais (
	cod bigint not null,
    Descricao varchar (255) not null unique,
    primary key (cod)
);

#Set dos canais yay
insert into Tipos_Canais values (0, "Bem Vindo (bemvindoCh)");
insert into Tipos_Canais values (1, "Sair (sairCh)");

create table Canais (
	cod bigint not null auto_increment,
    cod_Tipos_Canais bigint not null,
    nome varchar(255) not null,
    id bigint not null,
    codigo_servidor int not null,
    foreign key (cod_Tipos_Canais) references Tipos_Canais (cod),
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);
delimiter ;
 
select count(Canais.cod_Tipos_Canais) from Canais where Canais.codigo_servidor = 5 and Canais.cod_Tipos_Canais = 0;


delimiter $$

create function verificarCh(
	 _tipo_canal int,
     _cod_servidor int
) returns int begin
	declare _return int;
    set _return = (select count(Canais.cod) from Canais where Canais.codigo_servidor = _cod_servidor and Canais.cod_Tipos_Canais = _tipo_canal);
    return _return;
end$$

create procedure AdcCh (
	in _tipo_canal int,
	in _nome varchar (255),
    in _id_canal bigint,
    in _cod_servidor bigint
) begin
	if((select verificarCh(_tipo_canal, _cod_servidor) = 0)) then 
		insert into Canais (cod_tipos_Canais, nome, id, codigo_servidor) values (_tipo_canal, _nome, _id_canal, _cod_servidor);
    end if;
end $$

create procedure setCh(
	in _tipo_canal int,
    in _nome varchar (255),
    in _id_canal bigint,
    in _id_servidor bigint
) begin 
	declare cod_servidor int;
    set cod_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
	call AdcCh(_tipo_canal, _nome, _id_canal, cod_servidor);
    update Canais set id = _id_canal, nome = _nome where Canais.codigo_servidor = cod_servidor and Canais.cod_Tipos_Canais = _tipo_canal;
    
    if((select Canais.id from Canais where Canais.codigo_servidor = cod_servidor) = _id_canal) then
		select true as result;
    else
		select false as result;
    end if;
end$$

create procedure GetCh (
	in _tipo_canal bigint,
    in _id_servidor bigint
) begin 
	select Canais.cod, Canais.cod_Tipos_Canais, canal, id, servidores.id_servidor, Servidores.nome_servidor from Canais join servidores on Servidores.codigo_servidor = Canais.codigo_servidor where Canais.cod_Tipos_Canais = _tipo_canal and Canais.codigo_servidor = (Select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

call GetCh(0, 556580866198077451)$$

