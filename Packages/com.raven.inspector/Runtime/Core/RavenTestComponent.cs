using System.Collections.Generic;
using Attributes;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class PlayerStats
    {
        [ValidateInput("ValidateHealth")]
        public int Health;

        public float Speed;
    }
    
    public class RavenTestComponent : MonoBehaviour
    {
        [HorizontalGroup("TopRow")]
        public int Gold;

        [HorizontalGroup("TopRow")]
        public int Gems;
        
        [FoldoutGroup("Combat")]
        public int Damage;

        [FoldoutGroup("Combat")]
        public int Armor;
        
        [TabGroup("Stats", "Basic")]
        public int Health;

        [TabGroup("Stats", "Basic")]
        public int Mana;

        [TabGroup("Stats", "Advanced")]
        public int Speed;

        [TabGroup("Stats", "Advanced")]
        public bool AdvancedMode;

        public PlayerStats Stats;

        [ShowIf("AdvancedMode")]
        public float AdvancedSpeed;

        [ReadOnly]
        public string PlayerName;

        [EnableIf("AdvancedMode")]
        public List<int> Values;
        
        [InlineEditor]
        public WeaponConfig Weapon;
        
        [Button]
        private void PrintMessage()
        {
            Debug.Log(
                "Raven Button Pressed");
        }
        
        private bool ValidateHealth(
            int value,
            out string message)
        {
            if (value < 0)
            {
                message =
                    "Health cannot be negative.";

                return false;
            }

            message = "";

            return true;
        }
    }
}