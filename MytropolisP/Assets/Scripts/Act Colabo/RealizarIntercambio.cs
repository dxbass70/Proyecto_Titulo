using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealizarIntercambio : MonoBehaviour
{
    public GameObject VentanaTablon;
    [Header("Datos Usuario")]

    public Anuncio usuarioData = new Anuncio(); //se almacenan los recursos que ofrece el usuario
    public Text nombreUser;
    public InputField agua1;
    public InputField energia1;
    public InputField monedas1;
    private string aguatext1;
    private string energiatext1;
    private string monedastext1;
    [Header("Datos Anunciante")]
    public Anuncio anuncio;
    public Text NombreAnunciante;
    public Text agua2;
    public Text energia2;
    public Text monedas2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmarIntercambio(){
        gameObject.SetActive(false);
        VentanaTablon.SetActive(true);
        print("Intercambio Confirmado");
        //enviar datos a la bbdd
    }

    public void CancelarIntercambio(){
        gameObject.SetActive(false);
        VentanaTablon.SetActive(true);
        print("Intercambio Cancelado");
    }

    public void SetDatos(Anuncio anun){ //Asignamos los datos del anuncio
        anuncio = anun;
        NombreAnunciante.text = anuncio.nombre;
        agua2.text = anuncio.agua.ToString();
        energia2.text = anuncio.elect.ToString();
        monedas2.text = anuncio.monedas.ToString();
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
