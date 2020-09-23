using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlActividad2 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Electricidad = 0;
    public GameObject Ventanapuntaje;
    public Text TextPuntaje;
    public Text Textelectricidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FinPartida(int Incremento){
        Puntaje += Incremento;
        Debug.Log("Fin de la partida");
        if (Ventanapuntaje.activeSelf == false){
        Ventanapuntaje.SetActive(true); // activa la ventana puntaje
        TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
        Electricidad = Puntaje*10;
        Textelectricidad.text = Electricidad.ToString();
        }
    }
}