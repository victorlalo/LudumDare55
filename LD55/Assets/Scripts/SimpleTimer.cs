using UnityEngine;
using System;

public class SimpleTimer
{
	float currTime = 0f;
	float targetTime = 0f;

	public bool IsTimerActive { get; private set; } = false;

	public event Action OnTimerEnd;
	
	public SimpleTimer(float t)
	{
		targetTime = t;
	}

	public void Tick()
	{
		if (IsTimerActive)
		{
			if (currTime >= targetTime)
			{
				IsTimerActive = false;
				OnTimerEnd?.Invoke();
			}
			currTime += Time.deltaTime;
		}
	}
	
	public void SetTargetTime(float t)
	{
		targetTime = t;
	}

	public void Reset()
	{
		currTime = 0f;
		IsTimerActive = true;
	}
	public void Start()
	{
		
		IsTimerActive = true;
	}

	public void Pause()
	{
		IsTimerActive = false;
	}
}