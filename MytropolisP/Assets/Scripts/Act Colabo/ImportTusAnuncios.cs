using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportTusAnuncios : MonoBehaviour
{
    private Vector3 Posicion;
    public Vector2 Tamaño;
    private GameObject publicacion; //objeto que muestra el anuncio en pantalla
    //private bool importado = false;
    public GameObject PrefabPersona;
    public GameObject ventanaTablon;
    public GameObject ventanaIntercambio;
    public float Distancia;
    public int DebugPersonas;
    // Start is called before the first frame update
    private void Start() {
        
    }

    public void ImportPublicaciones() //importa los anuncios disponibles de la base de datos
    {
        DeleteChilds();//limpiamos la lista
        GetComponent<RectTransform>().sizeDelta = Tamaño;
        //Debug.Log(Tamaño.y);

        //ajustar el tamaño del canvas para que quepan los dibujos
        for(int i = 0; i < DebugPersonas; i++){
            GetComponent<RectTransform>().sizeDelta += new Vector2(0, Tamaño.y);
        }
        for(int i = 0; i < DebugPersonas; i++){
            Posicion = transform.position;
            Posicion.y -= Distancia*i; //posicion del siguiente dibujo
            //Se almacenan los datos
            Anuncio anuncio = new Anuncio();
            anuncio.nombre = "Nombre Apellidos";
            anuncio.monedas  = 100;
            anuncio.agua = 200;
            anuncio.elect = 300;
            //Se genera el anuncio
            publicacion = Instantiate(PrefabPersona, Posicion, Quaternion.identity, transform);
            publicacion.GetComponent<CtrlAnuncio>().AsignarDatos(anuncio, ventanaTablon, ventanaIntercambio);
            //Posicion.y -= Distancia; //posicion del siguiente dibujo
        }
    }

    public void DeleteChilds(){ //Limpia los anuncios descargados
        foreach(Transform child in gameObject.transform){
                Destroy(child.gameObject);
                //Debug.Log("eliminado");
            }
    }
}
