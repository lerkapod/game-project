using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
	public Sprite mus_on, mus_off;

    

	void Start () {

		if (gameObject.name == "Setting") {
			if (PlayerPrefs.GetString ("Music") == "off"){
				transform.GetChild (0).gameObject.GetComponent <Image> ().sprite = mus_off;
				Camera.main.GetComponent <AudioListener> ().enabled = false;
			}
		}
       
	}

    

    void OnMouseDown () {
    	transform.localScale = new Vector3 (1.08f, 1.08f,1.08f);
    }
    void OnMouseUp () {
    	transform.localScale = new Vector3 (1f, 1f, 1f);
    }

    void OnMouseUpAsButton () {
        if (PlayerPrefs.GetString ("Music") != "off"){
            GameObject.Find("Audio").GetComponent <AudioSource> ().Play();
            
        }
            

    	switch (gameObject.name){
    		case "Play":
    			SceneManager.LoadScene("Game");
    		break;
    		case "Music":
    			if (PlayerPrefs.GetString ("Music") == "off") { //Play music now
    				GetComponent <Image> ().sprite = mus_on;
    				PlayerPrefs.SetString ("Music", "on");
                     Camera.main.GetComponent <AudioListener> ().enabled = true;
                      AudioListener.volume = 0.5f;

    				
    			} else {// Off music
    				GetComponent <Image> ().sprite = mus_off;
    				PlayerPrefs.SetString ("Music", "off");
    			     Camera.main.GetComponent <AudioListener> ().enabled = false;
                      AudioListener.volume = 0;
                   
                }
    		break;
    		case "Setting":
    			for (int i = 0; i < transform.childCount; i++)
    				transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeSelf);
    		break;
    		case "Exit":
    			Application.Quit();
    			//UnityEditor.EditorApplication.isPlaying = false;
    		break;
            case "Loop":
                SceneManager.LoadScene("Game");
            break;
            case "Shop":
                SceneManager.LoadScene("Shop");
            break;
            case "Back":
                SceneManager.LoadScene("Main");
                //PlayerPrefs.SetInt("Acorn",Acorn);
            break;
           case "Reset":
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Main");
            break; 
    		
    		
    	}
    }
}
