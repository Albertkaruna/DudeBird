using UnityEngine;
using System.Collections;

public class Obstackle : MonoBehaviour
{
	private UIManagement UIM;
	private bool IsPlayerAttacked;

	void Start()
	{
		IsPlayerAttacked = false;
		UIM = GameObject.Find("ScoreCanvas").GetComponent<UIManagement>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			Collider2D cd = GetComponent<Collider2D>();
			cd.enabled = false;
			StartCoroutine(ActCol(cd));
			Destroy(Instantiate(UIM.Effect, col.transform.position, Quaternion.identity), 2f);
			UIM.Clip_Sounds.PlayOneShot(UIM.Attack);
			UIManagement.Lives--;
			IsPlayerAttacked = true;
			UIM.LivesCheck();
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.name == "DestroyAllExit")
		{
			if (!IsPlayerAttacked)
			{
				UIM.Score++;
			}
			IsPlayerAttacked = false;
			Destroy(gameObject);
		}
	}

	IEnumerator ActCol(Collider2D ccc)
	{
		yield return new WaitForSeconds(0.3f);
		ccc.enabled = true;
	}
        
}
