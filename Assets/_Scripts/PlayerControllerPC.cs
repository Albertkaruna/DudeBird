using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Security.Policy;

public class PlayerControllerPC : MonoBehaviour
{
	public Transform Player, Spawner1, Spawner2, Obs1, Obs2;
	public GameObject[] Others;
	public GameObject SpipeGesture;
	private Vector2 fp, lp;
	private float dragDistance = Screen.height * 0.15f;
	private bool CanProceed;
	private Animator PlayerAnim;
	private UIManagement UIM;

	// Use this for initialization
	void Start()
	{
		CanProceed = true;
		Player.position = Vector2.zero;
		PlayerAnim = Player.GetComponent<Animator>();
		UIM = GameObject.Find("ScoreCanvas").GetComponent<UIManagement>();
		InvokeRepeating("Spawn1", 4, 1.5f);

	}

	public void StopSpawn()
	{
		CancelInvoke("Spawn1");
	}

	public void StartSpawn()
	{
		InvokeRepeating("Spawn1", 2, 2);
	}
	// Update is called once per frame
	void Update()
	{

//		if (Input.GetButtonDown("Fire1"))
//		{
//			iTween.MoveTo(Player.gameObject, iTween.Hash("y", 3.5f, "easetype", iTween.EaseType.linear, "time", 0.15f));
//			iTween.MoveTo(Player.gameObject, iTween.Hash("y", 0f, "easetype", iTween.EaseType.linear, "time", 0.15f, "delay", 0.15f));
//		}
//		else if (Input.GetButtonDown("Fire3"))
//		{
//			iTween.MoveTo(Player.gameObject, iTween.Hash("y", -3.5f, "easetype", iTween.EaseType.linear, "time", 0.15f));
//			iTween.MoveTo(Player.gameObject, iTween.Hash("y", 0f, "easetype", iTween.EaseType.linear, "time", 0.15f, "delay", 0.15f));
//		}

		if (Input.GetMouseButtonDown(0) && CanProceed)
		{
			fp = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0) && CanProceed)
		{
			lp = Input.mousePosition;
			float xDistance = Mathf.Abs(lp.x - fp.x);
			float yDistance = Mathf.Abs(lp.y - fp.y);

			if (xDistance > dragDistance || yDistance > dragDistance)
			{
				if (SpipeGesture.activeSelf)
				{
					SpipeGesture.SetActive(false);
				}

				if (xDistance > yDistance)
				{
					if (lp.x > fp.x)
					{
						if (CanProceed)
						{
							CanProceed = false;
							PlayerAnim.SetInteger("player", 2);
						}
						Invoke("LetItGo", 1.2f);
					}
					else
					{
						if (CanProceed)
						{
							CanProceed = false;
							PlayerAnim.SetInteger("player", 1);
						}
						Invoke("LetItGo", 1.2f);
					}
				}
				else
				{
					if (lp.y > fp.y)
					{
						if (CanProceed)
						{
							CanProceed = false;
							PlayerAnim.SetInteger("player", 3);
						}
						Invoke("LetItGo", 1.2f);
					}
					else
					{
						if (CanProceed)
						{
							CanProceed = false;
							PlayerAnim.SetInteger("player", 4);
						}
						Invoke("LetItGo", 1.2f);
					}
				}
			}
		}   
	}

	void Spawn1()
	{
		int rand = Random.Range(0, 2);
		if (rand == 0)
		{
			StartCoroutine(GetArrow(true));
		}
		else if (rand == 1)
		{
			StartCoroutine(GetArrow(false));
		}
	}

	IEnumerator GetArrow(bool dir)
	{
		yield return new WaitForSeconds(0.1f);
		if (dir)
		{
			Others[0].SetActive(true);
			iTween.MoveTo(Others[0], iTween.Hash("x", 8.3f, "time", 0.2f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
			Instantiate(Obs1.gameObject, Spawner1.position, Quaternion.identity);
			Invoke("PlayEffect", 0.5f);
			yield return new WaitForSeconds(0.5f);
			Others[0].SetActive(false);
		}
		else
		{
			Others[1].SetActive(true);
			iTween.MoveTo(Others[1], iTween.Hash("y", -4.6f, "time", 0.2f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
			Instantiate(Obs2.gameObject, Spawner2.position, Quaternion.identity);
			Invoke("PlayEffect", 0.5f);
			yield return new WaitForSeconds(0.5f);
			Others[1].SetActive(false);
		}
	}

	void LetItGo()
	{
		CanProceed = true;
	}

	void PlayEffect()
	{
		UIM.Clip_Sounds.PlayOneShot(UIM.Gear);
	}
        
}
