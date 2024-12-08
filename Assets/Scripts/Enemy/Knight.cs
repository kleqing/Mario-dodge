using System;
using UnityEngine;

public class Knight : MonoBehaviour
{
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float ColliderDistance;
    private float cooldownTimer = Mathf.Infinity;
    
    [SerializeField] private BoxCollider2D attackCollider;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    
    private Health playerHealth;

    private Animator animator;
    
    private Patrolling patrolling;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        patrolling = GetComponentInParent<Patrolling>(); //* Get the patrolling component from the parent object, not the current object
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        //* Attack only if see the player
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("Attack");
            }
        }

        if (patrolling != null)
        {
            patrolling.enabled = !PlayerInSight(); //* Disable patrolling when player is in sight
        }
    }
    
    private bool PlayerInSight()
    {
        //* Check if player is in sight
        RaycastHit2D hit = Physics2D.BoxCast(attackCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, 
            new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>(); //* Get the player health component
        }
        return hit.collider != null;
    }
    
    //* Draw the attack collider in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, 
            new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            //* Damage the player
            playerHealth.TakeDamage(damage);
        }
    }
}
