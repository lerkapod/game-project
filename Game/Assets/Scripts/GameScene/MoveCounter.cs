using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveCounter : MonoBehaviour
{


public static MoveCounter Instance {get; private set;}
    private int _count;
   
    public int Count
    {
    	get => _count;
			set{
				if (_count == value) return;

				_count = value;
				countText.SetText($"{_count}");
				

			}
    }

    [SerializeField] private TextMeshProUGUI countText;


    private void Awake() => Instance = this;
 }


