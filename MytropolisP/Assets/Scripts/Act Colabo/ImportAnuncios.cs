using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Anuncio    //objeto con los datos del anuncio
{
    public int id;  //Id del anunciante
    public DateTime date;   //Fecha de publicacion
    public string nombre;   //Nombre del anunciante
    public int monedas; //Cantidad UlearnCoins
    public int agua;    //Cantidad Agua
    public int elect;   //Cantidad Electricidad
}

public class ImportAnuncios : MonoBehaviour
{
    private Vector3 Posicion;
    public Vector2 Tamaño;
    private GameObject publicacion; //objeto que muestra el anuncio en pantalla
    //private bool importado = false;
    public GameObject PrefabPersona; //prefab con la base de la publicación
    public GameObject ventanaTablon;
    public GameObject ventanaIntercambio;
    public float Distancia;
    private int DebugPersonas; //Cantidad de publicaciones
    private List<Anuncio> Anuncios = new List<Anuncio>();
    // Start is called before the first frame update
    private void Start() {
        
    }

    public void ImportPublicaciones() //importa los anuncios disponibles de la base de datos
    {
        DeleteChilds();//limpiamos el gameobject para que no se dupliquen anuncios
        GetComponent<RectTransform>().sizeDelta = Tamaño;
        //Debug.Log(Tamaño.y);

        
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                //Sin oferta (estado = 0)
                //Con oferta (estado = 1)
                //Completado (estado = 2)
                //consultamos solo los anuncios que no tienen alguna oferta (estado = 0) y so sean publicados por el usuario (usuariorecibe_id != SystemSave.usuario.id)
                string sqlQuery = "SELECT usuarioenvia_id, usuariorecibe_id, elemento_id, cantidad, datetime_transac, estado FROM transaccion_reim WHERE usuariorecibe_id != '"+ SystemSave.usuario.id +"' and estado = '0' and (elemento_id='"+ SystemSave.Ulearcoin.id +"'or elemento_id='"+ SystemSave.Agua.id +"'or elemento_id='"+ SystemSave.Electricidad.id +"' )";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        using (MySqlDataReader reader = command.ExecuteReader()){
                            DebugPersonas = 0;
                            Anuncios.Clear();
                            while (reader.Read()){ 
                                //ajustar el tamaño del canvas para que quepan los anuncios
                                GetComponent<RectTransform>().sizeDelta += new Vector2(0, Tamaño.y); 
                                DebugPersonas++;
                                print("Fila " + DebugPersonas + ":"); //para orientarme
                                //leer datos
                                int usuarioenvia_id = Convert.ToInt32(reader["usuarioenvia_id"]);
                                int usuariorecibe_id = Convert.ToInt32(reader["usuariorecibe_id"]);
                                int elemento_id = Convert.ToInt32(reader["elemento_id"]);
                                int cantidad = Convert.ToInt32(reader["cantidad"]);
                                DateTime datetime_transac = reader.GetDateTime(reader.GetOrdinal("datetime_transac"));
                                SearchAnuncio(usuariorecibe_id, elemento_id, cantidad, datetime_transac);
                            }
                        }
                    }
                        
                }
                catch (MySqlException exception){
                    print(exception.Message);
                }
                print("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            print(exception.Message);
        }

        //Generar cada anuncio
        GenerarAnuncios();
        
    }


    public void DeleteChilds(){ //Limpia los anuncios descargados
        foreach(Transform child in gameObject.transform){
                Destroy(child.gameObject);
                //Debug.Log("eliminado");
            }
    }

    private void SearchAnuncio(int id, int elemento_id, int cantidad, DateTime date){ //busca en la lista de anuncios si ya existe un anuncio hecho por el mismo usuario con la misma fecha
        foreach(Anuncio a in Anuncios){
            if(a.date == date && a.id == id){   //si la hay un anuncio con esa fecha
                if(elemento_id == SystemSave.Ulearcoin.id && a.monedas == 0){ //si son UlearnCoin
                    a.monedas = cantidad;    //Asignamos la cantidad al anuncio

                }else if(elemento_id == SystemSave.Agua.id && a.agua == 0){ //si es Agua
                    a.agua = cantidad;   //Asignamos la cantidad al anuncio
                }else if(elemento_id == SystemSave.Electricidad.id && a.elect == 0){ //si es Electricidad
                    a.elect = cantidad;   //Asignamos la cantidad al anuncio
                }else{  //si hay un anincio con fecha e id pero ya esta relleno
                    print("Hmmmm esto no deberia pasar  0.o");
                    print("monedas: " + a.monedas);
                    print("Agua: " + a.agua);
                    print("Electricidad: " + a.elect);
                    print("-----------------");

                }
                return;   //Termina si encuentra un anuncio
            }
        }
        //Si no hay un anuncio lo creamos
        Anuncio newAnun = new Anuncio();
        newAnun.id = id;
        if(elemento_id == SystemSave.Ulearcoin.id){ //si son UlearnCoin
            newAnun.monedas = cantidad; //Asignamos la cantidad al anuncio
        }else if(elemento_id == SystemSave.Agua.id){ //si es Agua
            newAnun.agua = cantidad;;   //Asignamos la cantidad al anuncio
        }else if(elemento_id == SystemSave.Electricidad.id){ //si es Electricidad
            newAnun.elect = cantidad;   //Asignamos la cantidad al anuncio
        }
        newAnun.nombre = SystemSave.GetUserFullName(id);  //Asignamos el nombre del anunciante
        newAnun.date = date;    //Registramos la fecha de publicacion
        //Lo agregamos a la lista
        Anuncios.Add(newAnun);

    }

    private void GenerarAnuncios(){
        for(int i = 0; i < Anuncios.Count; i++){
            Posicion = transform.position;
            Posicion.y -= Distancia*i; //posicion del siguiente dibujo
            //Se genera el anuncio
            publicacion = Instantiate(PrefabPersona, Posicion, Quaternion.identity, transform);
            publicacion.GetComponent<CtrlAnuncio>().AsignarDatos(Anuncios[i], ventanaTablon, ventanaIntercambio);
            //Posicion.y -= Distancia; //posicion del siguiente dibujo
        }
    }

}
