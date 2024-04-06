using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletSO")]
    public class BulletScriptableObject : ScriptableObject
    {
        public int Speed;
    }
}
