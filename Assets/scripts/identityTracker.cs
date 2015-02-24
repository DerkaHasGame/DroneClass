using UnityEngine;
using System.Collections;

public class identityTracker : MonoBehaviour {
	
	public GameObject toolTip;

	public GameObject hoveredObject;
	public GameObject[] objects;

	private bool canClick;
	private bool clicked;

	// Use this for initialization
	void Start ()
	{
		toolTip.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1") && canClick)
		{
			toolTip.SetActive(true);
		}

		if (Input.GetButtonDown("Fire1") && !canClick)
		{
			toolTip.SetActive(false);
		}
	}

	void OnMouseEnter (Collider other)
	{
		hoveredObject = other.gameObject;

		canClick = true;

		Debug.Log("Tree #1");
	}

	void OnMouseExit ()
	{
		canClick = false;

		Debug.Log("Nothing");
	}
}