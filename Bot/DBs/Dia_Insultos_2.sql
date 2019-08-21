delimiter ;
create table Insultos(
	cod bigint not null auto_increment,
    codigo_usuario int not null,
    insulto text not null,
    foreign key (codigo_usuario) references Usuarios(codigo_usuario),
    primary key (cod)
);

delimiter $$

create procedure AdicionarInsulto(
	in _idUsuario bigint,
    in _insulto text
) begin
	declare _codUsuario int;
    set _codUsuario = (select codigo_usuario from Usuarios where id_usuario = _idUsuario);
	insert into Insultos (codigo_usuario, insulto) values (_codUsuario, _insulto); 
end$$

create procedure PegarInsulto()
begin
	select Insultos.insulto, Insultos.cod ,Usuarios.id_usuario, Usuarios.nome_usuario from Insultos join Usuarios on Usuarios.codigo_usuario = Insultos.codigo_usuario order by rand() limit 1;
end$$

