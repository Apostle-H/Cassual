using System;
using Interactions.Damageable;
using UnityEngine;
using UnityEngine.AI;
using Utils.Extensions;

namespace Crowd
{
    public class CrowdMember : MonoBehaviour, Utils.Interfaces.Observable.IObserver<Vector3>
    {
        [SerializeField] private LayerMask turnMask;
        [SerializeField] protected MeshRenderer meshRenderer;
        
        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] protected LayerMask attackMask;
        [SerializeField] private int power;
        
        private Utils.Interfaces.Observable.IObservable<Vector3> _observable;
        private bool _isTurned;

        public event Action OnTurned;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!_isTurned || !turnMask.Contains(other.gameObject.layer) || !other.TryGetComponent(out CrowdMember crowdPerson))
                return;
            
            crowdPerson.Turn(_observable, meshRenderer.material);
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (!attackMask.Contains(other.gameObject.layer) || !other.gameObject.TryGetComponent(out IDamageable damageable) || damageable.IsDead)
                return;
            
            Attack(damageable);
        }

        protected void Turn(Utils.Interfaces.Observable.IObservable<Vector3> observable, Material material)
        {
            if (_isTurned)
                return;
            
            _isTurned = true;
            
            meshRenderer.material = material;

            navMeshAgent.enabled = true;
            _observable = observable;
            _observable.Add(this);
            OnTurned?.Invoke();
        }

        void Utils.Interfaces.Observable.IObserver<Vector3>.Update(Vector3 targetPos) => Move(targetPos);

        private void Move(Vector3 targetPos) => navMeshAgent.SetDestination(targetPos);

        private void Attack(IDamageable damageable)
        {
            if (!_isTurned)
            {
                return;
            }
            
            damageable.TakeDamage(power, meshRenderer.material);
            
            _observable.Remove(this);
            gameObject.SetActive(false);
        }
    }
}