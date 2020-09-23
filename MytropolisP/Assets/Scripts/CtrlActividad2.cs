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
    private float endTime;
    public Text TextMesh;
    // Start is called before the first frame update
    void Start()
    {
        endTime = Time.time + 30;
        TextMesh.text = "30";
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = (int)(endTime - Time.time);
        if(timeLeft == 0){
            FinPartida(0);
        }
        if (timeLeft < 0) timeLeft = 0;
        TextMesh.text = timeLeft.ToString();
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