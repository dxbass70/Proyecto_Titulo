using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Construir : MonoBehaviour
{
    public GameObject Ventana;
    public GameObject CtrlGeneradorEdificio;

    public void construiredificio(){
        CtrlGeneradorEdificio.SendMessage("invokeEdifico");
        Ventana.SetActive(false);
    }
}
