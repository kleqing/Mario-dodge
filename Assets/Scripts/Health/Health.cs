using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    
    public float currentHealth { get; private set; }
    private Animator _animator;
    private bool dead;

    private void Awake()
    {
	    _animator = GetComponent<Animator>();
         currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
	    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

	    if (currentHealth > 0)
	    {
		    _animator.SetTrigger("Hurt");
	    }
	    else
	    {
		    if (!dead)
		    {
			    _animator.SetTrigger("Die"); 
			    GetComponent<Player_Movement>().enabled = false; //* Disable player movement when dead
			    dead = true;
		    }
	    }
    }

}
