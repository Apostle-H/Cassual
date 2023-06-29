using UnityEngine;

namespace Utils.Services
{
    public static class MathService
    {
        public static Vector3 RadiusPlacePosition(Vector3 center, float offset, int index)
        {
            if (index == 0)
                return center;
            
            int currentIndex = index;
            int iteration = 1;
            Vector3 radiusOffset = Vector3.zero;
            float rotationAngle = 360f;
            
            while (rotationAngle * currentIndex >= 360)
            {
                currentIndex -= (int)(360f / rotationAngle);
                iteration++;
                
                radiusOffset = Vector3.right * offset * iteration;
                rotationAngle = Vector3.Angle(radiusOffset, radiusOffset + Vector3.forward * offset);
            }

            Vector3 finalOffset = Quaternion.AngleAxis(rotationAngle * currentIndex, Vector3.up) * radiusOffset;
            return center + finalOffset;
        }
    }
}