using System;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utils.Extensions;
using Utils.Services;
using Zenject;

namespace Crowd
{
    public class CrowdFormer : CrowdMember, Utils.Interfaces.Observable.IObservable<Vector3>
    {
        [SerializeField] private LayerMask navigableMask;
        [SerializeField] private float offset;
        
        private readonly HashSet<Utils.Interfaces.Observable.IObserver<Vector3>> _observers = new HashSet<Utils.Interfaces.Observable.IObserver<Vector3>>();

        private MainActions.CrowdActions _crowdActions;

        private Vector3 _destination;

        [Inject]
        private void Init(MainActions.CrowdActions crowdActions) => _crowdActions = crowdActions;

        private void Awake() => Turn(this, meshRenderer.material);

        public void Bind() => _crowdActions.SetDestination.started += SetDestination;

        public void Expose() => _crowdActions.SetDestination.started -= SetDestination;

        public void Add(Utils.Interfaces.Observable.IObserver<Vector3> newObserver)
        {
            _observers.Add(newObserver);
            
            Notify();
        }

        public void Remove(Utils.Interfaces.Observable.IObserver<Vector3> observer) => _observers.Remove(observer);

        private void SetDestination(InputAction.CallbackContext ctx)
        {
            Vector2 screenPoint = _crowdActions.Destination.ReadValue<Vector2>();
            _destination = PhysicsService.RayFromCamera(screenPoint, navigableMask).point;
            
            Notify();
        }

        private void Notify() => _observers.ForEach((observer, index) => observer.Update(MathService.RadiusPlacePosition(_destination, offset, index)));
    }
}