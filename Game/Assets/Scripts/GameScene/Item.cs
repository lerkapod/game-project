using UnityEngine;

[CreateAssetMenu(menuName = "Match-3/Item")]
public sealed class Item : ScriptableObject
{
	public int id;
	public int value;
	public Sprite sprite;
}
