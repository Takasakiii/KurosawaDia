delimiter ;
create table ACRS (
	codigo_acr bigint not null auto_increment,
    trigger_acr text not null,
    resposta_acr text not null,
    codigo_servidor int not null,
    foreign key (codigo_servidor) references Servidores (codigo_servidor),
    primary key (codigo_acr)
);

delimiter $$

create procedure criarAcr(
	in _trigger_acr text,
    in _resposta_acr text,
    in _id_servidor bigint
) begin
	insert into ACRS (trigger_acr, resposta_acr, codigo_servidor) values (_trigger_acr, _resposta_acr, ((select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor)));
    select ACRS.codigo_acr from ACRS where ACRS.trigger_acr = _trigger_acr and ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor) order by ACRS.codigo_acr desc limit 1;
end$$

create procedure deletarAcr(
	in _codigo_acr bigint,
    in _id_servidor bigint
) begin 
	if(select count(_codigo_acr) from ACRS where ACRS.codigo_acr = _codigo_acr and ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor)) = 1 then
		delete from ACRS where ACRS.codigo_acr = _codigo_acr;
        select true as Result;
	else
		select false as Result;
	end if;
end$$

create procedure responderAcr(
	in _trigger_acr text,
    in _id_servidor bigint
) begin
	select ACRS.resposta_acr from ACRS where ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where Servidores.id_servidor = _id_servidor) and ACRS.trigger_acr = _trigger_acr order by rand() limit 1;
end$$

create procedure listarAcr(
	in _id_servidor bigint
) begin
	select * from ACRS where ACRS.codigo_servidor = (select Servidores.codigo_servidor from Servidores where id_servidor = _id_servidor);
end$$
