using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;



[Serializable]
public class Reim{
    public int id;
    public string nombre;

    public Reim(int id, string nombre){
        this.id = id;
        this.nombre = nombre;
    }
}
[Serializable]
public class Actividad{
    public int id;
    public string nombre;
    public int id_reim;

    public Actividad(int id, string nombre, int id_reim){
        this.id = id;
        this.nombre = nombre;
        this.id_reim = id_reim;
    }
}

[Serializable]
public class Tiempoxactividad{
    public int id_tiempoactividad;
    public string inicio;
    public string final;
    public int causa;
    public int usuario_id;
    public int reim_id;
    public int actividad_id;
}

[Serializable]
public class dibujo_reim{
    public int id_imagenes_reim;
    public string sesion_id;
    public int usuario_id;
    public int reim_id;
    public int actividad_id;
    public byte[] imagen;
}

[Serializable]
public class Asigna_reim_alumno{
    public string sesion_id;
    public int usuario_id;
    public int periodo_id;
    public int reim_id;
    public string datetime_inicio;
    public string datetime_termino;

    public Asigna_reim_alumno(string sesion_id, int usuario_id, int periodo_id, int reim_id, string datetime_inicio, string datetime_termino){
        this.sesion_id = sesion_id;
        this.usuario_id = usuario_id;
        this.periodo_id = periodo_id;
        this.reim_id = reim_id;
        this.datetime_inicio = datetime_inicio;
        this.datetime_termino = datetime_termino;
    }
}
public static class SystemSave{
    //Datos del Reim
    
    public static Asigna_reim_alumno asigna_reim_alumno;
    public static Reim reim = new Reim(301, "Mytropolis");  //Guardamos los datos del reim para futuras consultas
    public static Actividad actividad1 = new Actividad(11011, "Atrapa la basura", 301);
    public static Actividad actividad2 = new Actividad(11012, "Laberinto en la ciudad", 301);
    public static Actividad actividad3 = new Actividad(11013, "Conecta la tuberia", 301);
    public static Actividad actividad4 = new Actividad(11014, "Galeria", 301);
    public static Actividad actividadColab = new Actividad(11015, "Tablon de anuncios", 301);
    public static Actividad Ciudad = new Actividad(11016, "Ciudad", 301);
    //Datos de la sesion
    public static Usuario usuario;  //Guardamos los datos del usuario para futuras consultas

    public static void SavePlayer (CtrlRecursos recursos){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(recursos);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer (){
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else{
            Debug.LogError("Archivo de guardado no encontrado en " + path);
            return null;
        }
    }

    public static void SaveEdificios (List<EdificioData> ListaEdificios){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Edificios.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        ListContainer container = new ListContainer(ListaEdificios);
        string json = JsonUtility.ToJson(container);
        //Debug.Log(json);

        formatter.Serialize(stream, json);
        stream.Close();
    }

    public static List<EdificioData> LoadEdificios (){
        string path = Application.persistentDataPath + "/Edificios.sav";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string json = formatter.Deserialize(stream) as string;
            stream.Close();

            ListContainer container = JsonUtility.FromJson<ListContainer>(json);
            List<EdificioData> ListaEdificios = new List<EdificioData>();

            for (int i = 0; i < container.dataList.Count; i++)
            {
                EdificioData edificioData = new EdificioData(container.dataList[i].pos, container.dataList[i].position, container.dataList[i].UltimoCobro);
                ListaEdificios.Add(edificioData);
                //Debug.Log("pos: " + container.dataList[i].pos + ", position: " + container.dataList[i].position + ", Ultimo Cobro: " + container.dataList[i].UltimoCobro);
            }

            return ListaEdificios;
        } else{
            File.Create(path); // si no existe se genera un save para guardar los edificios
            if (File.Exists(path)){
                Debug.LogError("No se pudo generar el archivo " + path);
            }
            
            return null;
        }
    }

    public static void SaveTiempoActividad(Tiempoxactividad data, MonoBehaviour instance){
        instance.StartCoroutine(addtiempoxactividad(data));
    }

    public static IEnumerator addtiempoxactividad(Tiempoxactividad data){
        string urlAPI = "http://localhost:3002/api/tiempoxactividad/add";
        var jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        //var id = JsonUtility.FromJson<String>(result);
                        //Debug.Log(result);
                        data.id_tiempoactividad = Convert.ToInt32(result);;
                        Debug.Log(data.id_tiempoactividad);
                        Debug.Log("agregado tiempoxactividad");
                    }
                }
            }

        }

    }

    public static void UpdateTiempoActividad(Tiempoxactividad data, MonoBehaviour instance){
        instance.StartCoroutine(updatetiempoxactividad(data));
    }

    public static IEnumerator updatetiempoxactividad(Tiempoxactividad data){
        string urlAPI = "http://localhost:3002/api/tiempoxactividad/update/"+data.id_tiempoactividad.ToString();
        var jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        //var id = JsonUtility.FromJson<String>(result);
                        //Debug.Log(result);
                        //data.id = Convert.ToInt32(result);;
                        Debug.Log(result);
                        Debug.Log("Actualizando tiempoxactividad");
                    }
                }
            }

        }

    }

    public static void Put_asigna_reim_alumno(Asigna_reim_alumno data, MonoBehaviour instance){
        instance.StartCoroutine(put_asigna_reim_alumno(data));
    }

    public static IEnumerator put_asigna_reim_alumno(Asigna_reim_alumno data){
        string urlAPI = "http://localhost:3002/api/asigna_reim_alumno/add";
        var jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        //Debug.Log("Sesion "+data.sesion_id+" registrada");                        
                    }
                    
                }
            }

        }

    }

    public static void Adddibujoreim(dibujo_reim data, MonoBehaviour instance){
        instance.StartCoroutine(adddibujoreim(data));
    }

    public static IEnumerator adddibujoreim(dibujo_reim data){
        string urlAPI = "http://localhost:3002/api/dibujo_reim/add";
        var jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        data.id_imagenes_reim = Convert.ToInt32(result);;
                        Debug.Log(data.id_imagenes_reim);
                        Debug.Log("agregado dibujo_reim");
                    }
                }
            }

        }

    }
}

public struct ListContainer
{
	public List<EdificioData> dataList;

    /// <summary>
	/// Constructor
	/// </summary>
	/// <param name="_dataList">Data list value</param>
	public ListContainer(List<EdificioData> _dataList)
	{
		dataList = _dataList;
	}
}
