using UnityEngine;
using System.Collections;

public class droneScript : MonoBehaviour {

	public GameObject tpCamera;
	public GameObject tpCameraFront;
	public GameObject fpCameraOrtho;
	public GameObject fpCameraPersp;
	public GameObject activeCamera;

	public float moveSpeed;
	public float forwardSpeed;
	public float sideSpeed;
	public float backSpeed;
	public float idleMoveSpeed;

	public float damping;
	public float rotationSpeed;

	public float zoomSpeed;
	public float zoomedOut;
	public float zoomedIn;

	private float baseForwardSpeed;
	private float baseBackSpeed;
	private float baseSideSpeed;

	private bool movingForward;
	private bool movingBackward;

	//private Vector3 position;

	// Use this for initialization
	void Start ()
	{
		tpCamera.camera.depth = -1;
		tpCameraFront.camera.depth = -3;
		fpCameraOrtho.camera.depth = -2;
		fpCameraPersp.camera.depth = 0;

		fpCameraOrtho.camera.enabled = false;
		fpCameraPersp.camera.enabled = true;
		tpCamera.camera.enabled = false;
		tpCameraFront.camera.enabled = false;

		movingForward = true;
		movingBackward = false;

		baseForwardSpeed = forwardSpeed;
		baseBackSpeed = backSpeed;
		baseSideSpeed = sideSpeed;

		//position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fpCameraOrtho.camera.enabled == true)
		{
			if (activeCamera != fpCameraOrtho)
			{
				activeCamera = fpCameraOrtho;
			}
		}

		else if (fpCameraPersp.camera.enabled == true)
		{
			if (activeCamera != fpCameraPersp)
			{
				activeCamera = fpCameraPersp;
			}
		}

		else if (tpCamera.camera.enabled == true)
		{
			if (activeCamera != tpCamera)
			{
				activeCamera = tpCamera;
			}
		}

		else if (tpCameraFront.camera.enabled == true)
		{
			if (activeCamera != tpCameraFront)
			{
				activeCamera = tpCameraFront;
			}
		}

		if (movingBackward)
		{
			//position.x -= idleMoveSpeed * Time.deltaTime;
			//this.transform.position = position;

			this.transform.position += transform.forward * -idleMoveSpeed * Time.deltaTime;
		}

		if (movingForward)
		{
			//position.x += idleMoveSpeed * Time.deltaTime;
			//this.transform.position = position;

			this.transform.position += transform.forward * idleMoveSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			//position.x += forwardSpeed * Time.deltaTime;

			this.transform.position += transform.forward * forwardSpeed * Time.deltaTime;

			movingBackward = false;
			movingForward = true;
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			//position.x -= backSpeed * Time.deltaTime;

			this.transform.position += transform.forward * -backSpeed * Time.deltaTime;

			movingForward = false;
			movingBackward = true;
		}

		if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
		{
			//position.z += sideSpeed * Time.deltaTime;

			//var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

			this.transform.Rotate(Vector3.up * Time.deltaTime * -rotationSpeed);
		}

		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
		{
			//position.z -= sideSpeed * Time.deltaTime;

			this.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
		}

		if (Input.GetKey(KeyCode.A))
		{
			//position.y += backSpeed * Time.deltaTime;

			this.transform.position += transform.up * backSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D))
		{
			//position.y -= backSpeed * Time.deltaTime;

			this.transform.position += transform.up * -backSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.Z))
		{
			activeCamera.camera.fieldOfView = Mathf.Lerp(activeCamera.camera.fieldOfView, zoomedIn, Time.deltaTime * zoomSpeed);
		}

		if (Input.GetKey(KeyCode.X))
		{
			activeCamera.camera.fieldOfView = Mathf.Lerp(activeCamera.camera.fieldOfView, zoomedOut, Time.deltaTime * zoomSpeed);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			forwardSpeed = baseForwardSpeed * 2;
			backSpeed = baseBackSpeed * 2;
			sideSpeed = baseSideSpeed * 2;
		}

		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (forwardSpeed != baseForwardSpeed * 2)
			{
				forwardSpeed = baseForwardSpeed * 2;
			}

			if (backSpeed != baseBackSpeed * 2)
			{
				backSpeed = baseBackSpeed * 2;
			}

			if (sideSpeed != baseSideSpeed * 2)
			{
				sideSpeed = baseSideSpeed * 2;
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			forwardSpeed = baseForwardSpeed;
			backSpeed = baseBackSpeed;
			sideSpeed = baseSideSpeed;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			tpCamera.camera.depth = -2;
			tpCameraFront.camera.depth = -3;
			fpCameraOrtho.camera.depth = -1;
			fpCameraPersp.camera.depth = 0;

			Debug.Log("Perspective View");

			fpCameraOrtho.camera.enabled = false;
			fpCameraPersp.camera.enabled = true;
			tpCamera.camera.enabled = false;
			tpCameraFront.camera.enabled = false;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			tpCamera.camera.depth = -2;
			tpCameraFront.camera.depth = -3;
			fpCameraOrtho.camera.depth = 0;
			fpCameraPersp.camera.depth = -1;

			Debug.Log("Orthographic View");

			fpCameraOrtho.camera.enabled = true;
			fpCameraPersp.camera.enabled = false;
			tpCamera.camera.enabled = false;
			tpCameraFront.camera.enabled = false;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			tpCamera.camera.depth = 0;
			tpCameraFront.camera.depth = -3;
			fpCameraOrtho.camera.depth = -1;
			fpCameraPersp.camera.depth = -2;

			Debug.Log("Third Person View");

			fpCameraOrtho.camera.enabled = false;
			fpCameraPersp.camera.enabled = false;
			tpCamera.camera.enabled = true;
			tpCameraFront.camera.enabled = false;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			tpCamera.camera.depth = -3;
			tpCameraFront.camera.depth = 0;
			fpCameraOrtho.camera.depth = -1;
			fpCameraPersp.camera.depth = -2;
			
			Debug.Log("Third Person Front View");
			
			fpCameraOrtho.camera.enabled = false;
			fpCameraPersp.camera.enabled = false;
			tpCamera.camera.enabled = false;
			tpCameraFront.camera.enabled = true;
		}
	}
}