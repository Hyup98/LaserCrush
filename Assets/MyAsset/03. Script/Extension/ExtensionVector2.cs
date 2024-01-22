using System.Collections;
using System.Collections.Generic;
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

            return new Vector2(Mathf.Cos(fixedAngle * Mathf.Deg2Rad), Mathf.Sin(fixedAngle * Mathf.Deg2Rad));
        }

        public static Vector2 ClampDirection(this Vector2 direction, float min, float max)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // x�� �� �������� ������ min������ max���� ����
            float clampedAngle = Mathf.Clamp(angle, min, max);

            return new Vector2(Mathf.Cos(clampedAngle * Mathf.Deg2Rad), Mathf.Sin(clampedAngle * Mathf.Deg2Rad));
        }
    }
}
