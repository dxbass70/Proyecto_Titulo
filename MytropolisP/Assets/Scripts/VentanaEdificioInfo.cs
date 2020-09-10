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
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DatosEdificio(List<string> datos){
        TituloEdificio.text = prefab.Nombre.ToString();
        InfoEdificio.text = prefab.informacion.ToString();
        AguaEdificio.text = prefab.AguaHora.ToString();
        ElectEdificio.text = prefab.ElectricidadHora.ToString();
        MonedasEdificio.text = prefab.Coste.ToString();
    }
}
