using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImportImages : MonoBehaviour
{
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
        ImportFromFolder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImportFromFolder()
    {
        //obtener posicion del slider
        Posicion = transform.position; //el primer dibuje debe eastear en la misma posicion que el padre
        Posicion.x +=0.15f;
        Tamaño = GetComponent<RectTransform>().sizeDelta;

        var dirPath = Application.dataPath + "/Dibujos/";
        DirectoryInfo dir = new DirectoryInfo(dirPath);
        FileInfo[] info = dir.GetFiles("*.png");
        Debug.Log("Importando archivos");
        //ajustar el tamaño del canvas para que quepan los dibujos
        foreach (FileInfo f in info){
            GetComponent<RectTransform>().sizeDelta += new Vector2(Tamaño.x, 0);
        }
        foreach (FileInfo f in info){
        
            Debug.Log(f.ToString());
            //creamos el sprite en base al png
            Texture2D SpriteTexture = LoadTexture(f.ToString());
            NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), 100.0f);
            dibujof = Instantiate(DibujoDefecto, Posicion, Quaternion.identity, transform);
            Imagen = dibujof.GetComponent<Image>();
            Imagen.sprite = NewSprite;
            Imagen.GetComponent<ZoomImagen>().zoom = zoom;
            Imagen.GetComponent<ZoomImagen>().Image = Image;
            Posicion.x += Distancia; //posicion del siguiente dibujo
        }
        Debug.Log("archivos importados");

    }
    public Texture2D LoadTexture(string FilePath) {
 
     // Load a PNG or JPG file from disk to a Texture2D
     // Returns null if load fails
 
        Texture2D Tex2D;
        byte[] FileData;
 
        if (File.Exists(FilePath)){
        FileData = File.ReadAllBytes(FilePath);
        Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
        if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
            return Tex2D;                 // If data = readable -> return texture
        }  
        return null;                     // Return null if load failed
    }

}
