using UnityEngine;
using UnityEngine.AI;
using Utils.Extensions;
using Utils.Interfaces.Observable;

namespace Crowd
{
    public class CrowdMember : MonoBehaviour, IObserver<Vector3>
    {
        [SerializeField] private LayerMask turnMask;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Collider turnCollider;
        [SerializeField] protected MeshRenderer meshRenderer;
        
        private IObservable<Vector3> _observable;
        private bool _isTurned;

        private void OnTriggerEnter(Collider other)
        {
            if (!_isTurned)
                return;

            if (!turnMask.Contains(other.gameObject.layer) || !other.TryGetComponent(out CrowdMember crowdPerson))
                return;
            
            crowdPerson.Turn(_observable, meshRenderer.material);
            Physics.IgnoreCollision(turnCollider, other);
        }

        protected void Turn(IObservable<Vector3> observable, Material material)
        {
            if (_isTurned)
                return;
            
            _isTurned = true;
            
            meshRenderer.material = material;
            
            _observable = observable;
            _observable.Add(this);
        }

        void IObserver<Vector3>.Update(Vector3 targetPos) => Move(targetPos);

        private void Move(Vector3 targetPos) => navMeshAgent.SetDestination(targetPos);
    }
}