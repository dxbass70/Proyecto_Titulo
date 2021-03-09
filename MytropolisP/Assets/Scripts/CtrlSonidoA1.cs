using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSonidoA1 : MonoBehaviour
{
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent <AudioSource> ();
    }

    // Update is called once per frame
    public static void PlaySound()
    {
        audioSrc.Play();
    }
}
