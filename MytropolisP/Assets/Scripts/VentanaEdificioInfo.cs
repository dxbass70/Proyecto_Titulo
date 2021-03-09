using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentanaEdificioInfo : MonoBehaviour
{
    public GameObject SpriteEdificio;
    public Text TituloEdificio;
    public Text InfoEdificio;
    public Text AguaEdificio;
    public Text ElectEdificio;
    public Text MonedasEdificio;
    private SpriteRenderer spriteRenderer;
    private GameObject Edificio;
    public GameObject[] Edificios;
    public int pos;
    // Start is called before the first frame update
    void Start()
    {
        Edificio = Edificios[pos];
        spriteRenderer = SpriteEdificio.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Edificio.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DatosEdificio(List<string> datos){
        TituloEdificio.text = datos[0];
        InfoEdificio.text = datos[1];
        AguaEdificio.text = datos[2];
        ElectEdificio.text = datos[3];
        MonedasEdificio.text = datos[4];
        pos = System.Convert.ToInt32(datos[5]);
        Start();
    }
}
