using System;
using Buildings.Signals;
using deVoid.Utils;
using UnityEngine;

namespace BankSystem
{
    public class Bank : MonoBehaviour
    {
        public int Amount { get; private set; }

        public event Action OnAmountUpdate;

        private TowerCapturedSignal _towerCapturedSignal;
        
        private void Awake()
        {
            _towerCapturedSignal = Signals.Get<TowerCapturedSignal>();
            
            Add(10);
            Bind();
        }

        private void Bind() => _towerCapturedSignal.AddListener(Add);

        public void Add(int amount)
        {
            Amount = Amount + amount < 0 ? 0 : Amount + amount;
            
            OnAmountUpdate?.Invoke();
        }
    }
}