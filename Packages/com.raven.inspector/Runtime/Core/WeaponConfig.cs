using UnityEngine;

namespace Core
{
    [CreateAssetMenu]
    public sealed class
        WeaponConfig : ScriptableObject
    {
        public int Damage;

        public float CritChance;

        public float Range;
    }
}