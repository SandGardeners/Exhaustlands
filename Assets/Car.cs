using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour {

	[System.Serializable]
	public struct SpeedSettings
	{
		[SerializeField]
		public float maxVelocity;

		[SerializeField]
		public float maxSteer;

		[SerializeField]
		public float slowingRadius;

		[SerializeField]
		public AudioSource audioSource;

		[SerializeField]
		public float minSizeCamera;

		[SerializeField]
		public string presetName;
	}

	[SerializeField] 
	SpeedSettings onRoadSettings;

	[SerializeField]
	SpeedSettings onDirtSettings;
	[SerializeField]
	SpeedSettings onMotorwaySettings;
	
	[SerializeField] 
	SpeedSettings offRoadSettings;
	SpeedSettings currentSettings;

	SpeedSettings CurrentSettings
	{
		get
		{
			return currentSettings;
		}
		set
		{
			if(value.presetName != currentSettings.presetName)
			{
				currentSource.DOFade(0f, 0.35f);
				currentSource = value.audioSource;
				currentSettings.audioSource = value.audioSource;
				currentSettings.minSizeCamera = value.minSizeCamera;
				currentSettings.presetName = value.presetName;
				DOTween.To(()=>{return currentSettings.maxSteer;}, (x)=>{currentSettings.maxSteer = x;}, value.maxSteer, 3.5f);
				DOTween.To(()=>{return currentSettings.maxVelocity;}, (x)=>{currentSettings.maxVelocity = x;}, value.maxVelocity, 3.5f);
				DOTween.To(()=>{return cam.m_Lens.OrthographicSize;}, (x)=>{cam.m_Lens.OrthographicSize = x;}, currentSettings.minSizeCamera, 3.5f);
			}
		}
	}

	public AudioSource currentSource;

	Vector3 velocity;

	SpriteRenderer sr;

	TrailRenderer[] trails;

	public Cinemachine.CinemachineVirtualCamera cam;

	[SerializeField]

	float driftingThreshold;

	[SerializeField]
	Animator anim;
	Dictionary<string, SpeedSettings> roadSettings;

	[SerializeField]
	Transform trackerTM;

	void Start () 
	{
		roadSettings = new Dictionary<string, SpeedSettings>();
		roadSettings["Road"] = onRoadSettings;
		roadSettings["Motorway"] = onMotorwaySettings;
		roadSettings["Dirt"] = onDirtSettings;
		sr = GetComponent<SpriteRenderer>();
		trails = GetComponentsInChildren<TrailRenderer>();
		foreach(TrailRenderer tr in trails)
		{
			tr.emitting = false;
		}
		CurrentSettings = offRoadSettings;
		TeamManager.teamChanged += SetWiz;
		lastPos = transform.position;
	}
	bool _drifting;
	bool drifting
	{
		get
		{
			return _drifting;
		}
		set
		{
			if(_drifting != value)
			{
				foreach(TrailRenderer tr in trails)
				{
					tr.emitting = value;
				}
			}
			_drifting = value;
		}
	}
	float angle;

	void SetWiz()
	{
		bool hasWiz = TeamManager.IsInTeam("Wizard");
		// Debug.Log(hasWiz);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("car"),LayerMask.NameToLayer("ForceField"),hasWiz);
	}
	Vector3 lastPos;
	[SerializeField]
	float distThreshold;
	
	[SerializeField]
	float ratioTM = 2f;
	void Update()
	{
		Vector3 target;


		bool canMove = InterfaceManager.CanCarMove();
		if(Input.GetMouseButton(0) && canMove)
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else
		{
			target = transform.position;
		}
		Vector3 vel = Seek(transform.position, target);

		anim.speed = 0.8f * (vel.magnitude/onMotorwaySettings.maxVelocity);
		if(currentSettings.maxVelocity != 0f && currentSource != null)
		{
			currentSource.volume = Mathf.Clamp01(vel.magnitude/currentSettings.maxVelocity);
		}
		transform.position += vel * Time.deltaTime;
		// trackerTM.localPosition = vel*Time.deltaTime*ratioTM;
		if(vel.magnitude > 0 && Vector3.Distance(lastPos, transform.position) > distThreshold)
		{
			TimeManager.Instance.Progress();
		}
		lastPos = transform.position;
		if(Input.GetMouseButton(0) && canMove)
		{
			angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
		}
		
		float driftAngle = Vector3.Angle(vel,vecDesired);
		if(driftAngle > driftingThreshold)
		{
			drifting = true;
		}
		else
		{
			drifting = false;
		}
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + velocity);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + vecDesired);
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position,currentSettings.slowingRadius);
	}

	Vector3 vecDesired;
	Vector3 vecSteer;
	Vector3 Seek(Vector3 current, Vector3 target)
	{
		vecDesired = target - current;
		vecDesired.z = 0f;
		float distance = vecDesired.magnitude;
		if (distance < currentSettings.slowingRadius) 
		{
    		vecDesired = vecDesired.normalized * currentSettings.maxVelocity * (distance / currentSettings.slowingRadius);
		}	
		else 
		{
    		vecDesired = vecDesired.normalized * currentSettings.maxVelocity;
		}
		vecSteer = vecDesired - velocity;
		if(vecSteer.magnitude > currentSettings.maxSteer)
		{
			vecSteer.Normalize();
			vecSteer *= currentSettings.maxSteer;
		}

		velocity += vecSteer;

		if(velocity.magnitude > currentSettings.maxVelocity)
		{
			velocity.Normalize();
			velocity *= currentSettings.maxVelocity * (TimeManager.HasFuel()?1.0f:0.5f);
		}

		return velocity;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Road" || other.tag == "Motorway" || other.tag == "Dirt")
		{
			CurrentSettings = roadSettings[other.tag];
			RoadManager.Instance.NewRoad(other.name);
		}
	}
	[SerializeField]
	GameObject bubble;

	string curDialogue;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Bubble" && !donebubbles.Contains(other.name) && TeamManager.HasPassenger())
		{
			curDialogue = other.name;
			bubble.SetActive(true);
		}	
	}

	List<string> donebubbles = new List<string>();
	public void TriggerDialogueBubble()
	{
		InterfaceManager.Instance.RequestDisplay(BaseDisplay.DISPLAY_TYPES.TEXT, "DIALOGUES." +curDialogue);
		bubble.SetActive(false);
		donebubbles.Add(curDialogue);
		curDialogue = string.Empty;
	}


	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Road" || other.tag == "Motorway" || other.tag == "Dirt")
		{
			CurrentSettings = TeamManager.IsInTeam("Warrior")?onDirtSettings:offRoadSettings;
			RoadManager.Instance.NewRoad("???");
		}
		else if(other.tag == "Bubble")
		{
			curDialogue = string.Empty;
			bubble.SetActive(false);
		}
	}
}
