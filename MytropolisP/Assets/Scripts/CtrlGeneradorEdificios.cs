using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EdificioData {
    public int pos;
    public Vector3 position;
    public System.DateTime UltimoCobro;

    public EdificioData (int pos, Vector3 position, System.DateTime UltimoCobro){
        this.pos = pos;
        this.position = position;
        this.UltimoCobro = UltimoCobro;
    }

}

public class CtrlGeneradorEdificios : MonoBehaviour
{
    public Camera cam;
    public GameObject[] EdificioPrefab;
    public GameObject CtrlRecursos;
    private Vector3 point;
    public bool Construir = false;
    private bool libre = true;
    private int pos;
    public int coste;
    private List<EdificioData> ListaEdificios = new List<EdificioData>();
    

    // Start is called before the first frame update
    void Start()
    {
        LoadEdificios();    //Se cargan los edificios al inicio de la ejecucion
    }

    // Update is called once per frame
    void Update()
    {

        if (Construir == true & Input.GetMouseButtonDown(0)){
            if(libre == true){  //Si el terreno esta libre
                Vector2 mousePos = new Vector2();
                mousePos = Input.mousePosition;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 105.0f));
                Invoke("CreateEdificio", 0);
                
                //Luego de Confirmar la construccion se hace el cobro del edificio
                int resta = 0 - coste;
                CtrlRecursos.SendMessage("SumarMonedas",resta); //resta el coste del edificio
                CtrlRecursos.SendMessage("SavePlayer"); //Cuando el edificio esta construido, Guarda los datos
                Construir = false;  //Se bloquea el modo de construccion
            }            
            
        }
        
    }

    void CreateEdificio(){
        GameObject newEdificio = Instantiate(EdificioPrefab[pos], point, Quaternion.identity);
        newEdificio.transform.parent = gameObject.transform;
        EdificioData edificioData = new EdificioData(pos, newEdificio.transform.position, newEdificio.GetComponent<CtrlEdificio>().GetUltimoCobro());
        ListaEdificios.Add(edificioData);
        SystemSave.UpdateEdificios(ListaEdificios);
    }

    public void invokeEdifico(int posicion){
        pos = posicion;
        Construir = true;
        //mensaje con voz
        //Debug.Log("Donde quieres colocar tu edificio");
    }

    public void ConfirmConstruccion(bool confirm){ //Controla si el edificio se pudo construir
        if(!confirm){
            Debug.Log("No se puede construir aqui");
            libre = false;  //El terreno esta ocupado
            Construir = true;   //Se vuelve a construir
        }else{
            //Debug.Log("Se puede construir aqui");
            libre = true;  //El terreno esta libre
            Construir = true;   //Se vuelve a construir
        }

    }

    private void LoadEdificios(){   //Carga los datos de los edificios en la ciudad
        List<EdificioData> EdificiosTemp = SystemSave.LoadEdificios();

        if(EdificiosTemp != null){  //si hay edificios
            ListaEdificios = EdificiosTemp;
            for (int i = 0; i < ListaEdificios.Count; i++)
            {
                GameObject newEdificio = Instantiate(EdificioPrefab[ListaEdificios[i].pos], ListaEdificios[i].position, Quaternion.identity);
                newEdificio.GetComponent<CtrlEdificio>().SetUltimoCobro(ListaEdificios[i].UltimoCobro);
                newEdificio.transform.parent = gameObject.transform;
            }
        }
    }
}
