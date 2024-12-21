using UnityEngine;

public class Magician : MonoBehaviour
{
	[Header("Attack Parameters")]
	[SerializeField] private float attackCooldown;
	[SerializeField] private float range;
	[SerializeField] private int damage;
	
	[Header("Attack")]
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject[] fireballs;
	
	[Header("Collider Parameters")]
	[SerializeField] private float ColliderDistance;
	private float cooldownTimer = Mathf.Infinity;
	
	[SerializeField] private BoxCollider2D attackCollider;
    
	[Header("Player Layer")]
	[SerializeField] private LayerMask playerLayer;
	
	private Health playerHealth;

	private Animator animator;
    
	//private Patrolling patrolling;

	[Header("Sound")] 
	[SerializeField] private AudioClip AttackSound;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		//patrolling = GetComponentInParent<Patrolling>(); //* Get the patrolling component from the parent object, not the current object
	}
	
	private void Update()
	{
		cooldownTimer += Time.deltaTime;
        
		//* Attack only if see the player
		if (PlayerInSight())
		{
			if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
			{
				cooldownTimer = 0;
				animator.SetTrigger("RangeAttack");
				SoundManager.Instance.PlaySound(AttackSound);
			}
		}

		// if (patrolling != null)
		// {
		// 	patrolling.enabled = !PlayerInSight(); //* Disable patrolling when player is in sight
		// }
	}

	private void Attack()
	{
		cooldownTimer = 0;
		//* Shoot a fireball
		fireballs[FindFireball()].transform.position = firePoint.localScale;
		fireballs[FindFireball()].GetComponent<ArrowProjectile>().ActiveProjectile();
	}
	
	private int FindFireball()
	{
		for (int i = 0; i < fireballs.Length; i++)
		{
			if (!fireballs[i].activeInHierarchy)
			{
				return i;
			}
		}
		return 0;
	}
	
	private bool PlayerInSight()
	{
		//* Check if player is in sight
		RaycastHit2D hit = Physics2D.BoxCast(attackCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, 
			new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z), 
			0, Vector2.left, 0, playerLayer);
		return hit.collider != null;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(attackCollider.bounds.center + transform.right * range * transform.localScale.x * ColliderDistance, 
			new Vector3(attackCollider.bounds.size.x * range, attackCollider.bounds.size.y, attackCollider.bounds.size.z));
	}

}
