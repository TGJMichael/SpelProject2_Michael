using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int XPNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

    }
}
