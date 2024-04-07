
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    [CreateAssetMenu(fileName = "Helicopter", menuName = "ScriptableObjects/HelicopterSO")]
    public class HelicopterScriptableObject : ScriptableObject
    {
        public float Speed;
        public int KillReward;
    }
}
