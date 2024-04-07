using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.CollisionDetectors
{
    public class TrooperSpawnAreaDetector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ITriggerTrooper triggerTrooper = collision.GetComponent<ITriggerTrooper>();
            if (triggerTrooper != null)
            {
                triggerTrooper.OnTriggerStartTroppers();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ITriggerTrooper triggerTrooper = collision.GetComponent<ITriggerTrooper>();
            if (triggerTrooper != null)
            {
                triggerTrooper.OnTriggerFinishTroopers();
            }
        }
    }
}
