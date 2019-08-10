delimiter ;
CREATE TABLE Fuck (
  cod bigint NOT NULL AUTO_INCREMENT,
  codigo_usuario int NOT NULL,
  urlImage varchar(255) NOT NULL,
  explicitImage bool NOT NULL,
  PRIMARY KEY (cod),
  KEY codigo_usuario (codigo_usuario),
  FOREIGN KEY (codigo_usuario) REFERENCES Usuarios (codigo_usuario)
);

delimiter $$

create procedure AdicionarImgFuck(
	in _idUsuario bigint,
    in _img text,
    in _explicit bool
) begin
	declare _codUsuario int;
    set _codUsuario = (select Usuarios.codigo_usuario from Usuarios where Usuarios.id_usuario = _idUsuario);
    insert into Fuck (Fuck.codigo_usuario, Fuck.urlImage, Fuck.explicitImage) values (_codUsuario, _img, _explicit);
end$$

create procedure GetFuckImg (
	in _explicit bool
) begin 
select Fuck.cod, Fuck.urlImage, Fuck.explicitImage, Usuarios.id_usuario, Usuarios.nome_usuario from Fuck join Usuarios on Usuarios.codigo_usuario = Fuck.codigo_usuario where Fuck.explicitImage = _explicit or Fuck.explicitImage = false order by rand() limit 1;
end$$

