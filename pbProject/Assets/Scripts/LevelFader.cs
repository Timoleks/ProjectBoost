using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour {

	public Animator animator;

	private int levelToLoad;

	void Update()
	{
		if(Input.GetKey(KeyCode.G))
		{
			FadeToLevel(1);
		}
	}

	public void FadeToLevel(int levelIndex)
	{
		levelToLoad = levelIndex;
		animator.SetTrigger("FadeOut");

	}

	public void OnFadeComplete()
	{
		SceneManager.LoadScene(levelToLoad);
	}

}
