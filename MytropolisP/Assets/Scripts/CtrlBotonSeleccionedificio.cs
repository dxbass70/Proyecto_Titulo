using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CtrlEdificio;

public class CtrlBotonSeleccionedificio : MonoBehaviour
{
    public GameObject VentanaSelf;
    public GameObject VentanaInfo;
    public CtrlEdificio prefab;
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
        datos[0] = prefab.Nombre.ToString();
        datos[1] = prefab.informacion.ToString();
        datos[2] = prefab.AguaHora.ToString();
        datos[3] = prefab.ElectricidadHora.ToString();
        datos[4] = prefab.Coste.ToString();
        VentanaSelf.SetActive(false);
        VentanaInfo.SetActive(true);
        VentanaInfo.SendMessage("DatosEdificio",prefab);
    }
}
