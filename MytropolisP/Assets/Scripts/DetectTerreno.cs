using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectTerreno : MonoBehaviour
{
    private GameObject Constructor;

    // Start is called before the first frame update
    void Start()
    {
        Constructor = GameObject.Find("CtrlGeneradorEdificios");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(Constructor.GetComponent<CtrlGeneradorEdificios>().Construir == true){ //Al chocar con un edificio no se construye
            //CtrlSonidoA1.PlaySound ();    //Sonido construccion
            Constructor.SendMessage("ConfirmConstruccion",true);
            Debug.Log("Se puede construir");
            //Destroy(gameObject);
        }
    }
}
