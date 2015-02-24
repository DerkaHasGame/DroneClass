using UnityEngine;
using System.Collections;

public class missionsScript : MonoBehaviour {

	// TRACK MISSION

	private float trackTimer;
	private float losingTimer;
	public float trackTimeLimit;
	public float losingTimeLimit;
	
	public float trackDisplayDistance;
	public float trackStartDistance;
	public float trackDistanceLimit;
	
	public float trackMissionCompletedTime;
	public int trackMissionFails;

	private bool trackMissionStarted;
	private bool trackMissionFailed;
	private bool trackMissionComplete;

	// CARRY MISSION

	public float carryDisplayDistance;
	public float carryStartDistance;

	public float carryMissionCompletedTime;
	public int carryMissionFails;

	private bool carryMissionStarted;
	private bool carryMissionFailed;
	private bool carryMissionComplete;

	// RING MISSION

	private float ringTimer;
	public float ringTimeLimit;
	private int ringNumber;

	public float ringDisplayDistance;
	public float ringStartDistance;

	public float ringMissionCompletedTime;
	public float ringRecordTime = 6.7f;
	public int ringMissionFails;

	private bool ringMissionStarted;
	private bool ringMissionFailed;
	private bool ringMissionComplete;

	// ALL MISSIONS VARIABLES

	public bool onMission;

	public Transform pc;
	public Transform trackedObject;
	public Transform pcCarryPoint;
	public Transform pickUpPoint;
	public Transform dropOffPoint;
	public Transform carryObject;
	public Transform[] rings;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// ONLY ONE MISSION ACTIVE AT A TIME

		if (trackMissionStarted || carryMissionStarted || ringMissionStarted)
		{
			if (!onMission)
			{
				onMission = true;
			}
		}

		// NOT ON MISSION IF NONE ARE ACTIVE

		else if (!trackMissionStarted && !carryMissionStarted && !ringMissionStarted)
		{
			if (onMission)
			{
				onMission = false;
			}
		}

		// TRACK MISSION START

		if (Vector3.Distance(pc.position, trackedObject.position) <= trackDisplayDistance && !trackMissionStarted && !trackMissionFailed && !trackMissionComplete && !onMission)
		{
			if (Vector3.Distance(pc.position, trackedObject.position) > trackStartDistance)
			{
				Debug.Log ("There is a mission nearby! Distance Away - " + System.Math.Round(Vector3.Distance(pc.position, trackedObject.position), 2));
			}

			else if (Vector3.Distance(pc.position, trackedObject.position) <= trackStartDistance)
			{
				trackMissionStarted = true;
			}
		}

		if (Vector3.Distance(pc.position, trackedObject.position) > trackDisplayDistance && trackMissionFailed)
		{
			trackMissionFailed = false;
		}

		if (trackMissionStarted && !trackMissionComplete)
		{
			trackMissionCompletedTime += Time.deltaTime;

			if (Vector3.Distance(pc.position, trackedObject.position) <= trackDistanceLimit)
			{
				trackTimer += Time.deltaTime;

				if (losingTimer != 0)
				{
					losingTimer = 0;
				}

				Debug.Log ("Great! Stay close to it! " + System.Math.Round(trackTimer, 2) + "s / " + trackTimeLimit + "s");
				
				if (trackTimer >= trackTimeLimit)
				{
					Debug.Log ("You did it in " + System.Math.Round(trackMissionCompletedTime, 1) + "s! Look around for more missions.");

					trackMissionComplete = true;

					trackMissionStarted = false;
				}
			}

			else if (Vector3.Distance(pc.position, trackedObject.position) > trackDistanceLimit)
			{
				losingTimer += Time.deltaTime;

				Debug.Log ("You're too far away! Distance Away - " + System.Math.Round(Vector3.Distance(pc.position, trackedObject.position), 2) + " / " + trackDistanceLimit + " Hurry! - " + System.Math.Round(losingTimer, 2) + "s / " + losingTimeLimit + "s");

				if (losingTimer >= losingTimeLimit)
				{
					trackMissionFails++;

					Debug.Log ("Mission Attempt #" + trackMissionFails + " Failed. Go back to the start to try again!");

					trackMissionCompletedTime = 0;
					trackTimer = 0;

					trackMissionFailed = true;

					trackMissionStarted = false;
				}
			}
		}

		// TRACK MISSION END

		// CARRY MISSION START

