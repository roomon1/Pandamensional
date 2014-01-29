using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PandaControl : MonoBehaviour {

	public float moveSpeed = 10f;
	public float jumpSpeed = 12f;

	public float damageKnockback = 3;
	public float damageStunTime = .6f;

	private bool isGrounded = true;
	private int groundLayer;
	private List<int> groundedLayers;

	private Rigidbody2D r;
	private bool facing = false;
	public float movement;

	private PandaSpawner spawner;
	private Animator animator;

	private List<Platform> passedThroughPlatforms;

	void Awake () {
		r = rigidbody2D;
		groundLayer = (1 << LayerMask.NameToLayer ("White")) + (1 << LayerMask.NameToLayer ("Red")) + (1 << LayerMask.NameToLayer ("Blue")) + (1 << LayerMask.NameToLayer ("Yellow"));
		facing = transform.localScale.x == -1 ? true : false;
		spawner = gameObject.GetComponent<PandaSpawner>();
	}

	void Start () {
		animator = gameObject.GetComponent<Animator>();
		StartCoroutine("MovementInput");	
		StartCoroutine("PlayerAttacked");

		passedThroughPlatforms = new List<Platform>();
	}

	void Update() {
		float dist = 1.5f;
		float width = 0.5f;
		bool movingUp = rigidbody2D.velocity.y > 0;
		if (movingUp)
		{
			Vector2[] rays = new Vector2[2];
			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			rays[0] = pos + width * Vector2.right;
			rays[1] = pos - width * Vector2.right;
			
			for (int i = 0; i < rays.Length; ++i)
			{
				Vector3 start = new Vector3(rays[i].x, rays[i].y);
				Debug.DrawLine(start, start + Vector3.up * dist);
				RaycastHit2D hit = Physics2D.Raycast(rays[i], Vector2.up, dist, groundLayer);
				if (hit)
				{	
					Platform hitPlat = hit.collider.gameObject.GetComponent<Platform>();
					if (hitPlat != null && hitPlat.m_PassThrough)
					{
						Debug.Log("disas");
						hitPlat.PassThrough();
						passedThroughPlatforms.Add(hitPlat);
						break;
					}
				}
			}
		}
		else
		{
			if (passedThroughPlatforms.Count > 0)
			{
				Debug.Log("en" + passedThroughPlatforms.Count);
				for (int i = 0; i < passedThroughPlatforms.Count; ++i)
					passedThroughPlatforms[i].PassThroughDone();
				passedThroughPlatforms.Clear();
			}
		}
	}

	IEnumerator MovementInput() {

		while (true) {
			movement = Input.GetAxis("Horizontal");

			if (movement != 0.0)
				animator.SetBool("Running", true);
			else
				animator.SetBool("Running", false);
			
			if (movement > 0 && facing == true) {
				transform.localScale = Vector3.one;
				facing = false;
			}
			else if (movement < 0 && facing == false) {
				transform.localScale = new Vector3(-1,1,1);
				facing = true;
			}

			Vector2 pos = new Vector2(transform.position.x, transform.position.y);
			Vector2[] rays = new Vector2[3];
			rays[0] = pos + Vector2.up;
			rays[1] = pos;
			rays[2] = pos - Vector2.up;
			Vector2 rayDir = facing ? -Vector2.right : Vector2.right;
			bool hit = false;
			for (int i = 0; i < rays.Length && !hit; ++i)
				if (Physics2D.Raycast(rays[i], rayDir, 0.75f, groundLayer))
					hit = true;

			if (hit)
				r.velocity = new Vector2(0, r.velocity.y);
			else
				r.velocity = new Vector2(movement * moveSpeed, r.velocity.y);

			if (Physics2D.Raycast(transform.position + Vector3.right * .5f, -Vector2.up, 1.1f, groundLayer) ||
			    Physics2D.Raycast(transform.position + Vector3.left * .5f, -Vector2.up, 1.1f, groundLayer))
			{
				isGrounded = true;
				animator.SetBool("Jumping", false);
			}
			else 
			{
				isGrounded = false;
				animator.SetBool("Jumping", true);
			}
				

			if (Input.GetButtonDown("Jump") && isGrounded) {
				r.velocity = new Vector2(r.velocity.x, jumpSpeed);
				isGrounded = false;
				animator.SetBool("Jumping", true); 
			}

			yield return null;
		}
	}


	IEnumerator PlayerAttacked() {
		while (true) {
			if (Input.GetMouseButtonUp(1)) {
				HaltMovement();
				renderer.material.color = Color.red;
				r.AddForce(new Vector2(damageKnockback * 200,damageKnockback * 200));
				yield return new WaitForSeconds(damageStunTime);
				renderer.material.color = Color.white;
				StartMovement();
			}

			yield return null;
		}
	}

	void HaltMovement() {
		StopCoroutine("MovementInput");
		Input.ResetInputAxes();
		r.velocity = Vector2.zero;
	}

	void StartMovement() {
		StartCoroutine("MovementInput");
		Input.ResetInputAxes();
	}

	IEnumerator DoRespawn() {
		r.velocity = Vector2.zero;
		//Play death animation, disable behaviours
		yield return new WaitForSeconds (1f);
		spawner.Respawn();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "DeathTrigger") {
			StartCoroutine(DoRespawn());
		}

		if (coll.tag == "Respawn") {
			spawner.SetRespawn(coll.transform.position);
		}
	}
}

