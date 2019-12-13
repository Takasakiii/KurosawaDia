export default class paginasControler{
    constructor(){
        this.divLocal = document.getElementById("conteudo");
    }

    carregarPagina(pagina){
        let div = this.divLocal;
        $.get(pagina, function(conteudo){
            div.innerHTML = conteudo;
        }, 'html');
    }
}