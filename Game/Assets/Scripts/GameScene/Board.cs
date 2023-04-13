using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public sealed class Board : MonoBehaviour
{
	public static Board Instance {get; private set;}
	public static Board Instance1 {get; private set;}

	[SerializeField] private AudioClip collectSound;

	[SerializeField] private AudioSource audioSource;

	[SerializeField] private bool ensureNoStartingMatches;

	public GameObject pEnd;
	public GameObject board_p;

	public Row[] rows;
	public Tile[,] Tiles {get; private set;}

	public int Width => Tiles.GetLength(0);
	public int Height => Tiles.GetLength(1);

	private readonly List<Tile> _selection = new List<Tile>();

	private const float TweenDuration = 0.25f;

	

	private void Awake() => Instance = this;


	private bool _isSwapping;
		private bool _isMatching;
		private bool _isShuffling;

	private TileData[,] Matrix
		{
			get
			{
				var width = rows.Max(row => row.tiles.Length);
				var height = rows.Length;

				var data = new TileData[width, height];

				for (var y = 0; y < height; y++)
					for (var x = 0; x < width; x++)
						data[x, y] = GetTile(x, y).Data;

				return data;
			}
		}

	private void Start()
		{
			for (var y = 0; y < rows.Length; y++)
			{
				for (var x = 0; x < rows.Max(row => row.tiles.Length); x++)
				{
					var tile = GetTile(x, y);

					tile.x = x;
					tile.y = y;

					
					tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

					tile.button.onClick.AddListener(() => Select(tile));
				}
			}

			if (ensureNoStartingMatches) StartCoroutine(EnsureNoStartingMatches());
}
private IEnumerator EnsureNoStartingMatches()
		{
			var wait = new WaitForEndOfFrame();

			while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
			{
				Shuffle();

				yield return wait;
			}
		}
	/*private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.A)) return;

		foreach (var connectedTile in Tiles[0,0].GetConnectedTiles()) connectedTile.icon.transform.DOScale(1.25f, TweenDuration).Play();
	}
	*/
	private Tile GetTile(int x, int y) => rows[y].tiles[x];

		private Tile[] GetTiles(IList<TileData> tileData)
		{
			var length = tileData.Count;

			var tiles = new Tile[length];

			for (var i = 0; i < length; i++) tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

			return tiles;
		}

	public async void Select(Tile tile)
	{
		if (_isSwapping || _isMatching || _isShuffling) return;

			if (!_selection.Contains(tile))
			{
				if (_selection.Count > 0)
				{
					if (Math.Abs(tile.x - _selection[0].x) == 1 && Math.Abs(tile.y - _selection[0].y) == 0
					    || Math.Abs(tile.y - _selection[0].y) == 1 && Math.Abs(tile.x - _selection[0].x) == 0)
						_selection.Add(tile);
				}
				else
				{
					_selection.Add(tile);
				}
			}

			if (_selection.Count < 2) return;

			await Swap(_selection[0], _selection[1]);

			if (!await TryMatchAsync()) await Swap(_selection[0], _selection[1]);

			var matrix = Matrix;

			while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
			{
				Shuffle();

				matrix = Matrix;
			}

			_selection.Clear();
	}

	public async Task Swap(Tile tile1, Tile tile2)

	{
		_isSwapping = true;
		var icon1  = tile1.icon;
		var icon2  = tile2.icon;

		var icon1Transform  = icon1.transform;
		var icon2Transform  = icon2.transform;
		icon1Transform.SetAsLastSibling();
		icon2Transform.SetAsLastSibling();

		var sequence = DOTween.Sequence();

		sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration)).SetEase(Ease.OutBack)
				.Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration)).SetEase(Ease.OutBack);

		await sequence.Play()
					  .AsyncWaitForCompletion();

		icon1Transform.SetParent(tile2.transform);
		icon2Transform.SetParent(tile1.transform);

		tile1.icon = icon2;
		tile2.icon = icon1;

		var tile1Item = tile1.Item;

		tile1.Item = tile2.Item;
		tile2.Item =  tile1Item;
		_isSwapping = false;
	}


	/*private bool CanPop()
	{
		for (var y = 0; y < Height; y++)
			for (var x = 0; x < Width; x++)
				if (Tiles[x,y].GetConnectedTiles().Skip(1).Count() >= 2) 
					return true;
		return false;

		
	}

	private async void Pop()
	{
		for (var y = 0; y < Height; y++)
		{
			for (var x = 0; x < Width; x++)
			{
				var tile =Tiles[x,y];

				var connectedTiles = tile.GetConnectedTiles();

				if (connectedTiles.Skip(1).Count() < 2) continue;

				var deflateSequence = DOTween.Sequence();

				foreach (var connectedTile in connectedTiles) deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));


				audioSource.PlayOneShot(collectSound);

				ScoreCounter.Instance.Score += tile.Item.value * connectedTiles.Count;

				MoveCounter.Instance.Count -= 1;

				await deflateSequence.Play()
									 .AsyncWaitForCompletion();

				

				var inflateSequence = DOTween.Sequence();

				foreach (var connectedTile in connectedTiles)
				{
					connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

					inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
				}
				
				await inflateSequence.Play()
									 .AsyncWaitForCompletion();

				x=0;
				y = 0;
			}
		}
	}


*/
	 public static int Сoins;



    int count =20;  int count1 =0;  
	private async Task<bool> TryMatchAsync()
		{
			var didMatch = false;

			_isMatching = true;

			var match = TileDataMatrixUtility.FindBestMatch(Matrix);

			var step = new List<int>();
			var ball = new List<int>();

		
			while (match != null)
			{ 
				didMatch = true;
				
				var tiles = GetTiles(match.Tiles);
				if (match.Score > 0)
			{	int n = 0;
				step.Add(n);
				count1+= tiles.Count();
			
				
				if (step.Count ==1)
				{
					ScoreCounter.Instance.Score = count - 1; 
					count = count - 1; 
					
				}

				/*if (count == 0) 
				{
					pEnd.SetActive(true); 
					//board_p.SetActive(false); 
					MoveCounter.Instance.Count = count1 * 10;
					ball.Add(count1 * 10);
					//if (PlayerPrefs.GetInt ("Money1") < count1* 10) PlayerPrefs.SetInt ("Money1", count1 * 10);
					
					 PlayerPrefs.SetInt ("Money2", count1 * 10);
					if (PlayerPrefs.GetInt ("Money1") < PlayerPrefs.GetInt ("Money2", count1 * 10)) PlayerPrefs.SetInt ("Money1", count1 * 10);
				}
				*/
				
				
				
				
				
				
			}
				var deflateSequence = DOTween.Sequence();

				foreach (var tile in tiles) deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, TweenDuration).SetEase(Ease.InBack));

				audioSource.PlayOneShot(collectSound);


				await deflateSequence.Play()
				                     .AsyncWaitForCompletion();

				var inflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
				
					tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

					inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, TweenDuration).SetEase(Ease.OutBack));
				}


				await inflateSequence.Play()
				                     .AsyncWaitForCompletion();

				

				match = TileDataMatrixUtility.FindBestMatch(Matrix);
			}
			
			if (count == 0) 
				{
					pEnd.SetActive(true); 
					//board_p.SetActive(false); 
					//MoveCounter.Instance.Count = count1 * 10;
					//ball.Add(count1 * 10);
					//if (PlayerPrefs.GetInt ("Money1") < count1* 10) PlayerPrefs.SetInt ("Money1", count1 * 10);
					
					//побил свой рекорд 
					/*
					if (PlayerPrefs.GetInt ("Money1") <  count1 * 10) 
					{ 	PlayerPrefs.SetInt ("Money1", count1 * 10);
						PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 10);
					}
					*/
					PlayerPrefs.SetInt ("Ball", count1 * 10);
					if (PlayerPrefs.GetInt ("Money1") <  count1 * 10) 
					{ 	PlayerPrefs.SetInt ("Money1", count1 * 10);
						PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 100);
						PlayerPrefs.SetInt ("Acorn_plus", 100);
						
					}
					else
					{
						if ((PlayerPrefs.GetInt ("Ball") >= 600) & (PlayerPrefs.GetInt ("Ball") < 750)) 
						{
							PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 10);
							PlayerPrefs.SetInt ("Acorn_plus", 10);

						}
						else if ((PlayerPrefs.GetInt ("Ball") >= 750) & (PlayerPrefs.GetInt ("Ball") < 900)) 
						{
							PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 20);
							PlayerPrefs.SetInt ("Acorn_plus", 20);

						}
						else if (PlayerPrefs.GetInt ("Ball") >= 900)  
						{
							PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 50);
							PlayerPrefs.SetInt ("Acorn_plus", 40);

						}
						else PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 0);
					}
				}
				
					
				//else if (PlayerPrefs.GetInt ("Ball") > 800)  PlayerPrefs.SetInt ("Acorn", PlayerPrefs.GetInt ("Acorn") + 2);
				
			
			
			_isMatching = false;
			return didMatch;
		}

	private void Shuffle()
		{
			_isShuffling = true;

			foreach (var row in rows)
				foreach (var tile in row.tiles)
					
					tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

			_isShuffling = false;
		}

		

}

	
