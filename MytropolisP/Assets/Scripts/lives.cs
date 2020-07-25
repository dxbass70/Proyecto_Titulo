using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lives : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] Corazones; 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        CambioVida (3);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambioVida(int pos){
        //this.GetComponent<Image>().sprite = Corazones [pos];
        spriteRenderer.sprite = Corazones [pos];
    }
}
