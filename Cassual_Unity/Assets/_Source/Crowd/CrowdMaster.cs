using System.Collections.Generic;
using System.Linq;
using InputSystem;
using Interactions.Damageable;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Extensions;
using Utils.Services;
using Zenject;
using Utils.Interfaces.Observable;

namespace Crowd
{
    public class CrowdMaster : CrowdMember, IObservable<Vector3>
    {
        [SerializeField] private LayerMask navigableMask;
        [SerializeField] private float offset;
        
        private readonly HashSet<IObserver<Vector3>> _observers = new HashSet<IObserver<Vector3>>();

        private MainActions.CrowdActions _crowdActions;

        private Vector3 _destination;

        [Inject]
        private void Init(MainActions.CrowdActions crowdActions) => _crowdActions = crowdActions;

        private void Awake() => Turn(this, meshRenderer.material);

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (!attackMask.Contains(other.gameObject.layer) || !other.TryGetComponent(out IDamageable _))
                return;

            CrowdAttack(other.transform.position);
        }

        protected override void OnCollisionEnter(Collision other) { }

        public void Bind() => _crowdActions.SetDestination.started += SetDestination;

        public void Expose() => _crowdActions.SetDestination.started -= SetDestination;

        public void Add(Utils.Interfaces.Observable.IObserver<Vector3> newObserver)
        {
            _observers.Add(newObserver);
            
            Notify();
        }

        public void Remove(IObserver<Vector3> observer) => _observers.Remove(observer);

        private void SetDestination(InputAction.CallbackContext ctx)
        {
            Vector2 screenPoint = _crowdActions.Destination.ReadValue<Vector2>();
            _destination = PhysicsService.RayFromCamera(screenPoint, navigableMask).point;
            
            Notify();
        }

        private void Notify() => _observers.ForEach((observer, index) => observer.Update(MathService.RadiusPlacePosition(_destination, offset, index)));

        private void CrowdAttack(Vector3 targetPos)
        {
            for (int i = 1; i < _observers.Count; i++)
            {
                _observers.ElementAt(i).Update(targetPos);
            }
        }
    }
}