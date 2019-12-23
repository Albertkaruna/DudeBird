using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Permissions;
using System.Threading;
using UnityEngine.EventSystems;
using StartApp;

public class UIManagement : MonoBehaviour
{

	public AudioSource BG_Sound, Clip_Sounds;
	public AudioClip BG1, BG2, Gear, Bird, Attack, FinalAttack;
	public Text Score_Text, HScore_Text, LevelStartText;
	public Sprite BGIcon1, BGIcon2, SIcon1, SIcon2, PlayI, PauseI;
	public GameObject PopUpMenu, ExitMenu, ResumeButton, MaiinPauseButton, SplashScreen;
	public GameObject[] Others;
	public GameObject Effect, FinalEffect;
	public Sprite Level2Unlock, Level3Unlock;
	public ActivateOnEnable ActivateScene;
	[HideInInspector]
	public int Score;
	[HideInInspector]
	public static int Lives;
	private bool s, ss;
	private Animator ExitAnim;
	private bool ExitControl, OnlyForExit;
	private Button PauseButton;
	[HideInInspector]
	public int HScore;
	private Transform Player;
	private Collider2D DBird;
	private int LevelNo;

	// Use this for initialization
	void Start()
	{
//		PlayerPrefs.DeleteAll();

		Invoke("HideSplashScreen", 3f);
		Lives = 3;
		s = true;
		ExitControl = false;
		ss = true;
		OnlyForExit = false;
		Time.timeScale = 1;
		ExitAnim = ExitMenu.GetComponent<Animator>();
		DontDestroyOnLoad(gameObject);
		#if UNITY_ANDROID
		StartAppWrapper.init();
		StartAppWrapper.loadAd();
		#endif
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape) && !ExitControl)
		{
			if (LevelNo != 0 && LevelNo != 1)
			{
				ExitControl = true;
				OnlyForExit = true;
				Time.timeScale = 0;
				MaiinPauseButton.GetComponent<Button>().interactable = false; 
				ExitAnim.SetBool("exit", true);
			}
		}
		else if (Input.GetMouseButtonDown(0) && ExitControl && !EventSystem.current.IsPointerOverGameObject())
		{
			if (OnlyForExit)
			{
				OnlyForExit = false;
				Cancel();
			}
		}
		Score_Text.text = "Score : " + Score.ToString();
	}


	public void LivesCheck()
	{

		if (Lives == 2)
		{
			Others[3].SetActive(false);
		}
		else if (Lives == 1)
		{
			Others[4].SetActive(false);
		}
		else if (Lives == 0)
		{
			Others[5].SetActive(false);

			if (Score > HScore)
			{
//				PlayerPrefs.SetInt("HScore", Score);
				HighScoreManager(SceneManager.GetActiveScene().buildIndex);
			}
			Lives = 3;

			MaiinPauseButton.GetComponent<Button>().interactable = false;

			Invoke("GameOver", 1f);
			Destroy(Instantiate(FinalEffect, Player.position, Quaternion.identity), 2f);
			Clip_Sounds.PlayOneShot(FinalAttack);

		}
	}


	void GameOver()
	{
		PauseButton = MaiinPauseButton.GetComponent<Button>();
		MaiinPauseButton.GetComponent<Button>().interactable = false;
		PopUpMenu.GetComponent<Animator>().SetBool("pop", true);
		Time.timeScale = 0;
		ResumeButton.SetActive(false);
		ExitControl = true;
	}

 
	public void Okay()
	{
		if (Score > HScore)
		{
//			PlayerPrefs.SetInt("HScore", Score);
			HighScoreManager(SceneManager.GetActiveScene().buildIndex);
		}
		Application.Quit();
	}

	public void Cancel()
	{
		
		ExitAnim.SetBool("exit", false);
		
		Invoke("GetPauseNExit", 0.5f);
	}

	void GetPauseNExit()
	{
		MaiinPauseButton.GetComponent<Button>().interactable = true;
		ExitControl = false;

	}

	public void Play()
	{
		StartCoroutine(GetScene(1));
	}

	public void LevelSel()
	{
		DBird.enabled = false;
		Invoke("GetPauseButtonBack", 1.5f);
		PopUpMenu.GetComponent<Animator>().SetBool("pop", false);
		Camera.main.GetComponent<CameraFadee>().MakeCameraFadeTo();
		StartCoroutine(GetScene(1));
	}

	public void HighScore()
	{
		// Show Highscore screen
	}

	public void BGSound(Image b)
	{
		BG_Sound.enabled = !BG_Sound.enabled;
		if (s)
		{
			s = false;
			b.sprite = BGIcon2;
		}
		else if (!s)
		{
			s = true;
			b.sprite = BGIcon1;
		}
	}

	public void VFX(Image b)
	{
		Clip_Sounds.enabled = !Clip_Sounds.enabled;
		if (ss)
		{
			ss = false;
			b.sprite = SIcon2;
		}
		else if (!ss)
		{
			ss = true;
			b.sprite = SIcon1;
		}
	}

	public void Twitter()
	{
		Application.OpenURL("https://twitter.com/hyper_zeta");
	}

	public void Facebook()
	{
		Application.OpenURL("https://www.facebook.com/HyperZeta-522463037937722");
	}

	public void Restart()
	{
		ExitControl = true;
		DBird.enabled = false;
		Invoke("GetPauseButtonBack", 1.5f);
		PopUpMenu.GetComponent<Animator>().SetBool("pop", false);
		Camera.main.GetComponent<CameraFadee>().MakeCameraFadeTo();
		StartCoroutine(GetScene(SceneManager.GetActiveScene().buildIndex));
	}

	public void Resume()
	{
//		iTween.MoveTo(PopUpMenu, iTween.Hash("y", 16f, "time", 1.5f, "easetype", iTween.EaseType.easeInElastic));
      
		Invoke("GetPauseButtonBack", 1.5f);
		PopUpMenu.GetComponent<Animator>().SetBool("pop", false);
	}



	public void Pause(Button b)
	{
//		iTween.MoveTo(PopUpMenu, iTween.Hash("y", 1.5f, "time", 1.5f, "easetype", iTween.EaseType.easeOutElastic));

		ExitControl = true;
		ResumeButton.SetActive(true);
		b.interactable = false;
		PauseButton = b;
		PopUpMenu.GetComponent<Animator>().SetBool("pop", true);
		Time.timeScale = 0;
	}

	void GetPauseButtonBack()
	{
		ExitControl = false;
		PauseButton.interactable = true;
	}

	IEnumerator GetScene(int l)
	{
		
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadSceneAsync(l);
//		SceneManager.LoadScene(l);
	}

	void OnLevelWasLoaded(int mmm)
	{
		Score = 0;
		LevelNo = mmm;
		if (mmm == 2 || mmm == 3 || mmm == 4)
		{
			GetHighScore(SceneManager.GetActiveScene().buildIndex);
			DBird = GameObject.Find(("Bird1")).GetComponent<Collider2D>();
			Player = GameObject.FindGameObjectWithTag("Player").transform;
			Lives = 3;
			foreach (GameObject g in Others)
			{
				g.SetActive(true);
			}
		}
		else if (mmm == 1)
		{
			foreach (GameObject g in Others)
			{
				g.SetActive(false);
			}

		}
		if (mmm == 0 || mmm == 1)
		{
			BG_Sound.clip = BG1;
			BG_Sound.Play();
		}
		else if (mmm == 2 || mmm == 3 || mmm == 4)
		{
			BG_Sound.clip = BG2;
			BG_Sound.Play();
		}
	}

	void GetHighScore(int gh)
	{
		if (gh == 2)
		{
			HScore = PlayerPrefs.GetInt("L1Score");
			HScore_Text.text = "Highscore: " + HScore.ToString();
			if (HScore < 100)
			{
				LevelStartText.gameObject.SetActive(true);
				LevelStartText.text = "Score 100 to unlock the next level";
				Invoke("HideText", 2f);
			}
		}
		else if (gh == 3)
		{
			HScore = PlayerPrefs.GetInt("L2Score");
			HScore_Text.text = "Highscore: " + HScore.ToString();

			if (HScore < 200)
			{
				LevelStartText.gameObject.SetActive(true);
				LevelStartText.text = "Score 200 to unlock the next level";
				Invoke("HideText", 2f);
			}
		}
		else if (gh == 4)
		{
			HScore = PlayerPrefs.GetInt("L3Score");
			HScore_Text.text = "Highscore: " + HScore.ToString();
		}
	}

	void HideText()
	{
		LevelStartText.gameObject.SetActive(false);
	}

	void HighScoreManager(int hs)
	{
		if (hs == 2)
		{
			PlayerPrefs.SetInt("L1Score", Score);
		}
		else if (hs == 3)
		{
			PlayerPrefs.SetInt("L2Score", Score);
		}
		else if (hs == 4)
		{
			PlayerPrefs.SetInt("L3Score", Score);
		}
	}

	void HideSplashScreen()
	{
		SplashScreen.SetActive(false);
		ActivateScene.enabled = true;
	}
	//	public void LevelSelectionPage()
	//	{
	//		SceneManager.LoadScene(1);
	//	}
}
