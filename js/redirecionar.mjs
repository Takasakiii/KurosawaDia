import UrlExt from './extensions/urlext.mjs';

$("document").ready(function(){
    let redirect = UrlExt.getUrlVars();
    $.getJSON("/assets/Jsons/redirecionar.json", function(data){
        let rd = data.paginas.find(nome => nome.nome == redirect["pg"]);
        if(rd){
            window.location.href = rd.url;
        }
        else{
            let statusDiv = document.getElementById("status");
            statusDiv.innerHTML = "404";
        }
    });
});