using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {
	public bool IsSelected;
	Animator animator;

	ButtonAction action;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponent<Animator>();
		action = transform.GetComponent<ButtonAction>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsSelected != animator.GetBool("Selected"))
			animator.SetBool("Selected", IsSelected);
	}

	public void Activate()
	{
		action.Activate();
	}
}
