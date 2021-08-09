using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportAnuncios : MonoBehaviour
{
    private Vector3 Posicion;
    public Vector2 Tamaño;
    private GameObject publicacion;
    private bool importado = false;
    public GameObject PrefabPersona;
    public float Distancia;
    public int DebugPersonas;
    // Start is called before the first frame update
    private void Start() {
        
    }

    public void ImportPublicaciones()
    {
        GetComponent<RectTransform>().sizeDelta = Tamaño;
        //Debug.Log(Tamaño.y);

        //ajustar el tamaño del canvas para que quepan los dibujos
        for(int i = 0; i < DebugPersonas; i++){
            GetComponent<RectTransform>().sizeDelta += new Vector2(0, Tamaño.y);
        }
        for(int i = 0; i < DebugPersonas; i++){
            Posicion = transform.position;
            Posicion.y -= Distancia*i; //posicion del siguiente dibujo
            publicacion = Instantiate(PrefabPersona, Posicion, Quaternion.identity, transform);
            //Posicion.y -= Distancia; //posicion del siguiente dibujo
        }
    }

    public void DeleteChilds(){
        foreach(Transform child in gameObject.transform){
                Destroy(child.gameObject);
                //Debug.Log("eliminado");
            }
    }
}
