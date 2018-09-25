using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour {

	public Rocket rocket;

	public Animator animator;

	void Start()
	{
		rocket.resetDeathCount();
	}

	void Update()
	{
		if(rocket.deathCount == 2)
			Invoke("hint",1f);
	}

	public void hint()
	{
		animator.enabled = true;
	}
}
