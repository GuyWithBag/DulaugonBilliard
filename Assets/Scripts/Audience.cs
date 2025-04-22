using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
	public Animator[] obj;
	private float animationTimer = 0f;
	private bool isAnimating = false;
	public static int c = 0;

	void Start()
	{
		obj = GetComponentsInChildren<Animator>(true);


	}

	void Update()
	{
		if (c == 1 && !isAnimating)
		{
			StartAnimation();
		}

		if (isAnimating)
		{
			animationTimer += Time.deltaTime;

			if (animationTimer >= 5f)
			{
				StopAnimation();
			}
		}
	}

	void StartAnimation()
	{
		
		foreach (Animator t in obj)
		{
			
				t.SetFloat("Blend", 1.0f, 0.5f, 0.5f);

		}
			
		isAnimating = true;
		animationTimer = 0f;
	}

	void StopAnimation()
	{
		foreach (Animator t in obj)
		{
			
				t.SetFloat("Blend", 0f);

		}
			
		isAnimating = false;
		animationTimer = 0f;
	}
}
