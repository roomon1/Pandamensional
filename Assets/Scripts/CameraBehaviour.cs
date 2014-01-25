using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public Vector3 offset = new Vector3(0,2f,-10f);
	public Vector2 yBounds = new Vector2(-3f,0f);
	public Vector2 xBounds = new Vector2(-5f,15f);

	private Transform player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(
			Mathf.Clamp(player.position.x + offset.x, xBounds.x, xBounds.y),
			Mathf.Clamp(player.position.y + offset.y, yBounds.x, yBounds.y),
			offset.z
			);
	}
}
