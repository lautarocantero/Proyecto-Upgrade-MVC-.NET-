
$('#auriculares').change(function () {
    const auricular = document.querySelector('#auriculares').value;
    const auricularx = document.querySelector('.aur');
    auricularx.innerHTML = `<img src="/Productos/getImage/${auricular}"/>`;

})

$('#monitor').change(function () {
    const monitor = document.querySelector('#monitor').value;
    const monitorx = document.querySelector('.mon');
    monitorx.innerHTML = `<img src="/Productos/getImage/${monitor}"/>`;

})

$('#teclado').change(function () {
    const teclado = document.querySelector('#teclado').value;
    const tecladox = document.querySelector('.tec');
    tecladox.innerHTML = `<img src="/Productos/getImage/${teclado}"/>`;

})

$('#mouse').change(function () {
    const mouse = document.querySelector('#mouse').value;
    const mousex = document.querySelector('.mou');
    mousex.innerHTML = `<img src="/Productos/getImage/${mouse}"/>`;

})


$('#ram').change(function () {
    const ram = document.querySelector('#ram').value;
    const ramx = document.querySelector('.ram');
    ramx.innerHTML = `<img src="/Productos/getImage/${ram}"/>`;

})


$('#discoduro').change(function () {
    const disco = document.querySelector('#discoduro').value;
    const discox = document.querySelector('.dis');
    discox.innerHTML = `<img src="/Productos/getImage/${disco}"/>`;

})

$('.botonCatalogo').onClick(function () {
    console.log("agregado al carrito");
    alert("Los productos Se han agregado al Carrito!");

})



