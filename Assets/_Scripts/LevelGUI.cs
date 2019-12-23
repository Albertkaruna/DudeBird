using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelGUI : MonoBehaviour
{

	public GameObject PopUpMenu;
	private UIManagement uim;

	// Use this for initialization
	void Start()
	{
//		uim = GameObject.Find("DontDestroy").GetComponent<UIManagement>();
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnMouseDown()
	{
		if (gameObject.name == "PlayPause")
		{
			iTween.MoveTo(PopUpMenu, iTween.Hash("y", 1.5f, "time", 1.5f, "easetype", iTween.EaseType.easeOutElastic));
		}
		else if (gameObject.name == "Restart")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (gameObject.name == "Resume")
		{
			iTween.MoveTo(PopUpMenu, iTween.Hash("y", 16f, "time", 1.5f, "easetype", iTween.EaseType.easeInElastic));
		}
		else if (gameObject.name == "Replay")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (gameObject.name == "LevelSelection")
		{
			StartCoroutine(GetScene(1));
		}
	}

	IEnumerator GetScene(int l)
	{
		SceneManager.LoadSceneAsync(l);
		yield return new WaitForSeconds(1f);
//		SceneManager.LoadScene(l);
	}
}
