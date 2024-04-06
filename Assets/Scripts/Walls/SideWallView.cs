using Assets.Scripts.Helicopters;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Walls
{
    public class SideWallView : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollisionHandler collidedObject = collision.GetComponent<ICollisionHandler>();
            if (collidedObject != null)
            {
                collidedObject.OnCollisionDetected();
            }
        }
    }
}