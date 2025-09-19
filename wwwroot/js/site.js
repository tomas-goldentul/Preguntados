function revisarConfiguracion()
{
event.preventDefault();
const categoria = document.getElementById("categoria").value
const dificultad = document.getElementById("dificultad").value
const valor = document.getElementById("enviado").value
const botonJuego = document.getElementById("botonJuego")
if(valor == 1){
    if(dificultad !== 0 && categoria !== 0){
    botonJuego.style.display = "block";
}}
}