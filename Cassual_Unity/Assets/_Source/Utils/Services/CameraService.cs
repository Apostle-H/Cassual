using UnityEngine;

namespace Utils.Services
{
    public static class CameraService
    {
        private static readonly Camera CAMERA = Camera.main;

        public static Vector3 ScreenToWorldPoint(Vector3 screenPoint) => CAMERA.ScreenToWorldPoint(screenPoint);

        public static Ray ScreenPointToRay(Vector3 screenPoint) => CAMERA.ScreenPointToRay(screenPoint);
    }
}