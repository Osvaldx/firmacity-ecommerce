/////////////////// GLOBAL VARIABLES /////////////////
let promotionText = document.querySelector(".promotion-text");
let categoriesUL = document.getElementById("categories");
let promotionMessages = [
    "Hasta 6 cuotas sin interes con tarjetas BBVA",
    "Encuentra tu sucursal más cercana",
    "Con tu compra de $ 70.000 o más, ENVIO GRATIS!"
];
let categoriesList = ["Especial 2x1","Dermocosmética","Belleza","Cuidado Personal","Bebés","Hogar y Alimentos","Salud y Farmacia","Medicamentos"];
//////////////////////////////////////////////////////

const promotionTextChange = () => {
    let count = 0;
    setInterval(() => {
        if(count >= promotionMessages.length){
            count = 0;
        }

        promotionText.innerText = promotionMessages[count];
        count ++;
    }, 3000);
}

const addCategoriesWeb = () => {
    let categoriesLI = ""
    categoriesList.forEach(name => {
        categoriesLI += `<li><button class="btns-options">${name}</button></li>`
    });

    categoriesUL.innerHTML = categoriesLI;
}

///////////////// INICIALIZADOR ////////////////
const init = () => {
    promotionTextChange();
    addCategoriesWeb();
}

init();