using System;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [Header("Patroll points")]
    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;

    [Header("Enemy Movement")] 
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;

    private Vector3 initialScale;
    
    [Header("Enemy Animation")]
    [SerializeField] private Animator animator;
    
    private bool movingLeft;

    [Header("Idle Time")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    
    private void Awake()
    {
        
        initialScale = enemy.localScale;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("Moving", true);
        //* Face the enemy in the direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);
        
        //* Move the enemy in the direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= LeftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                //* Change direction
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= RightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                //* Change direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("Moving", false);
        
        idleTimer += Time.deltaTime;
        
        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void OnDisable() //* Disable the animator parameter when the object is disabled or the object is destroyed
    {
        animator.SetBool("Moving", false);
    }
}
