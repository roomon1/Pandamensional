using UnityEngine;
using System.Collections;

public class CollectibleManager : MonoBehaviour {
	
	public int maxCollectibles;
	public int collectedCollectibles;

	private Collectible[] collectibles;

	// Use this for initialization
	void Start () {
		collectibles = gameObject.GetComponentsInChildren<Collectible>();
		maxCollectibles = collectibles.Length;

		foreach (Collectible curCollectible in collectibles)
		{
			curCollectible.SetManager(this);
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		string output = string.Format("Collectibles {0}/{1}", collectedCollectibles, maxCollectibles);
		GUI.TextField(new Rect(10, 10, 100, 20), output);
	}
	
	public void CollectibleObtained()
	{
		if (++collectedCollectibles > maxCollectibles)
			Debug.Log("Obtained more collectibles than possible.");
	}
	
	public void Reset()
	{
		collectedCollectibles = 0;
		foreach (Collectible curCollectible in collectibles)
		{
			curCollectible.gameObject.SetActive(true);
		}
	}
}
