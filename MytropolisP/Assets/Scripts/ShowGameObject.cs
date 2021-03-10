using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameObject : MonoBehaviour
{
    public GameObject VC;
    public GameObject Ventana2;
    public GameObject Ventana3;
    public GameObject Ventana4;

    public void ShowVentanaC()
    {
        //en caso de que una ventana este abierta no la abre
        if (VC.activeSelf == false && Ventana2.activeSelf == false && Ventana3.activeSelf == false && Ventana4.activeSelf == false){
            VC.SetActive(true); // activa el objeto
            Debug.Log(" if Activada ventana" + VC.name);
        }
        else if (VC.activeSelf == true && Ventana2.activeSelf == false && Ventana3.activeSelf == false && Ventana4.activeSelf == false){
            VC.SetActive(false); // activa el objeto
            Debug.Log("el el else if Activada ventana" + VC.name);
        }
        else {
            VC.SetActive(false); // activa el objeto
            //desactiva las otras ventanas
            Ventana2.SetActive(false);
            Ventana3.SetActive(false);
            Ventana4.SetActive(false);
            ShowVentanaC();
            Debug.Log(" else Desactivada ventana" + VC.name);
        }
    }
}