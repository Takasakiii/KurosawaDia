create procedure AddCR (
	in _t text,
	in _resposta text,
	in _modo bool,
	in _servidor bigint
) begin
	insert into CustomReactions (modo_cr, resposta_cr, servidor_cr, trigger_cr) values (_modo, _resposta, (select GetCodServidor(_servidor )), _t );
end;
