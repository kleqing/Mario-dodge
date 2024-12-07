using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //* Camera follows the player
    [SerializeField] private Transform player;
    [SerializeField] private float cameraAhead;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    
    private void Update()
    {
        //* Camera movement for Room Transition
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), 
        //     ref velocity, speed);
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        //* Lerp mean get the latest value from the origin value
        lookAhead = Mathf.Lerp(lookAhead, (cameraAhead * transform.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
