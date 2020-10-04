
var user = document.getElementsByName("usuario")[0]
user.addEventListener("blur", function (evt) {
    if (evt.target.value == "") {
        evt.target.setAttribute("style", "border-color:red")
    } else {
        evt.target.setAttribute("style", "border-color:green")
    }
})





/*


var arrayLista = new Array;
var lista = new Array;
lista = document.getElementsByTagName("input");
var er = /^[a-zA-Z\s0-9@]{2,20}$/;
var erpass = /^[a-zA-Z0-9]{3,20}$/;
var b = 0;
function CajaTexto() {
    CajaTexto.prototype.identificador = "id" || "";
    CajaTexto.prototype.valor = "val" || "";
    CajaTexto.prototype.valida = false;
}
for (var a = 0; a < lista.length; a++) {
    if (lista[a].getAttribute("type") == "text") {
        console.log("poscion " + a + " ....typo " + lista[a].getAttribute("type"))
        var caja = new CajaTexto;
        arrayLista[b] = caja;
        caja.identificador = lista[a].getAttribute("id");
        caja.valor = lista[a].value;
        if (er.test(caja.valor) == true) {
            caja.valida = true;
        }
        b++;
    }
}
//para ver las password
for (var a = 0; a < lista.length; a++) {
    if (lista[a].getAttribute("type") == "password") {
        console.log("poscion " + a + " ....typo " + lista[a].getAttribute("type"))
        var caja = new CajaTexto;
        arrayLista[b] = caja;
        caja.identificador = lista[a].getAttribute("id");
        caja.valor = lista[a].value;
        if (erpass.test(caja.valor) == true) {
            caja.valida = true;
        }
        b++;
    }
}
var ok = false
for (var a = 0; a < arrayLista.length; a++) {
    if (arrayLista[a].valida == true) {
        console.log("el valor es: " + arrayLista[a].valida + " con valor: " + arrayLista[a].valor)
        ok = true
    } else {
        console.log("el valor es: " + arrayLista[a].valida + " con valor: " + arrayLista[a].valor)
        ok = false
    }
}
if (ok == true)

/*<a href="javascript:document.f1.submit()" type="button" class="botonregistra" title="Enviar datos para registrarse"></a>

boton.addEventListener("click", function(ev){
    window.alert("hola, vamos a cancelar el submit()");
    ev.preventDefault();
});


*/
