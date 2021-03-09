using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CtrlBotonSeleccionedificio : MonoBehaviour
{
    public GameObject VentanaSelf;
    public GameObject VentanaInfo;
    public GameObject Edificio;
    //public CtrlEdificio prefab;
    public string NombreEdificio;
    public string InfoEdificio;
    public int AguaEdificio;
    public int ElectEdificio;
    public int CosteEdificio;
    public int NumEdif;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IrInfo(){
        List<string> datos = new List<string>();
        Debug.Log("lista creada insertando datos");
        datos.Add(Edificio.GetComponent<CtrlEdificio>().Nombre);
        datos.Add(Edificio.GetComponent<CtrlEdificio>().informacion);
        datos.Add(Edificio.GetComponent<CtrlEdificio>().AguaHora.ToString());
        datos.Add(Edificio.GetComponent<CtrlEdificio>().ElectricidadHora.ToString());
        datos.Add(Edificio.GetComponent<CtrlEdificio>().Coste.ToString());
        datos.Add(NumEdif.ToString());
        VentanaSelf.SetActive(false);
        VentanaInfo.SetActive(true);
        VentanaInfo.SendMessage("DatosEdificio",datos);
    }
}
