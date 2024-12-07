using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   [SerializeField] private Transform previousRoom;
   [SerializeField] private Transform newRoom;

   [SerializeField] private CameraController _cameraController;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         if (other.transform.position.x < transform.position.x)
         {
            _cameraController.MoveToNewRoom(newRoom);
         }
         else
         {
            _cameraController.MoveToNewRoom(previousRoom);
         }
      }
   }
}
