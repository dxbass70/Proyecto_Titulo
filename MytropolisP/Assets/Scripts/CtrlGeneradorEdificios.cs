using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGeneradorEdificios : MonoBehaviour
{
    public Camera cam;
    public GameObject[] EdificioPrefab;
    private Vector3 point;
    public bool Construir = false;
    private bool libre = true;
    private int pos;

    // Start is called before the first frame update
    void Start()
    {
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
                //prefab.GetComponent<CtrlEdificio>().Constructor = gameObject;
                Construir = false;
            }            
            
        }
        
    }

    void CreateEdificio(){
        Instantiate(EdificioPrefab[pos], point, Quaternion.identity);
    }

    public void invokeEdifico(int posicion){
        pos = posicion;
        Construir = true;
        //mensaje con voz
        Debug.Log("Donde quieres colocar tu edificio");
    }

    public void ConfirmConstruccion(bool confirm){ //Controla si el edificio se pudo construir
        if(!confirm){
            Debug.Log("No se puede construir aqui");
            libre = false;  //El terreno esta ocupado
            Construir = true;   //Se vuelve a construir
        }else{
            Debug.Log("Se puede construir aqui");
            libre = true;  //El terreno esta libre
            Construir = true;   //Se vuelve a construir
        }

    }
}
