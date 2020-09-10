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
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMonedas.text=CountMonedas.ToString();
        TextElect.text=CountElect.ToString();
        TextAgua.text=CountAgua.ToString();
    }
}
