using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
public class Notifications : MonoBehaviour 
{
	static Notifications instance;
	public static Notifications Instance
	{
		get 
		{
			return instance;
		}
	}

	struct NotifRequest
	{
		public string header;
		public string content;

		public NotifRequest(string h, string c)
		{
			header = h;
			content = c;
		}
	}
	public static void RequestNotification(string header, string text)
	{
		if(instance != null)
		{
			instance.NewNotif(header, text);
		}
	}
	public RoadDisplay prefab;
	void NewNotif(string header, string text)
	{
		requestQueue.Enqueue(new NotifRequest(header, text), 0);
		UnrollRequests();
	}

	bool playing;
	void UnrollRequests()
	{
		if(playing == false && requestQueue.Count > 0)
		{
			NotifRequest re = requestQueue.Dequeue();
			RoadDisplay g = Instantiate(prefab, transform);
			g.StartThing(Finished, re.header, re.content);
			playing = true;
		}
	}

	void Finished()
	{
		playing = false;
		UnrollRequests();
	}

	SimplePriorityQueue<NotifRequest> requestQueue;
	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		requestQueue = new SimplePriorityQueue<NotifRequest>();
	}
}
