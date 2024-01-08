using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Data
{
    [CreateAssetMenu(fileName = "Scriptable Data", menuName = "Scriptable/Laser Data", order = int.MaxValue)]
    public class LaserData : ScriptableObject
    {
        [Tooltip("1 �����Ӵ� ������")]
        [SerializeField] private int m_Damage = 1;

        [Tooltip("������ �����ϴ� �ӵ�")]
        [SerializeField] private float m_EraseVelocity = 0.4f;

        [Tooltip("������ �߻��ϴ� �ӵ�")]
        [SerializeField] private float m_ShootingVelocity = 0.2f;

        public int Damage { get => m_Damage; }
        public float EraseVelocity { get => m_EraseVelocity; }
        public float ShootingVelocity { get => m_ShootingVelocity; }
    }
}
