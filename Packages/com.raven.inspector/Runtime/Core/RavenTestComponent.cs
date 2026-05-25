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
        [Tab("Stats")]
        public int Health;

        [Tab("Stats")]
        public int Mana;

        [Tab("Advanced")]
        public float Speed;
        
        public bool AdvancedMode;

        public PlayerStats Stats;

        [ShowIf("AdvancedMode")]
        public float AdvancedSpeed;

        [ReadOnly]
        public string PlayerName;

        [EnableIf("AdvancedMode")]
        public List<int> Values;
        
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