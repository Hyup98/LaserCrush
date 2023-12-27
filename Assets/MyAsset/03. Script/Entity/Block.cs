using Laser.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Laser.Object
{
    public class Block : ICollisionable
    {
        #region Property
        private int m_HP;
        private Vector2 m_Position;
        private DroppedItem m_Item = null;
        private List<Vector2> m_Direction = new List<Vector2>();
        #endregion

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="droppedItem"></param>
        /// ��� �������� ���� ��� �ΰ��� ����
        /// <param name="entityType"></param>
        /// <param name="hp"></param>
        public void Init(DroppedItem droppedItem, EntityType entityType, int hp)
        {
            m_HP = hp;
            m_Type = entityType;
            m_Item = droppedItem;
        }
        private void Destroy()
        {
            //������ �ִ� �������� �ʵ忡 ���� -> �� ���� �� ȹ�� ���
        }
        public override EntityType GetEntityType()
        {
            return m_Type;
        }

        public override void GetDamage(int damage)
        {
            if (m_HP < damage) // ���� �ǰ� ���������� ���� ���
            {
                int getDamage = Energy.UseEnergy(m_HP); //��� ������ �������� ��ȯ�޴´�. -> ������ ����
                if (m_HP - getDamage == 0)
                {
                    Destroy();
                }
                else
                {
                    m_HP -= getDamage;
                }
            }
            else
            {
                int getDamage = Energy.UseEnergy(damage);  //��� ������ �������� ��ȯ�޴´�.
                m_HP -= getDamage;
            }
        }

        public override bool IsAttackable()
        {
            if(m_Type == EntityType.NormalBlock || m_Type == EntityType.ReflectBlock)
            {
                return true;
            }
            return false;
        }
    }
}