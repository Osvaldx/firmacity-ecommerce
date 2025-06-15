/////////////////// GLOBAL VARIABLES /////////////////
let promotionText = document.querySelector(".promotion-text");
let categoriesUL = document.getElementById("categories-list");
let sectionLi = document.querySelector(".style-list-section")
let promotionMessages = [
    "Hasta 6 cuotas sin interes con tarjetas BBVA",
    "Encuentra tu sucursal más cercana",
    "Con tu compra de $ 70.000 o más, ENVIO GRATIS!"
];
let categoriesList = ["Especial 2x1","Dermocosmética","Belleza","Cuidado Personal","Bebés","Hogar y Alimentos","Salud y Farmacia","Medicamentos"];
let sectionList = [
    {categorie: "Dermocosmética", img: "imgs/categories/Dermocosmetica.svg" },
    {categorie: "Belleza", img: "imgs/categories/Belleza.svg"},
    {categorie:"Cuidado Personal" , img: "imgs/categories/CuidadoPersonal.svg"},
    {categorie: "Bebés", img: "imgs/categories/Bebes.svg"},
    {categorie: "Combos", img: "imgs/categories/Combos.svg"},
    {categorie: "Nutricion y deportes", img: "imgs/categories/Nutricionydeportes.svg"},
    {categorie: "Nuestras Marcas", img: "imgs/categories/Nuestrasmarcas.svg"},
    {categorie: "Electro Salud", img: "imgs/categories/Electrosalud.webp"},
    {categorie: "Medicamentos con receta", img: "imgs/categories/Medicamentosreceta.webp"},
    {categorie: "Medicamentos venta libre", img:"imgs/categories/Medicamentosventalibre.webp"}]
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


///////////////////////ADD LIST OF SECTIONS///////////////////////////////

const addSections = (sectionList) => {

    let showList = ""

    for (let i = 0; i < sectionList.length; i++) {
        showList += `
                    <li class="btns-categories"><img class ="categorie-img"src=${sectionList[i].img} alt="">${sectionList[i].categorie}</li>
                
        `      
    }
    
    sectionLi.innerHTML = showList;
}

///////////////// INICIALIZADOR ////////////////
const init = () => {
    promotionTextChange();
    addCategoriesWeb();
    addSections(sectionList);
}

init();