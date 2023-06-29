using Crowd;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CrowdMaster crowdMaster;
        
        private void Start()
        {
            crowdMaster.Bind();
        }
    }
}