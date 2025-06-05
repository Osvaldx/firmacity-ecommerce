/////////////////// GLOBAL VARIABLES /////////////////
let promotionText = document.querySelector(".promotion-text");
//////////////////////////////////////////////////////
let promotionMessages = [
    "Hasta 6 cuotas sin interes con tarjetas BBVA",
    "Encuentra tu sucursal más cercana",
    "Con tu compra de $ 70.000 o más, ENVIO GRATIS!"
];

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

const init = () => {
    promotionTextChange();
}

init();