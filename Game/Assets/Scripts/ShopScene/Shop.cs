using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    int moneyAmount;
    int gribsold;
    int fencesold;
    int flowsold;
    

   public Text moneyText;
    public Text gribPrice;
    public Text fencePrice;
    public Text flowPrice;

  public Button buttonBuy;
  public Button buttonBuy1;
  public Button buttonBuy2;

  public Image im;
  public Image im1;
  public Image im2;
    // Start is called before the first frame update
    void Start()
    {
         moneyAmount = PlayerPrefs.GetInt ("Acorn");
       
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = moneyAmount.ToString();
        gribsold = PlayerPrefs.GetInt ("Gribsold");
        fencesold = PlayerPrefs.GetInt ("Fencesold");
        flowsold = PlayerPrefs.GetInt ("Flowsold");

        if (moneyAmount >= 350 && gribsold == 0)
            buttonBuy.interactable =true;
        else
        	buttonBuy.interactable =false;

        if (moneyAmount >= 900 && fencesold == 0)
            buttonBuy1.interactable =true;
        else
        	buttonBuy1.interactable =false;

        if (moneyAmount >= 550 && flowsold == 0)
            buttonBuy2.interactable =true;
        else
        	buttonBuy2.interactable =false;

        
    }
 
      public void buyGrib()
 
    {
        moneyAmount -= 350;
        PlayerPrefs.SetInt("Gribsold",1);
        gribPrice.text = "Куплено";
        buttonBuy.gameObject.SetActive(false);
        im.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Acorn",moneyAmount);
    }

    public void buyFence()
    {
        moneyAmount -= 900;
        PlayerPrefs.SetInt("Fencesold",1);
        fencePrice.text = "Куплено";
        buttonBuy1.gameObject.SetActive(false);
        im1.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Acorn",moneyAmount);
    }

    public void buyFlow()
    {
        moneyAmount -= 550;
        PlayerPrefs.SetInt("Flowsold",1);
        flowPrice.text = "Куплено";
        buttonBuy2.gameObject.SetActive(false);
        im2.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Acorn",moneyAmount);
    }

    void OnMouseUpAsButton () {
    	switch (gameObject.name){
    		
            case "Back":
            	//PlayerPrefs.SetInt("Acorn",moneyAmount);
                SceneManager.LoadScene("Main");
                
            break;
    		
    		
    	}
    }
}
