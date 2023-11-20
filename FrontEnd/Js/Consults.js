/*------------------FUNCTION SWIP SIDEBAR-------------------*/

let menuBotton = document.querySelector(".menu-consults");
let mainMenu = document.querySelector(".main-container");
let mainConsults = document.querySelector(".aux-container");

menuBotton.addEventListener('click', () => {
    console.log("Botón de menú clicado");
    mainMenu.classList.toggle('container')
    mainConsults.classList.toggle('aux-container')
})