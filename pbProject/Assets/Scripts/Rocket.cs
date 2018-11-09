using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {


	public LevelFader fader;
    public LevelSelect lvlSelect;

 

	[SerializeField] private float rcsThrust = 250f;
	[SerializeField] private float mainThrust = 50f;
	public int deathCount = 0;

    public bool canPause;

	//audio
	[SerializeField] private AudioClip mainEngine;
	[SerializeField] private AudioClip success;
	[SerializeField] private AudioClip death;
		

	//particles
	[SerializeField] private ParticleSystem rocketParticle;
	[SerializeField] private ParticleSystem successParticle;
	[SerializeField] private ParticleSystem deathParticle;


   

	Rigidbody rb;
	AudioSource source;

	enum State {Alive,Dying,Trans}

     State state = State.Alive;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
		deathCount = PlayerPrefs.GetInt("deathCount",0);
        canPause = true;
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.P))
			resetDeathCount();

		if(state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotateInput(); 
		}

		//Debug.Log(deathCount);
		
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
            if (rocketParticle.isPlaying)
                rocketParticle.Stop();
		}
	}

	private void ApplyThrusting()
	{
		rb.AddRelativeForce(Vector3.up * mainThrust);
		if(!source.isPlaying)
		{
			source.PlayOneShot(mainEngine);
		}
        if(!rocketParticle.isPlaying)
		    rocketParticle.Play();
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
        canPause = false;
		source.Stop();
		source.PlayOneShot(success);
		successParticle.Play();
        lvlSelect.WinLevel();
		Invoke("loadNextLevel",2f);
	}

	private void StartDyingSequence()
	{
		state = State.Dying;
        canPause = false;
		source.Stop();
		source.PlayOneShot(death);
		deathParticle.Play();
		deathCount++;
	    PlayerPrefs.SetInt("deathCount",deathCount);
		//Debug.Log(deathCount);
		Invoke("reloadLevel",1f);
	}

	private void loadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;
		if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0;
		}
		fader.FadeToLevel(nextSceneIndex);

		resetDeathCount();
	}

	private void reloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	
	
	public void resetDeathCount()
	{
		PlayerPrefs.DeleteKey("deathCount");
		
	}

    

}
