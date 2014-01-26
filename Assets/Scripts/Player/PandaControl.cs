using UnityEngine;
using System.Collections;

public class PandaControl : MonoBehaviour {

	public float moveSpeed = 10f;
	public float jumpSpeed = 12f;

	public float damageKnockback = 3;
	public float damageStunTime = .6f;

	private bool isGrounded = true;
	private int groundLayer;

	private Rigidbody2D r;
	private bool facing = true;

	private PandaSpawner spawner;

	void Awake () {
		r = rigidbody2D;
		groundLayer = (1 << LayerMask.NameToLayer ("White")) + (1 << LayerMask.NameToLayer ("Red")) + (1 << LayerMask.NameToLayer ("Blue")) + (1 << LayerMask.NameToLayer ("Yellow"));
		facing = transform.localScale.x == -1 ? true : false;
		spawner = gameObject.GetComponent<PandaSpawner>();
	}

	void Start () {
		StartCoroutine("MovementInput");	
		StartCoroutine("PlayerAttacked");
	}

	IEnumerator MovementInput() {
		float movement;

		while (true) {
			movement = Input.GetAxis("Horizontal");
			
			if (movement > 0 && facing == false) {
				transform.localScale = new Vector3(-1,1,1);
				facing = true;
			}
			else if (movement < 0 && facing == true) {
				transform.localScale = Vector3.one;
				facing = false;
			}

			if (!Physics2D.Raycast(transform.position, facing ? Vector2.right : -Vector2.right, .75f, groundLayer)) {
				r.velocity = new Vector2(movement * moveSpeed, r.velocity.y);
			}
			else {
				r.velocity = new Vector2(0, r.velocity.y);
			}

			if (Physics2D.Raycast(transform.position, -Vector2.up, 1.1f, groundLayer))
				isGrounded = true;
			else 
				isGrounded = false;

			if (Input.GetButtonDown("Jump") && isGrounded) {
				r.velocity = new Vector2(r.velocity.x, jumpSpeed);
				isGrounded = false;
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

