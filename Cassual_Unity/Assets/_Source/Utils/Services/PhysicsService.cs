using UnityEngine;

namespace Utils.Services
{
    public static class PhysicsService
    {
        public static RaycastHit RayFromCamera(Vector3 screenPoint, LayerMask layerMask)
        {
            Ray ray = CameraService.ScreenPointToRay(screenPoint);
            Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask);
            
            return hit;
        }
    }
}