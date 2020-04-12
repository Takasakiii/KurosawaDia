create procedure AddCR (
	in _t text,
	in _resposta text,
	in _modo bool,
	in _servidor bigint
) begin
	insert into CustomReactions (modo_cr, resposta_cr, servidor_cr, trigger_cr) values (_modo, _resposta, (select GetCodServidor(_servidor )), _t );
end;


create procedure CREvent(
	in _servidor bigint,
	in _msg text
)begin
	select * from CustomReactions where servidor_cr = GetCodServidor(_servidor) and if(modo_cr, _msg like (concat('%', trigger_cr, '%')), (trigger_cr = _msg)) order by rand() limit 1;
end;

create procedure Lcr(
	in _servidor bigint,
	in _pesquisa text,
	in _page int
) begin
	set _page = ((_page - 1) * 10);
	if(_pesquisa <> "") then
		SELECT * from CustomReactions where servidor_cr = GetCodServidor(_servidor ) and trigger_cr = _pesquisa LIMIT _page, 10;
	ELSE 
		SELECT * from CustomReactions where servidor_cr = GetCodServidor(_servidor ) LIMIT _page, 10;
	end if;
end;

