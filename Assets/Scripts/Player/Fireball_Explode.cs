using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Explode : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifetime;
    
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
            
        lifetime += Time.deltaTime;
        if (lifetime > 3)
        {
            gameObject.SetActive(false);
        }
    }
    
    //* Check if the fireball hits the enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        hit = true;
        boxCollider2D.enabled = false;
        animator.SetTrigger("Explode");

        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(1);
        }
        
    }

    public void Direction(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
