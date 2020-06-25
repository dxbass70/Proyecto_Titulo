
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public void TransicionCiudad()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Transicion a ciudad");
    }

}
