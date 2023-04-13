using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuyControl : MonoBehaviour
{
	public Text moneyText;
    public static int moneyAmount;
    int gribsold;
    public GameObject grib;
    int fencesold;
    public GameObject fence;
    int flowsold;
    public GameObject flow;
    // Start is called before the first frame update
    void Start()
    {
    	moneyAmount = PlayerPrefs.GetInt ("Acorn");
       gribsold = PlayerPrefs.GetInt ("Gribsold");
       fencesold = PlayerPrefs.GetInt ("Fencesold");
        flowsold = PlayerPrefs.GetInt ("Flowsold");

       if (gribsold == 1)
            grib.SetActive(true);
       else 
            grib.SetActive(false);

        if (fencesold == 1)
            fence.SetActive(true);
       else 
            fence.SetActive(false);

         if (flowsold == 1)
            flow.SetActive(true);
       else 
            flow.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = moneyAmount.ToString();
         PlayerPrefs.SetInt("Acorn",moneyAmount);
    }

    void OnMouseUpAsButton () {
    	switch (gameObject.name){
    		
            case "Shop":
           // PlayerPrefs.SetInt("Acorn",moneyAmount);
                SceneManager.LoadScene("Shop");
            break;
             
           
    	}
    }
}
