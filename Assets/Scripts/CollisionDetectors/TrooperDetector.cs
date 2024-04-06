using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.CollisionDetectors
{
    public class TrooperDetector : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            IGroundLandable groundedObject = collision.gameObject.GetComponent<IGroundLandable>();
            if (groundedObject != null)
            {
                groundedObject.OnTouchGround();
            }
        }
    }
}
