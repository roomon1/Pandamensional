using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	private static int DefaultCullMask = -1044481;

	public Vector3 offset = new Vector3(0,2f,-10f);
	public Vector2 yBounds = new Vector2(-3f,0f);
	public Vector2 xBounds = new Vector2(-5f,15f);

	public bool useBounds = true;

	private Transform player;
	private Transform gameCameras;

	private Camera foregroundCamera;
	private Camera secondCamera;
	private Camera thirdCamera;

	private Camera foregroundCamera_BG;
	private Camera secondCamera_BG;
	private Camera thirdCamera_BG;

	public LayerMask baseCullMask = DefaultCullMask;
//	private int redCullMask = 1 << LayerMask.NameToLayer ("Red");
//	private int blueCullMask = 1 << LayerMask.NameToLayer ("Blue");
//	private int yellowCullMask = 1 << LayerMask.NameToLayer ("Yellow");
//	private int redBGCullMask = 1 << LayerMask.NameToLayer ("Red BG");
//	private int blueBGCullMask = 1 << LayerMask.NameToLayer ("Blue BG");
//	private int yellowBGCullMask = 1 << LayerMask.NameToLayer ("Yellow BG");

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		gameCameras = transform.Find ("Game Cameras");

		foregroundCamera = transform.Find("Game Cameras/Foreground Camera").camera;
		secondCamera = transform.Find("Game Cameras/Second Camera").camera;
		thirdCamera = transform.Find("Game Cameras/Third Camera").camera;
		
		foregroundCamera_BG = transform.Find("Background Cameras/Foreground Camera BG").camera;
		secondCamera_BG = transform.Find("Background Cameras/Second Camera BG").camera;
		thirdCamera_BG = transform.Find("Background Cameras/Third Camera BG").camera;

		if (baseCullMask.value != DefaultCullMask)
			Debug.LogWarning("Non-default base cull mask on cameras: " + baseCullMask.value);
	}

	void Start () {
	
	}

	public void SetNewActiveColor(eColor activeColour, eColor secondColour, eColor thirdColour) {
		if (activeColour == eColor.White) {
			foregroundCamera.cullingMask = baseCullMask.value;
			foregroundCamera_BG.enabled = false;
		}
		else {
			foregroundCamera.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(activeColour)));
			foregroundCamera_BG.enabled = true;
			foregroundCamera_BG.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(activeColour) + " BG"));
		}

		if (secondColour == eColor.White) {
			secondCamera.enabled = false;
			secondCamera_BG.enabled = false;
		}
		else {
			secondCamera.enabled = true;
			secondCamera.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(secondColour)));
			secondCamera_BG.enabled = true;
			secondCamera_BG.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(secondColour) + " BG"));
		}

		if (thirdColour == eColor.White) {
			thirdCamera.enabled = false;
			thirdCamera_BG.enabled = false;
		}
		else {
			thirdCamera.enabled = true;
			thirdCamera.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(thirdColour)));
			thirdCamera_BG.enabled = true;
			thirdCamera_BG.cullingMask = baseCullMask.value | (1 << LayerMask.NameToLayer (PlatformColor.ToString(thirdColour) + " BG"));
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {

		gameCameras.position = useBounds ? new Vector3(
			Mathf.Clamp(player.position.x + offset.x, xBounds.x, xBounds.y),
			Mathf.Clamp(player.position.y + offset.y, yBounds.x, yBounds.y),
			offset.z
		) : player.position + offset;
	}
}
