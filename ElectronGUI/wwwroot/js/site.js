let formulario;
let video;
let configuracoes;

function happyOnClick() {
    formulario.hidden = true;
    video.muted = false;
    video.currentTime = 0; 
}

function configuracoesOnClick() {
    
    formulario.hidden = true;
    configuracoes.hidden = false;
}

function voltarOnClick() {
    configuracoes.hidden = true;
    formulario.hidden = false;
}

$('document').ready(function () {
    if (dbexiste == "False") {
        let iniciardb = document.getElementById("iniciarbt");
        iniciardb.hidden = true;
    }
    configuracoes = document.getElementById("configuracao");
    formulario = document.getElementById("formulario");
    video = document.getElementById("myVideo");
    document.getElementById('myVideo').addEventListener('ended', function () {
        video.muted = true;
        formulario.hidden = false;
        video.play();
    });
});