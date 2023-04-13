using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acorn_plus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         GetComponent <Text> ().text = PlayerPrefs.GetInt ("Acorn_plus",0).ToString();
    }

   
}
