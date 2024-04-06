using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Troopers
{
    [CreateAssetMenu(fileName = "Trooper", menuName = "ScriptableObjects/TrooperSO")]
    public class TrooperScriptableObject : ScriptableObject
    {
        public float MovementSpeed;
        public Sprite SpriteAfterDestroy;
    }
}
