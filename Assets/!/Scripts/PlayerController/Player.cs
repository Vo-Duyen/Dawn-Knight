using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DanlangA
{
    public class Player : PlayerBase
    {
        private void FixedUpdate()
        {
            Rd.velocity = moveAmount * Time.deltaTime * PlayerData.moveSpeed;
        }
    }
}