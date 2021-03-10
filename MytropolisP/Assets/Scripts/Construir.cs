using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Construir : MonoBehaviour
{
    public GameObject Ventana;
    public GameObject CtrlGeneradorEdificio;
    public GameObject CtrlRecursos;
    private int coste;
    private int monedas;
    private int resta;

    public void construiredificio(){
        coste = System.Convert.ToInt32(Ventana.GetComponent<VentanaEdificioInfo>().MonedasEdificio.text);
        monedas = CtrlRecursos.GetComponent<CtrlRecursos>().CountMonedas;
        if (coste <= monedas){
            Debug.Log(coste.ToString());
            CtrlGeneradorEdificio.GetComponent<CtrlGeneradorEdificios>().coste = coste;
            
            //Actualizar monto en Base de datos
            CtrlGeneradorEdificio.SendMessage("invokeEdifico",Ventana.GetComponent<VentanaEdificioInfo>().pos);
            Ventana.SetActive(false);
        }
        else{
            //mensaje con voz
            Debug.Log("No tienes las monedas necesarias");
        }
        
    }
}
