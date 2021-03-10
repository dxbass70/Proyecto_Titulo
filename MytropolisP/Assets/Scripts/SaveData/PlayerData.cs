using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int Monedas;
    public int Elect;
    public int Agua;

    public PlayerData (CtrlRecursos recursos){
        Monedas = recursos.CountMonedas;
        Elect = recursos.CountElect;
        Agua = recursos.CountAgua;

    }

}
