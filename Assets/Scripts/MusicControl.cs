using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

	public AudioClip menumusic;

	public AudioClip whitemusic;
	public AudioClip redmusic;
	public AudioClip bluemusic;
	public AudioClip yellowmusic;
	
	public AudioClip redmusictran;
	public AudioClip bluemusictran;
	public AudioClip yellowmusictran;

	public AudioClip completionditty;
	public AudioClip deathditty;

	public static MusicControl Instance;

	private AudioSource source;
	private string currentColor;
	private int currentTime;
	private AudioClip nextclip;

	private AudioSource SFX;

	void Awake() {
		Instance = this;
		Instance.source = GetComponent<AudioSource> ();
		Instance.SFX = transform.Find ("SFX").GetComponent<AudioSource> ();
	}

	void Start() {
	//	StartCoroutine(Instance.ChangeTest());
	}

	public static void ChangeMusic(eColor c) {
		Instance.currentColor = PlatformColor.ToString (c);

		switch (Instance.currentColor) {
		case "Red":
			Instance.nextclip = Instance.redmusic;
			//Instance.SFX.PlayOneShot(Instance.redmusictran,.8f);
			break;
		case "Blue":
			Instance.nextclip = Instance.bluemusic;
			//Instance.SFX.PlayOneShot(Instance.bluemusictran,.8f);
			break;
		case "Yellow":
			Instance.nextclip = Instance.yellowmusic;
			//Instance.SFX.PlayOneShot(Instance.yellowmusictran,.8f);
			break;
		case "White":
			Instance.nextclip = Instance.whitemusic;
			break;
		}
				
		Instance.currentTime = Instance.source.timeSamples;

		Instance.source.clip = Instance.nextclip;
		Instance.source.PlayDelayed(0);
		Instance.source.timeSamples = Instance.currentTime;
	}


	IEnumerator ChangeTest() {
		while (true) {
			yield return new WaitForSeconds(5f);
			ChangeMusic (eColor.Red);
			yield return new WaitForSeconds(5f);
			ChangeMusic (eColor.Blue);
			yield return new WaitForSeconds(5f);
			ChangeMusic (eColor.Yellow);
		}
	}
}
