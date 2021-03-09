using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlEdificio : MonoBehaviour
{
    public int ElectricidadHora;
    public int AguaHora;
    public int Coste;
    public string Nombre;
    public string informacion;
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
            Constructor.SendMessage("ConfirmConstruccion",false);
            Debug.Log("No se puede construir");
            //Destroy(gameObject);
        }
    }
}
