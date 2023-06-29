using System;
using UnityEngine;

namespace Interactions.Damageable
{
    public interface IDamageable
    {
        public event Action OnDamaged;
        public event Action OnDeath;

        public bool IsDead { get; }
        
        public void TakeDamage(int amount, Material killMaterial);
    }
}