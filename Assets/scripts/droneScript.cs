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
	
	public GameObject drone;
	public Transform droneReset;
	
	public bool forwardTilt;
	public bool backwardTilt;
	public bool leftwardTilt;
	public bool rightwardTilt;
	
	public float yTilt;
	public float zTilt;
	public float increaseTilt;
	public float defaultTilt;
	
	private float tiltY;
	private float tiltZ;
	
	public float smooth;
	
	
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
		
		droneReset.transform.rotation = drone.transform.rotation;
		
		forwardTilt = false;
		backwardTilt = false;
		leftwardTilt = false;
		rightwardTilt = false;
		
		//position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		droneTilt();
		
		/*if (Input.GetButton (buttonName: "joystick button 0"))
		{
			Debug.Log ("shit");
		}*/
		
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
			//this.transform.position = positi
			
			this.transform.position += transform.forward * -idleMoveSpeed * Time.deltaTime;
		}
		
		if (movingForward)
		{
			//position.x += idleMoveSpeed * Time.deltaTime;
			//this.transform.position = position;
			
			this.transform.position += transform.forward * idleMoveSpeed * Time.deltaTime;
		}
		
		//Moves the Drone forwards
		if (Input.GetKey(KeyCode.W))
		{
			//position.x += forwardSpeed * Time.deltaTime;
			
			this.transform.position += transform.forward * forwardSpeed * Time.deltaTime;
			
			movingBackward = false;
			movingForward = true;
			
		}
		
		//moves the drone backwards
		if (Input.GetKey(KeyCode.S))
		{
			//position.x -= backSpeed * Time.deltaTime;
			
			this.transform.position += transform.forward * -backSpeed * Time.deltaTime;
			
			movingForward = false;
			movingBackward = true;
		}
		
		//Rotates the Drone to the Left
		if (Input.GetKey(KeyCode.A))
		{
			//position.z += sideSpeed * Time.deltaTime;
			
			//var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			
			this.transform.Rotate(Vector3.up * Time.deltaTime * -rotationSpeed);
		}
		
		//Rotates the Drone to the right
		if (Input.GetKey(KeyCode.D))
		{
			//position.z -= sideSpeed * Time.deltaTime;
			
			this.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
		}
		
		//Increases the Drone's altitude
		if (Input.GetKey(KeyCode.UpArrow))
		{
			//position.y += backSpeed * Time.deltaTime;
			
			this.transform.position += transform.up * backSpeed * Time.deltaTime;
		}
		
		//Decreases the Drones altitude
		if (Input.GetKey(KeyCode.DownArrow))
		{
			//position.y -= backSpeed * Time.deltaTime;
			
			this.transform.position += transform.up * -backSpeed * Time.deltaTime;
		}
		
		//Strafes the Drone to the Right
		if(Input.GetKey (KeyCode.RightArrow))
		{
			
			this.transform.position += transform.right * backSpeed * Time.deltaTime;
			
		}
		
		//Strafes the Drone to the Left
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			
			this.transform.position += transform.right * -backSpeed * Time.deltaTime;
			
		}
		
		//Zooms the camera in
		if (Input.GetKey(KeyCode.Z))
		{
			activeCamera.camera.fieldOfView = Mathf.Lerp(activeCamera.camera.fieldOfView, zoomedIn, Time.deltaTime * zoomSpeed);
		}
		
		//Zooms the Camera Out
		if (Input.GetKey(KeyCode.X))
		{
			activeCamera.camera.fieldOfView = Mathf.Lerp(activeCamera.camera.fieldOfView, zoomedOut, Time.deltaTime * zoomSpeed);
		}
		
		//Increases the Drones speed on first frame of Shift press
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			forwardSpeed = baseForwardSpeed * 2;
			backSpeed = baseBackSpeed * 2;
			sideSpeed = baseSideSpeed * 2;
		}
		
		//Keeps the Drones speed increased as the player holds the Shift key
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
		
		//Returns the Drones speed back to normal when shift is released
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			forwardSpeed = baseForwardSpeed;
			backSpeed = baseBackSpeed;
			sideSpeed = baseSideSpeed;
		}
		
		//Changes the Camera to Camera 1
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
		
		//Changes the Camera to Camera 2
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
		
		//Changes the camera to Camera 3
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
		
		//Changes the camera to Camera 4
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
	
	//determines which direction the drone is tilting for flight
	void droneTilt(){
		
		//the Drone will tilt forward
		
		drone.transform.localRotation = Quaternion.RotateTowards (drone.transform.localRotation, Quaternion.Euler (0, tiltY, tiltZ), smooth);
		
		if(Input.GetKey(KeyCode.W))
		{
			tiltZ = zTilt;
		}
		else if(!Input.GetKey(KeyCode.S))
		{
			tiltZ = 0;
		}
		
		
		//The drone will tilt backwards
		if(Input.GetKey(KeyCode.S))
		{
			tiltZ = -zTilt;
		}
		else if (!Input.GetKey(KeyCode.W))
		{
			tiltZ = 0;
		}
		
		//The drone will tilt left
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			tiltY = yTilt;
		}
		else if(!Input.GetKey (KeyCode.RightArrow))
		{
			tiltY = 0;
		}
		
		//the drone will tilt left
		if(Input.GetKey (KeyCode.RightArrow))
		{
			tiltY = -yTilt;
		}
		else if(!Input.GetKey(KeyCode.LeftArrow))
		{
			tiltY = 0;
		}
		
		if(Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			zTilt = increaseTilt;
			yTilt = increaseTilt;
		}
		else 
		{
			zTilt = defaultTilt;
			yTilt = defaultTilt;
		}
		
	}
	
}