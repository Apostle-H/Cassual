using Crowd;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CrowdFormer crowdFormer;
        
        private void Start()
        {
            crowdFormer.Bind();
        }
    }
}