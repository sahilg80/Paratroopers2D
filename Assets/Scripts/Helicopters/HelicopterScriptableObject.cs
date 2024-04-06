using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helicopters
{
    [CreateAssetMenu(fileName = "Helicopter", menuName = "ScriptableObjects/HelicopterSO")]
    public class HelicopterScriptableObject : ScriptableObject
    {
        public float Speed;
    }
}
