using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ShowGameObject : MonoBehaviour
{
    public GameObject VC;

    public void ShowVentanaC()
    {
        if (VC.activeSelf == false){
            VC.SetActive(true); // activa el objeto
            Debug.Log("Activada ventana" + VC.name);
        }
        else {
            VC.SetActive(false); // activa el objeto
            Debug.Log("Desactivada ventana" + VC.name);
        }
    }

}