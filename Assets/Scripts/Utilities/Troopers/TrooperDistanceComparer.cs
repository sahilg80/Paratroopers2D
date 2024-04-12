using Assets.Scripts.Troopers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities.Troopers
{
    public class TrooperDistanceComparer : IComparer<TrooperController>
    {
        private Vector3 referencePosition;

        public TrooperDistanceComparer(Vector3 referencePosition)
        {
            this.referencePosition = referencePosition;
        }

        public int Compare(TrooperController x, TrooperController y)
        {
            float distanceX = Vector3.Distance(referencePosition, x.TrooperView.transform.position);
            float distanceY = Vector3.Distance(referencePosition, y.TrooperView.transform.position);

            // Compare the distances
            if (distanceX < distanceY)
                return -1;
            else if (distanceX > distanceY)
                return 1;
            else
                return 0;
        }
    }
}
