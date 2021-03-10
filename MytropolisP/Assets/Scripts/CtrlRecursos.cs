using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlRecursos : MonoBehaviour
{

    public int CountMonedas;
    public int CountElect;
    public int CountAgua;
    public Text TextMonedas;
    public Text TextElect;
    public Text TextAgua;
    // Start is called before the first frame update
    void Start()
    {
        LoadPlayer();   //En caso de tener datos guardados, Cargamos los datos al iniciar la escena 
    }

    // Update is called once per frame
    void Update()
    {
        if(TextMonedas != null && TextElect != null && TextAgua != null)  //En caso de que se vean los textos existan en la escena los actualiza
        TextMonedas.text=CountMonedas.ToString();
        TextElect.text=CountElect.ToString();
        TextAgua.text=CountAgua.ToString();
    }

    void SumarMonedas(int monto){
        CountMonedas+=monto;
    }

    void SumarElect(int monto){
        CountElect+=monto;
    }

    void SumarAgua(int monto){
        CountAgua+=monto;
    }

    public void SavePlayer(){
        SystemSave.SavePlayer(this); 
    }

    public void LoadPlayer(){
        PlayerData data = SystemSave.LoadPlayer();

        if(data != null){
            CountMonedas = data.Monedas;
            CountElect = data.Elect;
            CountAgua = data.Agua;
        }
    }

}
