using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportPNG : MonoBehaviour
{
    public GameObject SonidoGuardado;
    public dibujo_reim dibujo;

    public void SaveAsPNG(Texture2D texture)
    {
        string Nombre = System.DateTime.Now.ToString("yyyyMMddHHmmss"); //nombre del dibujo
        byte[] bytes = texture.EncodeToPNG();                           //bytes del dibujo
        //Debug.Log("Sesion "+SystemSave.asigna_reim_alumno.sesion_id+" registrada");
        Guardardibujo(dibujo, bytes);
        SystemSave.Adddibujoreim(dibujo, this);
        SonidoGuardado.GetComponent<AudioSource>().Play();
        }

    private void Guardardibujo(dibujo_reim dibujo, byte[] imagen){
        dibujo.sesion_id = SystemSave.asigna_reim_alumno.sesion_id;
        dibujo.usuario_id = SystemSave.usuario.id;
        dibujo.reim_id = SystemSave.reim.id;
        dibujo.actividad_id = SystemSave.actividad4.id;
        dibujo.imagen = imagen;
    }
}
