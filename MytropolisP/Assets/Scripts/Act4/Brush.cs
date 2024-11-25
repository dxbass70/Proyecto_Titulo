﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        gameObject.transform.position = mousePos;
        spriteRenderer.enabled = Input.GetMouseButton(0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Boton" && Input.GetMouseButton(0)){ //Al chocar con un objeto de tag boton, la basura se destruye
            Debug.Log("Cambiaste de escena");
            other.gameObject.SendMessage("TransicionEscena","Actividad4Menu");
        }
    }
}
