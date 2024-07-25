using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirVentana : MonoBehaviour
{

    public GameObject VentanaActual;
    public GameObject NuevaVentana;

     public void AbrirNuevaVentana()
    {
        VentanaActual.SetActive(false);
        NuevaVentana.SetActive(true);
    }
}
