using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealizarIntercambio : MonoBehaviour
{
    public InputField agua1;
    public InputField energia1;
    public InputField monedas1;
    private string aguatext1;
    private string energiatext1;
    private string monedastext1;
    public GameObject agua2;
    public GameObject energia2;
    public GameObject monedas2;
    private string aguatext2;
    private string energiatext2;
    private string monedastext2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmarIntercambio(){
        aguatext1 = agua1.GetComponent<InputField>().text;
        energiatext1 = energia1.GetComponent<InputField>().text;
        monedastext1 = monedas1.GetComponent<InputField>().text;

        aguatext2 = agua2.GetComponent<UnityEngine.UI.Text>().text;
        energiatext2 = energia2.GetComponent<UnityEngine.UI.Text>().text;
        monedastext2 = monedas2.GetComponent<UnityEngine.UI.Text>().text;

        //enviar datos a la bbdd
    }
}
