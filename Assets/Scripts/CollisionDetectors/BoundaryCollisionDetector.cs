using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.CollisionDetectors
{
    public class BoundaryCollisionDetector : MonoBehaviour
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
                collidedObject.OnCollisionWithBoundary();
            }
        }
    }
}