import navibarControler from './extensions/navibarControler.mjs';
import paginasControler from './extensions/paginasControler.mjs';

const paginas = ["main", "comandos", "informacao"];



$('document').ready(function(){
    new paginasControler().carregarPagina("paginas/main.html");
    navibarControler.mudarPagina(paginas);
});



