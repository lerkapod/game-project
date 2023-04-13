using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    
    void Start()
    {  // PlayerPrefs.DeleteKey("Money1");
        GetComponent <Text> ().text = PlayerPrefs.GetInt ("Money1",0).ToString();
    }

   
}
