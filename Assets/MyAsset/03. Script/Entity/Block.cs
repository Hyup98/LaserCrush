using LaserCrush.Entity;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LaserCrush
{
    public class Block : ICollisionable
    {
        #region Property
        [SerializeField] private TextMeshProUGUI m_Text;

        private List<Vector2> m_Direction = new List<Vector2>();
        private Item m_Item = null;

        private Vector2 m_Position;

        private int m_HP = 1000;
        private bool m_IsDestroyed;
        #endregion

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="droppedItem"></param>
        /// ��� �������� ���� ��� �ΰ��� ����
        /// <param name="entityType"></param>
        /// <param name="hp"></param>
        public void Init(int hp, EntityType entityType, Item droppedItem)
        {
            m_HP = hp;
            m_Type = entityType;
            m_Item = droppedItem;

            m_Text.text = hp.ToString();
        }

        public void Destroy()
        {
            if (m_IsDestroyed) return;
            m_IsDestroyed = true;
            Destroy(gameObject);
            //������ �ִ� �������� �ʵ忡 ���� -> �� ���� �� ȹ�� ���
        }

        public override void GetDamage(int damage)
        {
            Debug.Log("GetDamage");

            //ü�� �������� �μ��� ���� �ٷ� return �ϵ��� ����
            //�ӽ� �������� ���� �ȶ߰� ���Ƴ��� ������

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
            m_Text.text = m_HP.ToString();
        }

        public override bool IsAttackable()
        {
            return true;
        }

        public override List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
        {
            Debug.Log("Block Hitted");

            List<Vector2> answer = new List<Vector2>();
            if (m_Type == EntityType.ReflectBlock)//�ݻ� ���� ��츸 �ڽ� ����
            {
                Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
                return new List<Vector2>() { dir };
            }
            return answer;
        }
    }
}