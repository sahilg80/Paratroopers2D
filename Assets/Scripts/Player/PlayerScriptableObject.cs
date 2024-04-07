
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public float BulletLaunchSpeed;
        public float RotationSpeed;
        public float MaxRotationAngle;
    }
}
