using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {
	Action clickAction;
	public bool IsSelected;
	Animator animator;

	public IButtonAction action;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsSelected != animator.GetBool("Selected"))
			animator.SetBool("Selected", IsSelected);
	}
}
