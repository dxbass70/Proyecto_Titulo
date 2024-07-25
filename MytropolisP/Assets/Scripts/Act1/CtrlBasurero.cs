using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CtrlBasurero : MonoBehaviour {

    public GameObject ActivityCtrl;
    public GameObject Ventanapuntaje;
    [Range(2f, 20f)]
    public float velocidadMovimiento = 2f;
    private Rigidbody2D rb2D;


    private void Start () {
        rb2D = GetComponent<Rigidbody2D>();  
    }
    private void FixedUpdate () {
        if (Ventanapuntaje.activeSelf == false){
            //rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadMovimiento, Input.GetAxis("Vertical") * velocidadMovimiento);
            rb2D.velocity = new Vector2(Input.acceleration.x * velocidadMovimiento, Input.acceleration.y * velocidadMovimiento);
        }
        else{
            rb2D.velocity = new Vector2(0,0);
        }

        

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Basura"){ //Al chocar con un objeto de tag basura, la basura se destruye
            CtrlSonidoA1.PlaySound ();
            Destroy(other.gameObject);
            ActivityCtrl.SendMessage("IncrementarPuntos");
            //Debug.Log("Basura destruida, +1 punto");
        }
    }
  
}
