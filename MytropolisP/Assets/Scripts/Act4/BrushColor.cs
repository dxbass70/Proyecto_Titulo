using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushColor : MonoBehaviour
{
    public SpriteRenderer Brush;
    public Color color;

    public void SetColor()
    {
        Debug.Log(" Boton Presionado");
        Brush.color = color;
    }
}
