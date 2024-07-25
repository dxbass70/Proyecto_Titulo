using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImportImages : MonoBehaviour
{
    private List<dibujo_reim> listaDibujos;
    public GameObject DibujoDefecto;
    public GameObject zoom;
    public GameObject Image;
    private Vector3 Posicion;
    private Vector2 Tamaño;
    private GameObject dibujof;
    private Sprite NewSprite;
    private Image Imagen;
    public float Distancia;

    // Start is called before the first frame update
    void Start()
    {
        ImportDibujoFromDB();
    }

    public void ImportDibujoFromDB(){
        listaDibujos = SystemSave.ImportDibujo();
        //obtener posicion del slider
        Posicion = transform.position; //el primer dibujo debe estar en la misma posicion que el padre
        //Posicion.x +=Distancia;
        Tamaño = GetComponent<RectTransform>().sizeDelta;

        foreach (dibujo_reim draw in listaDibujos)
        {
            GetComponent<RectTransform>().sizeDelta += new Vector2(Tamaño.x, 0);
            Texture2D SpriteTexture = LoadTexturebyte(draw.imagen); //se guarda el archivo bytes como textura
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), 100.0f);
            dibujof = Instantiate(DibujoDefecto, Posicion, Quaternion.identity, transform);
            Imagen = dibujof.GetComponent<Image>();
            Imagen.sprite = NewSprite;
            Imagen.GetComponent<ZoomImagen>().zoom = zoom;
            Imagen.GetComponent<ZoomImagen>().Image = Image;
            Posicion.x += Distancia; //posicion del siguiente dibujo
        }
    }

    public Texture2D LoadTexturebyte(byte[] draw) {
     // Load a PNG or JPG file from disk to a Texture2D
     // Returns null if load fails
        Texture2D Tex2D;
        
        Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
        if (Tex2D.LoadImage(draw)){           // Load the imagedata into the texture (size is set automatically)
            return Tex2D;                 // If data = readable -> return texture
        }
        return null;
    }

}
