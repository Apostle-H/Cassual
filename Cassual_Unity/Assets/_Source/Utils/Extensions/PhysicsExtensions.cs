using UnityEngine;

namespace Utils.Extensions
{
    public static class PhysicsExtensions
    {
        public static bool Contains(this LayerMask mask, int layer) => mask == (mask | (1 << layer));
    }
}