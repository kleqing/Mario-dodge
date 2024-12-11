using System;
using UnityEngine;

public class ArrowProjectile : EnemyDamage //* Will damage the player
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;
    public void ActiveProjectile()
    {
	    lifetime = 0;
	    gameObject.SetActive(true);
    }

    private void Update()
    {
	    float movementSpeed = speed * Time.deltaTime;
	    transform.Translate(movementSpeed, 0, 0);
	    
	    lifetime += Time.deltaTime;
	    if (lifetime > resetTime)
	    {
		    gameObject.SetActive(false);
	    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    base.OnTriggerEnter2D(other); //* Call the base class OnTriggerEnter2D method (EnemyDamage), then continue with the code below
	    gameObject.SetActive(false); //* Disable the arrow when it hits the player
    }
}
