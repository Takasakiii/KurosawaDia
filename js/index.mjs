import paginasControler from './extensions/paginasControler.mjs';

let paginasObj = new paginasControler();


$('document').ready(function(){
    paginasObj.carregarPagina("paginas/main.html");

    $('#navEl1').click(function(){
        navClick("main");
    });

    $('#navEl2').click(function(){
        navClick("comandos");
    });

    $('#navEl3').click(function(){
        navClick("informacao");
    })

});




function navClick(ref){
    paginasObj.carregarPagina(`paginas/${ref}.html`);
}