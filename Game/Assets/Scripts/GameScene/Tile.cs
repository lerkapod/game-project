using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public sealed class Tile : MonoBehaviour
	{
		public int x;
		public int y;

		public Image icon;

		public Button button;

		private Item _item;
		public Item Item
		{
			get => _item;
			set{
				if (_item == value) return;
				_item = value;
				icon.sprite = _item.sprite;
			}
		}

		public TileData Data => new TileData(x, y, _item.id);
	}