using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CtrlEdificio : MonoBehaviour
{
    public int ElectricidadHora;
    public int AguaHora;
    public int Coste;
    public string Nombre;
    public string informacion;
    private GameObject Constructor;
    private GameObject Recursos;
    private System.DateTime UltimoCobro;
    private bool Cobrar = false;
    private int HorasPasadas;

    void Start()
    {
        UltimoCobro = System.DateTime.Now;  //Se registra la hora de creacion
        Recursos = GameObject.Find("CtrlRecursos");
        Constructor = GameObject.Find("CtrlGeneradorEdificios");
        Constructor.GetComponent<CtrlGeneradorEdificios>().coste = Coste; 

        InvokeRepeating("CalcularHoras", 0f, 60f);  //Necesita una forma de indicar que hay que pagar  
    }


    void Update()
    {   
           
    }

    void OnMouseDown(){
        if(Constructor.GetComponent<CtrlGeneradorEdificios>().Construir == true){ //Al chocar con un edificio no se construye
            Constructor.SendMessage("ConfirmConstruccion",false);   //Se envia el mensaje, no se puede construir
        }

        if(Cobrar){
            CobrarRecursos();
            
        }
    }

    void CobrarRecursos(){
        int RestaElec = 0 - (ElectricidadHora * HorasPasadas);
        int RestaAgua = 0 - (AguaHora * HorasPasadas);
        if(Recursos.GetComponent<CtrlRecursos>().CountElect < ElectricidadHora || Recursos.GetComponent<CtrlRecursos>().CountAgua < AguaHora){
            Debug.Log ("No tienes dinero suficiente, Hoy no se come :'c");
        }
        Recursos.SendMessage("SumarElect",RestaElec);
        Recursos.SendMessage("SumarAgua",RestaAgua);
        Cobrar = false;
    }

    void CalcularHoras(){
        System.DateTime horaActual = System.DateTime.Now;
        double Horas = (horaActual - UltimoCobro).TotalHours;   //Total de horas desde el ultimo cobro
        if(Horas>=1){
            HorasPasadas = System.Convert.ToInt32(Horas);
            Cobrar = true;
            Debug.Log ("A cobrar!!! Familia hoy se come");
        }
        Debug.Log ("Han pasado " + Horas.ToString() + " horas Desde el ultimo cobro");
    }
}
