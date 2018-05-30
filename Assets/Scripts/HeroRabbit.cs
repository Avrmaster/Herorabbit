using UnityEngine;

public class HeroRabbit : MonoBehaviour
{

	public float Speed = 2;
	public float JumpSpeed = 6.66f;
	private Rigidbody2D _bunny;
	private SpriteRenderer _sprite;
	
	// Use this for initialization
	private void Start ()
	{
		_bunny = GetComponent<Rigidbody2D>();
		_sprite = GetComponent<SpriteRenderer>();
	}
	
	private void FixedUpdate()
	{
		var value = Input.GetAxis("Horizontal");
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		if (value != 0)
			_sprite.flipX = value < 0;
		
		if ((Mathf.Abs(value) > 0))
		{
			var velocity = _bunny.velocity;
			velocity.x = value * Speed;
			_bunny.velocity = velocity;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			var velocity = _bunny.velocity;
			velocity.y = JumpSpeed;
			_bunny.velocity = velocity;
		}
		
	}
}
