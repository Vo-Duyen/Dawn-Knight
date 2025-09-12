using Sirenix.OdinInspector;
using UnityEngine;

namespace DanlangA
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/New Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Title("Info")] 
        public string nameKnight;
        public float maxHealth;
        public float moveSpeed;
        public float damage;

        [Title("Show")] 
        [PreviewField(100)] public Sprite icon;
        [PreviewField(100)] public GameObject prefab;
    }
}