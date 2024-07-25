using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportPNG : MonoBehaviour
{
    public GameObject SonidoGuardado;
    public dibujo_reim dibujo;
    public Tiempoxactividad tiempoxactividad = new Tiempoxactividad();
    void Start()
    {
        AddTiempoActividad();
    }

    public void SaveAsPNG(Texture2D texture)
    {
        string Nombre = System.DateTime.Now.ToString("yyyyMMddHHmmss"); //nombre del dibujo
        byte[] bytes = texture.EncodeToPNG();                           //bytes del dibujo
        //Debug.Log("Sesion "+SystemSave.asigna_reim_alumno.sesion_id+" registrada");
        Guardardibujo(dibujo, bytes);
        updatetiempoxactividadfinal(2);
        SonidoGuardado.GetComponent<AudioSource>().Play();
        }

    private void Guardardibujo(dibujo_reim dibujo, byte[] imagen){
        dibujo.sesion_id = SystemSave.asigna_reim_alumno.sesion_id;
        dibujo.usuario_id = SystemSave.usuario.id;
        dibujo.reim_id = SystemSave.reim.id;
        dibujo.actividad_id = SystemSave.actividad4.id;
        dibujo.imagen = imagen;
        SystemSave.ExportDibujo(dibujo);
    }

    private void AddTiempoActividad(){
        tiempoxactividad.inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        tiempoxactividad.final = tiempoxactividad.inicio;       // inicializamos con tiempo final = a inicial
        tiempoxactividad.causa = 0;                             //causa por defecto 0
        tiempoxactividad.usuario_id = SystemSave.usuario.id;
        tiempoxactividad.reim_id = SystemSave.reim.id;
        tiempoxactividad.actividad_id = SystemSave.actividad2.id;
        SystemSave.SaveTiempoActividad(tiempoxactividad);
        
    }

    private void UpdateTiempoActividad(){
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        StartCoroutine(SystemSave.UpdateTiempoActividad(tiempoxactividad.id_tiempoactividad ,tiempoxactividad.final));
        
    }

    public void updatetiempoxactividadfinal(int causa){
        if (tiempoxactividad.causa == 2)    //ya se registro como guardado el dibujo (en caso de volver al menu)
        {
            return;   
        }
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        tiempoxactividad.causa = causa; //Actualizmos la causa para indicar como termino
        StartCoroutine(SystemSave.UpdateTiempoActividadFinal(tiempoxactividad.id_tiempoactividad ,tiempoxactividad.final, tiempoxactividad.causa));
        
    }
}
