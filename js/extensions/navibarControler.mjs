import paginasControler from './paginasControler.mjs';
export default class navibarControler{
    static mudarPagina(paginas){
        let navibarElements = document.getElementsByClassName("nav-item");

        let definirNavbar = function(index){
            for(let i = 0; i < navibarElements.length; i++){
                let temp = "nav-item ";
                if(i == index){
                    temp += "active";
                }
                navibarElements[i].setAttribute("class", temp);
            }
        }

        for (let i = 0; i < navibarElements.length; i++){
            let aelement = navibarElements[i].getElementsByTagName("a")[0];
            aelement.addEventListener("click", function(){
                definirNavbar(i);
                new paginasControler().carregarPagina(`/paginas/${paginas[i]}.html`);
            });
        }
    }


}