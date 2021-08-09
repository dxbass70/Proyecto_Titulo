using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedirRecursos : MonoBehaviour
{
    public InputField agua;
    public InputField energia;
    public InputField monedas;
    private string aguatext;
    private string energiatext;
    private string monedastext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmarPeticion(){
        aguatext = agua.GetComponent<InputField>().text;
        energiatext = energia.GetComponent<InputField>().text;
        monedastext = monedas.GetComponent<InputField>().text;

        //enviar datos a la bbdd
    }
}
