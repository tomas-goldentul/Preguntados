function revisarConfiguracion()
{
event.preventDefault();
const categoria = document.getElementById("categoria").value
const dificultad = document.getElementById("dificultad").value
const user = document.getElementById("User").value
const valor = document.getElementById("enviado").value
const botonJuego = document.getElementById("botonJuego")
if(valor == 1){
    if(dificultad !== 0 && categoria !== 0 && user != null && user != ""){
    botonJuego.style.display = "block";
}}
}