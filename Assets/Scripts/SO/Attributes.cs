using UnityEngine;

namespace PlayerSpace
{
    [CreateAssetMenu(menuName = "RPG Generator/player/Create Attribute")]
    public class Attributes : ScriptableObject
    {
        public string Description;
        public Sprite Thumbnail;
    }

}