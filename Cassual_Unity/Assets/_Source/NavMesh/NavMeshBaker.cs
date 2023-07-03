using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

namespace NavMesh
{
    public class NavMeshBaker : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface surface;

        public void Bake()
        {
            StartCoroutine(BakeRoutine());
        }

        private IEnumerator BakeRoutine()
        {
            yield return new WaitForFixedUpdate();
            surface.BuildNavMesh();
        }
    }
}