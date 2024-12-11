using System;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Arrows;
    
    [SerializeField] private AudioClip arrowSound;

    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.Instance.PlaySound(arrowSound);
        Arrows[FindArrow()].transform.position = firePoint.position;
        Arrows[FindArrow()].GetComponent<ArrowProjectile>().ActiveProjectile();
        
    }

    private int FindArrow()
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            if (!Arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > attackCooldown)
        {
            Attack();
        }
    }
}
