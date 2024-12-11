using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[Header ("Health")]
    [SerializeField] private float startingHealth;
    
    public float currentHealth { get; private set; }
    private Animator _animator;
    private bool dead;

    [Header("Iframe")] 
    [SerializeField] private float invunerableTime;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer _spriteRenderer;
    
    [Header("SFX")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    
    private bool invunable;
    
    private void Awake()
    {
	    _animator = GetComponent<Animator>();
         currentHealth = startingHealth;
         _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
	    if (invunable)
	    {
		    return;
	    }
	    currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

	    if (currentHealth > 0)
	    {
		    _animator.SetTrigger("Hurt");
		    StartCoroutine(Invunerability()); //* NOTE: To call an IEnumerator, you need to use StartCoroutine. Otherwise, it won't work
		    SoundManager.Instance.PlaySound(hurtSound);
	    }
	    else
	    {
		    if (!dead)
		    {
			    _animator.SetTrigger("Die");
			    if (GetComponent<Player_Movement>() != null)
			    {
				    GetComponent<Player_Movement>().enabled = false; //* Disable player movement when dead
			    }
			    
			    //* Enemy death
			    if (GetComponentInParent<Patrolling>() != null)
			    {
				    GetComponentInParent<Patrolling>().enabled = false; //* Disable patrolling when dead

			    }
			    if (GetComponent<Knight>() != null)
			    {
				    GetComponent<Knight>().enabled = false; //* Disable knight when dead
			    }
			    SoundManager.Instance.PlaySound(deathSound);
			    dead = true;
		    }
	    }
    }

    public void AddHealth(float _value)
    {
	    currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    
    
    private IEnumerator Invunerability() //! NOTE: IEnumerator is a coroutine. And type correct, not IEnumberable!!!
	{
		invunable = true;
	    Physics2D.IgnoreLayerCollision(10, 11, true);
	    //* Ignore collision between player and enemy. Make sure to change the layer number to the correct one
	    for (int i = 0; i < numberOfFlashes; i++)
	    {
		    //* Make the player flash when invunerable
		    _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
		    yield return new WaitForSeconds(invunerableTime / (numberOfFlashes * 2)); //* Wait for half the time before changing back
		    _spriteRenderer.color = Color.white;
		    yield return new WaitForSeconds(invunerableTime / (numberOfFlashes * 2));
	    }
	    Physics2D.IgnoreLayerCollision(10, 11, false); //* Vunerable again
		invunable = false;
	}
}
