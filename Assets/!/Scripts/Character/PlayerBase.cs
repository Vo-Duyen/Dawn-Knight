using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace DanlangA
{
    public class PlayerBase : SerializedMonoBehaviour, ICharacter
    {
        [Title("Info Base")]
        public Transform Tf => transform;
        [OdinSerialize, Required] public PlayerData PlayerData { get; set; }
        [OdinSerialize, Required] public Rigidbody2D Rd { get; set; }
        [ReadOnly] public Vector2 moveAmount;

    }
}