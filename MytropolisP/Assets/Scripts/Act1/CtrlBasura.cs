using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlBasura : MonoBehaviour
{
    
    public float velocity = 2f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.down * velocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
