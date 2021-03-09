using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportPNG : MonoBehaviour
{
    public GameObject SonidoGuardado;

    public void SaveAsPNG(Texture2D texture)
    {
        string Nombre = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        //then Save To Disk as PNG
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/Dibujos/";
        if(!Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
            }
        File.WriteAllBytes(dirPath + Nombre + ".png", bytes);
        Debug.Log(dirPath + Nombre + ".png");
        SonidoGuardado.GetComponent<AudioSource>().Play();
        }
}
