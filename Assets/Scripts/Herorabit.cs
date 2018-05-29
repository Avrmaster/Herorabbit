using System;
using UnityEngine;

public class Herorabit : MonoBehaviour
{

	public float Speed = 2;
	private Rigidbody2D _bunny;
	
	// Use this for initialization
	private void Start ()
	{
		_bunny = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate()
	{
		var value = Input.GetAxis("Horizontal");
		if ((Mathf.Abs(value) > 0))
		{
			var velocity = _bunny.velocity;
			velocity.x = value * Speed;
			_bunny.velocity = velocity;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			var velocity = _bunny.velocity;
			velocity.y = 2;
			_bunny.velocity = velocity;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			_bunny.MoveRotation(_bunny.rotation+10);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			_bunny.MoveRotation(_bunny.rotation-10);
		}	
		
	}
}
