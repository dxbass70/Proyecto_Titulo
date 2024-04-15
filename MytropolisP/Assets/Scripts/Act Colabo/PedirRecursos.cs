using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedirRecursos : MonoBehaviour
{
    public GameObject VentanaTablon;
    public Anuncio usuarioData = new Anuncio();
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
        gameObject.SetActive(false);
        VentanaTablon.SetActive(true);
        print("Peticion Confirmada");
        //enviar datos a la bbdd
    }
    public void CancelarPeticion(){
        gameObject.SetActive(false);
        VentanaTablon.SetActive(true);
        print("Peticion Cancelada");
    }

    public void GuardaMonedas(Text textfield){
        usuarioData.monedas =  Convert.ToInt32(textfield.text);
        print(usuarioData.monedas);
    }

    public void GuardAgua(Text textfield){
        usuarioData.agua =  Convert.ToInt32(textfield.text);
        print(usuarioData.agua);
    }

    public void GuardaElect(Text textfield){
        usuarioData.elect =  Convert.ToInt32(textfield.text);
        print(usuarioData.elect);
    }
}
