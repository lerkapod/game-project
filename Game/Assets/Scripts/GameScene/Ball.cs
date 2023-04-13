using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent <Text> ().text = PlayerPrefs.GetInt ("Ball",0).ToString();
    }

    
}
