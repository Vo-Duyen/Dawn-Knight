using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.WaitForSecondCache
{
    public class WaitForSecondCache
    {
        private static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds Get(float time)
        {
            if (!WaitForSeconds.ContainsKey(time))
            {
                WaitForSeconds[time] = new WaitForSeconds(time);
            }
            return WaitForSeconds[time];
        }
    }
}
