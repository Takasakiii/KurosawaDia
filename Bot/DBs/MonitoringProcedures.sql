delimiter $$
create procedure MostrarAsTetas()
begin
	select Servidores.nome_servidor, Usuarios.nome_usuario from Servidores_Usuarios join Servidores on Servidores.codigo_servidor = Servidores_Usuarios.Servidores_codigo_servidor join Usuarios on Usuarios.codigo_usuario = Servidores_Usuarios.Usuarios_codigo_usuario;
end$$

create procedure MandaOsNude (
    in _codigo_usuario bigint
) begin 
    select Servidores.codigo_servidor, Servidores.nome_servidor, Servidores.id_servidor, " " as separador, Usuarios.codigo_usuario, Usuarios.nome_usuario, Usuarios.id_usuario from Servidores_Usuarios join Servidores on Servidores.codigo_servidor = Servidores_Usuarios.Servidores_codigo_servidor join Usuarios on Usuarios.codigo_usuario = Servidores_Usuarios.Usuarios_codigo_usuario where Usuarios.codigo_usuario = _codigo_usuario;
end$$