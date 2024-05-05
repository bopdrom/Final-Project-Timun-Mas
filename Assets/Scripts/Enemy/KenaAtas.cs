using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenaAtas : MonoBehaviour
{
    public static bool kena;

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            Debug.Log("Kena");
            kena = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            Debug.Log("Tidakkkk");
            kena = false;
        }
    }
}