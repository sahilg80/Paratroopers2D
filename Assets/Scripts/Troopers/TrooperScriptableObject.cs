
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    [CreateAssetMenu(fileName = "Trooper", menuName = "ScriptableObjects/TrooperSO")]
    public class TrooperScriptableObject : ScriptableObject
    {
        public float WalkingSpeed;
        public Sprite CrateSprite;
        public Sprite ParachuteSprite;
        public Sprite ParatrooperSprite;
        public Sprite DeathSprite;
        public int KillReward;
    }
}
