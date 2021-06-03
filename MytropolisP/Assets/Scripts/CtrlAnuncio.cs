using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlAnuncio : MonoBehaviour
{
    public GameObject VentanaActual;
    public GameObject NuevaVentana;

    public void SelectPersona(){
        VentanaActual.SetActive(false);
        NuevaVentana.SetActive(true);
    }
}
