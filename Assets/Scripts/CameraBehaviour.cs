using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

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

	private int baseCullMask = -520193;
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
	}

	void Start () {
	
	}

	public void SetNewActiveColor(eColor activeColour, eColor secondColour, eColor thirdColour) {
		foregroundCamera.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(activeColour)));
		secondCamera.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(secondColour)));
		thirdCamera.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(thirdColour)));
		
		foregroundCamera_BG.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(activeColour) + " BG"));
		secondCamera_BG.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(secondColour) + " BG"));
		thirdCamera_BG.cullingMask = baseCullMask + (1 << LayerMask.NameToLayer (PlatformColor.ToString(thirdColour) + " BG"));
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
