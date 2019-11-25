let formulario;
let video;

function happyOnClick() {
    formulario.hidden = true;
    video.muted = false;
    video.play();
}

$('document').ready(function () {
    formulario = document.getElementById("formulario");
    video = document.getElementById("myVideo");
    document.getElementById('myVideo').addEventListener('ended', function () {
        video.muted = true;
        formulario.hidden = false;
        video.play();
    });
});