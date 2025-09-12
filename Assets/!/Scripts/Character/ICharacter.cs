using UnityEngine;

namespace DanlangA
{
    public interface ICharacter
    {
        public Transform Tf { get; }
        public Rigidbody2D Rd { get; set; }
        
    }
}