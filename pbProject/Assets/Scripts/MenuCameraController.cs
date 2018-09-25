using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCameraController : MonoBehaviour {

	
	public Transform[] views;
	public Transform defaultView;

	public GameObject[] gameCanvas;

	
	[SerializeField] private float transitionSpeed;
	Transform currentView;
	void Start () {
		
		currentView = defaultView;
		gameCanvas[0].SetActive(false);
		gameCanvas[1].SetActive(false);
		gameCanvas[2].SetActive(false);
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.G))
		{
			backToDefaultView();
		}
		if(Input.GetKey(KeyCode.W))
		{
			playViewMenu();
			
		}
		if(Input.GetKey(KeyCode.S))
		{
			optionsView();
			
		}
	}
	void LateUpdate () {
		transform.position = Vector3.Lerp(transform.position,currentView.position,Time.deltaTime * transitionSpeed);

		Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x,currentView.transform.rotation.eulerAngles.x,Time.deltaTime * transitionSpeed),
										   Mathf.LerpAngle(transform.eulerAngles.y,currentView.transform.rotation.eulerAngles.y,Time.deltaTime * transitionSpeed),
										   Mathf.LerpAngle(transform.eulerAngles.z,currentView.transform.rotation.eulerAngles.z,Time.deltaTime * transitionSpeed));

		transform.eulerAngles = currentAngle;

	}

	public void playViewMenu()
	{
		currentView = views[0];
		gameCanvas[0].SetActive(true);
	}
	public void optionsView()
	{
		currentView = views[1];
		gameCanvas[1].SetActive(true);
	}

	public void backToDefaultView()
	{
		currentView = defaultView;
		gameCanvas[0].SetActive(false);
		gameCanvas[1].SetActive(false);
		gameCanvas[2].SetActive(false);
	}

	public void levelViewMenu()
	{
		currentView = views[2];
		gameCanvas[2].SetActive(true);
	}
}
