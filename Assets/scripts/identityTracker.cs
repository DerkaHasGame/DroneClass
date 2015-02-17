using UnityEngine;
using System.Collections;

public class identityTracker : MonoBehaviour {
	
	public GameObject toolTip;

	private bool canClick;
	private bool clicked;

	// Use this for initialization
	void Start ()
	{
		if (this.gameObject.name == "PC")
		{
			toolTip.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			clicked = true;

			if (canClick)
			{
				Debug.Log("Clicked Tree #1");
			}
		}

		if (!canClick && Input.GetButtonDown("Fire1") && this.gameObject.name == "PC")
		{
			clicked = false;

			Debug.Log("Clicked Nothing");
		}

		if (clicked)
		{
			if (this.gameObject.name == "PC")
			{
				toolTip.SetActive(true);
			}
		}

		else if (!clicked)
		{
			if (this.gameObject.name == "PC")
			{
				toolTip.SetActive(false);
			}
		}
	}

	void OnMouseEnter ()
	{
		canClick = true;

		Debug.Log("Tree #1");
	}

	void OnMouseExit ()
	{
		canClick = false;

		Debug.Log("Nothing");
	}
}