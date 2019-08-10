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
    canal varchar(255) not null,
    id bigint not null,
    codigo_servidor int not null,
    foreign key (cod_Tipos_Canais) references Tipos_Canais (cod),
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (cod)
);


delimiter $$

create procedure AdcCh (
	in _tipo_canal int,
	in _canal varchar (255),
    in _id_canal bigint,
    in _id_servidor bigint
) begin
	if(select count(Canais.cod) from Canais where Canais.id  = _id_canal and Canais.codigo_servidor = (select Servidores.codigo_Servidor from Servidores where Servidores.id_servidor = _id_servidor)) = 0 then
		insert into Canais (cod_Tipos_Canais, canal, id, codigo_servidor) values (_tipo_canal, _canal, _id_canal, (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor));
    end if;
end $$

create procedure GetCh (
	in _tipo_canal bigint,
    in _id_servidor bigint
) begin 
	select Canais.cod, Canais.cod_Tipos_Canais, canal, id, servidores.id_servidor, Servidores.nome_servidor from Canais join servidores on Servidores.codigo_servidor = Canais.codigo_servidor where Canais.cod_Tipos_Canais = _tipo_canal and Canais.codigo_servidor = (Select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor);
end$$

