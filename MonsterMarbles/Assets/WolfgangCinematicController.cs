﻿using UnityEngine;
using System.Collections;


public class WolfgangCinematicController : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			animator.SetBool("IsPanicing?", !animator.GetBool("IsPanicing?"));
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			animator.SetBool("IsLaughing?", !animator.GetBool("IsLaughing?"));
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			animator.SetTrigger("ShouldHowl");
		}
	}
}
