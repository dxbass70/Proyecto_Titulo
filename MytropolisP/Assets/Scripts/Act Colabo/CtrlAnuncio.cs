using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlAnuncio : MonoBehaviour
{

    public Anuncio anuncio;

    [Header("Ventanas relacionadas")]
    public GameObject VentanaActual;
    public GameObject NuevaVentana;

    [Header("TextFields")]
    public Text nombre;
    public Text monedas;
    public Text agua;
    public Text electricidad;

    public void SelectPersona(){ //Se elige el anuncio
        VentanaActual.SetActive(false);
        NuevaVentana.SetActive(true);
        NuevaVentana.GetComponent<RealizarIntercambio>().SetDatos(anuncio);
    }

    public void AsignarDatos(Anuncio a, GameObject ventanaAct, GameObject ventanaNueva){
        VentanaActual = ventanaAct;
        NuevaVentana = ventanaNueva;
        anuncio = a;
        nombre.text = anuncio.nombre;
        monedas.text = anuncio.monedas.ToString();
        agua.text = anuncio.agua.ToString();
        electricidad.text = anuncio.elect.ToString();
        //print("Anuncio: "+ a.nombre);
    }
}
