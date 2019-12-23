using UnityEngine;
using System.Collections;

public class PlayerControllerr : MonoBehaviour
{
	public Transform Player;
	Vector2 fp, lp;
	float dragDistance = Screen.height * 0.2f;

	// Use this for initialization
	void Start()
	{
		Player.position = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fp = touch.position;
			}
			if (touch.phase == TouchPhase.Ended)
			{
				lp = touch.position;
				float xDistance = Mathf.Abs(lp.x - fp.x);
				float yDistance = Mathf.Abs(lp.y - fp.y);

				if (xDistance > dragDistance || yDistance > dragDistance)
				{
					if (xDistance > yDistance)
					{
						if (lp.x > fp.x)
						{
							Debug.Log("Right Swipe");
//							Player.Translate(Vector3.right * 3f);
							iTween.MoveTo(Player.gameObject, iTween.Hash("y", 3f, "easetype", iTween.EaseType.linear, "time", 0.5f));
							iTween.MoveTo(Player.gameObject, iTween.Hash("y", 0f, "easetype", iTween.EaseType.linear, "time", 0.5f, "delay", 0.5f));
						}
						else
						{
							Debug.Log("Left Swipe");
//							Player.Translate(-Vector3.right * 3f);
							iTween.MoveTo(Player.gameObject, iTween.Hash("y", -3f, "easetype", iTween.EaseType.linear, "time", 0.5f));
							iTween.MoveTo(Player.gameObject, iTween.Hash("y", 0f, "easetype", iTween.EaseType.linear, "time", 0.5f, "delay", 0.5f));
						}
					}
					else
					{
						if (lp.y > fp.y)
						{
							Debug.Log("Up Swipe");
//							Player.Translate(Vector3.up * 3f);
							iTween.MoveTo(Player.gameObject, iTween.Hash("x", 3f, "easetype", iTween.EaseType.linear, "time", 0.5f));
							iTween.MoveTo(Player.gameObject, iTween.Hash("x", 0f, "easetype", iTween.EaseType.linear, "time", 0.5f, "delay", 0.5f));
						}
						else
						{
							Debug.Log("Down Swipe");
//							Player.Translate(Vector3.down * 3f);
							iTween.MoveTo(Player.gameObject, iTween.Hash("x", -3f, "easetype", iTween.EaseType.linear, "time", 0.5f));
							iTween.MoveTo(Player.gameObject, iTween.Hash("x", 0f, "easetype", iTween.EaseType.linear, "time", 0.5f, "delay", 0.5f));
						}
					}
				}
			}
		}
	}
}
