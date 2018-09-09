using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField]
		private float rcsThrust = 250f;
	[SerializeField]
		private float mainThrust = 50f;

	[SerializeField]
		private AudioClip mainEngine;

	[SerializeField]
		private AudioClip success;

	[SerializeField]
		private AudioClip death;


	Rigidbody rb;
	AudioSource source;

	enum State {Alive,Dying,Trans}

	State state = State.Alive;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if(state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	private void RespondToRotateInput()
	{

		rb.freezeRotation = true;


		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if(Input.GetKey(KeyCode.A))
		{

			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rb.freezeRotation = false;
	}

	private void RespondToThrustInput()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			ApplyThrusting();
		}
		else{
			source.Stop();
		}
	}

	private void ApplyThrusting()
	{
		rb.AddRelativeForce(Vector3.up * mainThrust);
		if(!source.isPlaying)
		{
			source.PlayOneShot(mainEngine);
		}
	}

	void OnCollisionEnter(Collision col)
	{

		if(state != State.Alive)
			return;

		switch(col.gameObject.tag)
		{
			case "Friendly":
				
				break;
			case "Terrain":
				Debug.Log("Terrain collide");
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				if(state != State.Trans)
				{
					StartDyingSequence();
				}
				break;
		}
	}

	private void StartSuccessSequence()
	{
		state = State.Trans;
		source.Stop();
		source.PlayOneShot(success);
		Invoke("loadNextLevel",1f);
	}

	private void StartDyingSequence()
	{
		state = State.Dying;
		source.Stop();
		source.PlayOneShot(death);
		Invoke("reloadLevel",1f);
	}

	private void loadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void reloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
