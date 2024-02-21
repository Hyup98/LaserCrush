using UnityEngine;

namespace LaserCrush.Extension
{
    public static class ExtensionVector2
    {
        public static Vector2 DiscreteDirection(this Vector2 direction, int discreteUnit)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;

            //10���� == �� 36���� ����
            float fixedAngle = (int)(angle / discreteUnit) * discreteUnit;
            float radianAnble = fixedAngle * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(radianAnble), Mathf.Sin(radianAnble));
        }

        public static Vector2 ClampDirection(this Vector2 direction, float min, float max)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // x�� �� �������� ������ min������ max���� ����
            float clampedAngle = Mathf.Clamp(angle, min, max);
            float radianAnble = clampedAngle * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(radianAnble), Mathf.Sin(radianAnble));
        }
    }
}
