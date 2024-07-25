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
    public GameObject Ventana5;
    public GameObject Ventana6;
    public GameObject Ventana7;

    //Funcion porque soy un flojo ql
    public void ShowVentanaC()
    {
        //en caso de que una ventana este abierta no la abre
        if (VC.activeSelf == false && Ventana2.activeSelf == false && Ventana3.activeSelf == false && Ventana4.activeSelf == false && Ventana5.activeSelf == false && Ventana6.activeSelf == false && Ventana7.activeSelf == false){
            VC.SetActive(true); // activa el objeto
        }
        else if (VC.activeSelf == true && Ventana2.activeSelf == false && Ventana3.activeSelf == false && Ventana4.activeSelf == false && Ventana5.activeSelf == false && Ventana6.activeSelf == false && Ventana7.activeSelf == false){
            VC.SetActive(false); // activa el objeto
        }
        else {
            VC.SetActive(false); // activa el objeto
            //desactiva las otras ventanas
            Ventana2.SetActive(false);
            Ventana3.SetActive(false);
            Ventana4.SetActive(false);
            Ventana5.SetActive(false);
            Ventana6.SetActive(false);
            Ventana7.SetActive(false);
            ShowVentanaC();
        }
    }
}