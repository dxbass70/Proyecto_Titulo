using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

[Serializable]
public class Usuario{
    public int id;
    public string loginame;
    public string password;
    public string nombre;
}
public class Login : MonoBehaviour
{
    public InputField usuarioInput;
    public InputField contrasenaInput;

    public void login(){
        Usuario usuario;
        usuario = new Usuario();
        usuario.loginame = usuarioInput.GetComponent<InputField>().text;
        usuario.password = contrasenaInput.GetComponent<InputField>().text;
        StartCoroutine(Post(usuario));
        
    }

    IEnumerator Post(Usuario usuario){
        string urlAPI = "http://localhost:3002/api/login";
        var jsonData = JsonUtility.ToJson(usuario);

        using (UnityWebRequest www = UnityWebRequest.Post(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        var usuarios = JsonUtility.FromJson<Usuario>(result);
                        SystemSave.usuario = usuarios;
                        SystemSave.usuario.loginame = usuario.loginame;
                        asigna_reim_alumno();
                        SceneManager.LoadScene("Ciudad");
                        Debug.Log("Ingresando a la ciudad");
                    }
                }
            }

        }

    }

    private void asigna_reim_alumno(){
        string sesion_id = SystemSave.usuario.id + "-" + SystemSave.reim.id + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //ajustar a formato
        string inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Asigna_reim_alumno datos = new Asigna_reim_alumno(sesion_id, SystemSave.usuario.id, 202101, SystemSave.reim.id, inicio, inicio);
        SystemSave.asigna_reim_alumno = datos;
        Debug.Log("Sesion "+SystemSave.asigna_reim_alumno.sesion_id+" registrada");
        SystemSave.Put_asigna_reim_alumno(SystemSave.asigna_reim_alumno, this);
    }
}
