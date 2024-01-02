using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Entity
{

    public class Wall : MonoBehaviour, ICollisionable
    {
        #region Property
        private EEntityType m_EntityType;
        #endregion
        public void Awake()
        {
            m_EntityType = EEntityType.Wall;
            Debug.Log("�� �ʱ�ȭ");
        }

        public List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
        {
            Debug.Log("���� �浹 �� �ڽĻ���");
            Debug.Log("���� �浹 �� ������ ���");
            Energy.CollidWithWall();
            Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
            return new List<Vector2>() { dir };
        }

        public bool IsGetDamageable()
        {
            return false;
        }

        public void GetDamage(int damage)
        {
            return;
        }
    }
}