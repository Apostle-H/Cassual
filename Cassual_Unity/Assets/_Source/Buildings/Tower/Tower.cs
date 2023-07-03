using System;
using Buildings.Signals;
using Interactions.Damageable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utils.Extensions;

namespace Buildings
{
    public class Tower : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int Power { get; private set; }
        [SerializeField] private int worth;

        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private UnityEvent capturedEvents; 

        public bool IsDead { get; private set; }

        private TowerCapturedSignal _towerCapturedSignal;
        
        private void Awake()
        {
            _towerCapturedSignal = deVoid.Utils.Signals.Get<TowerCapturedSignal>();
        }

        public event Action OnDamaged;
        public event Action OnDeath;

        public void TakeDamage(int amount, Material killMaterial)
        {
            Power -= amount;
            
            OnDamaged?.Invoke();
            if (Power > 0) 
                return;
            
            IsDead = true;
            meshRenderer.material = killMaterial;
            OnDeath?.Invoke();
            _towerCapturedSignal.Dispatch(worth);
            
            capturedEvents.Invoke();
        } 
    }
}