		if (Vector3.Distance(pcCarryPoint.position, pickUpPoint.position) <= carryDisplayDistance && !carryMissionStarted && !carryMissionFailed && !carryMissionComplete && !onMission)
		{
			if (Vector3.Distance(pcCarryPoint.position, pickUpPoint.position) > carryStartDistance)
			{
				Debug.Log ("There is a mission nearby! Distance Away - " + System.Math.Round(Vector3.Distance(pcCarryPoint.position, pickUpPoint.position), 2));
			}
			
			else if (Vector3.Distance(pcCarryPoint.position, pickUpPoint.position) <= carryStartDistance)
			{
				carryMissionStarted = true;
			}
		}

		if (Vector3.Distance(pcCarryPoint.position, pickUpPoint.position) > carryDisplayDistance && carryMissionFailed)
		{
			carryMissionFailed = false;
		}

		if (carryMissionStarted && !carryMissionComplete)
		{
			carryMissionCompletedTime += Time.deltaTime;

			Vector3 pos;
			pos.x = pcCarryPoint.position.x;
			pos.y = pcCarryPoint.position.y - 4;
			pos.z = pcCarryPoint.position.z;
			
			if (carryObject.position != pos)
			{
				carryObject.position = pos;
			}

			if (Vector3.Distance(pcCarryPoint.position, dropOffPoint.position) > carryStartDistance)
			{
				Debug.Log ("Distance to drop off - " + System.Math.Round(Vector3.Distance(pcCarryPoint.position, dropOffPoint.position), 2));
			}

			else if (Vector3.Distance(pcCarryPoint.position, dropOffPoint.position) <= carryStartDistance)
			{
				Debug.Log ("You did it in " + System.Math.Round(carryMissionCompletedTime, 1) + "s! Look around for more missions.");

				carryMissionComplete = true;

				carryMissionStarted = false;
			}
		}

		// CARRY MISSION END

		// RING MISSION START

		if (Vector3.Distance(pc.position, rings[ringNumber].position) <= ringDisplayDistance && !ringMissionStarted && !ringMissionFailed && !ringMissionComplete && !onMission)
		{
			if (Vector3.Distance(pc.position, rings[ringNumber].position) > ringStartDistance)
			{
				Debug.Log ("There is a mission nearby! Distance Away - " + System.Math.Round(Vector3.Distance(pc.position, rings[ringNumber].position), 2));
			}
			
			else if (Vector3.Distance(pc.position, rings[ringNumber].position) <= ringStartDistance)
			{
				rings[ringNumber].gameObject.SetActive(false);

				ringTimer = ringTimeLimit;

				ringNumber = 1;

				ringMissionStarted = true;
			}
		}

		if (Vector3.Distance(pc.position, rings[0].position) > ringDisplayDistance && ringMissionFailed)
		{
			ringMissionFailed = false;
		}

		if (ringMissionStarted && !ringMissionComplete)
		{
			ringMissionCompletedTime += Time.deltaTime;

			Debug.Log ("Time Left - " + ringTimer + "s  Rings Left - " + (rings.Length - ringNumber));

			if (Vector3.Distance(pc.position, rings[ringNumber].position) > ringStartDistance)
			{
				ringTimer -= Time.deltaTime;

				if (ringTimer <= 0)
				{
					ringMissionFails++;

					Debug.Log ("Mission Attempt #" + ringMissionFails + " Failed. Go back to the start to try again!");

					for (int i = 0; i < rings.Length; i++)
					{
						rings[i].gameObject.SetActive(true);
					}

					ringNumber = 0;
					ringTimer = 0;
					ringMissionCompletedTime = 0;

					ringMissionFailed = true;

					ringMissionStarted = false;
				}
			}

			else if (Vector3.Distance(pc.position, rings[ringNumber].position) <= ringStartDistance)
			{
				if (ringNumber >= rings.Length - 1)
				{
					if (ringMissionCompletedTime < ringRecordTime)
					{
						Debug.Log("New record time! You did it in " + System.Math.Round(ringMissionCompletedTime, 1) + "s! Old record was " + System.Math.Round(ringRecordTime, 1) + "s! Look around for more missions.");

						ringRecordTime = ringMissionCompletedTime;
					}

					else
					{
						Debug.Log ("You did it in " + System.Math.Round(ringMissionCompletedTime, 1) + "s! The record is " + System.Math.Round(ringRecordTime, 1) + "s! Look around for more missions.");
					}

					rings[ringNumber].gameObject.SetActive(false);
					
					ringMissionComplete = true;
					
					ringMissionStarted = false;
				}

				else
				{
					rings[ringNumber].gameObject.SetActive(false);

					ringTimer += ringTimeLimit;
					
					ringNumber ++;
				}
			}
		}
		
		// RING MISSION END
	}
}