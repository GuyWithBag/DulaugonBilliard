using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
	private static UnityMainThreadDispatcher instance;
	private static readonly Queue<Action> executionQueue = new Queue<Action>();

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public static UnityMainThreadDispatcher Instance()
	{
		if (instance == null)
		{
			GameObject obj = new GameObject("UnityMainThreadDispatcher");
			instance = obj.AddComponent<UnityMainThreadDispatcher>();
		}
		return instance;
	}

	void Update()
	{
		lock (executionQueue)
		{
			while (executionQueue.Count > 0)
			{
				executionQueue.Dequeue().Invoke();
			}
		}
	}

	public void Enqueue(Action action)
	{
		lock (executionQueue)
		{
			executionQueue.Enqueue(action);
		}
	}
}