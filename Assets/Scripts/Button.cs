using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {
	Action clickAction;
	public bool IsSelected;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponent<Animator>();
		animator.StartPlayback();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
