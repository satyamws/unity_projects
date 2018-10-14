﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
	public int health = 100;
	public Color hitColor = Color.white;

	public Transform player = null;
	public GameObject cannonball = null;

	public float minDelay = 1.0f;
	public float maxDelay = 4.0f;

	private float lastTime = 0.0f;
	private float delayTime = 0.0f;
	private Color originalColor = Color.white;

	void Awake ()
	{
		originalColor = this.GetComponent<Renderer>().material.color;
	}

	void Update ()
	{
		FollowPlayer ();

		Shoot ();
	}

	void FollowPlayer ()
	{
		this.transform.LookAt (player);
	}

	void Shoot ()
	{
		if ( Time.time > lastTime + delayTime )
		{
			lastTime = Time.time;

			delayTime = GetRandomValue ();

			GameObject obj = Instantiate ( cannonball, this.transform.position, this.transform.rotation ) as GameObject;
		
			obj.name = "cannonball";
		}
	}

	float GetRandomValue ()
	{
		return Random.Range ( minDelay, maxDelay );
	}

	void OnTriggerEnter ( Collider other )
	{
		if ( other.name == "magicOrb" )
		{
			int hp = other.GetComponent<MagicOrb>().hitpoint;

			GetHealth ( hp );
		}
	}

	void GetHealth ( int hp )
	{
		if ( health > 0 )
		{
			health -= hp;

			StartCoroutine ( GetHit ());
		}
		else
		{
			Destroy ( this.gameObject );
		}
	}

	IEnumerator GetHit ()
	{
		this.GetComponent<Renderer>().material.color = hitColor;

		yield return new WaitForSeconds (0.4f);

		this.GetComponent<Renderer>().material.color = originalColor;
	}
}









