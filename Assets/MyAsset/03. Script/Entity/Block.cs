using Laser.Entity;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Laser.Object
{
    public class Block : ICollisionable
    {
        #region Property
        [SerializeField] private TextMeshProUGUI m_Text;

        private bool m_IsDestroyed;
        private int m_HP = 1000;
        private Vector2 m_Position;
        private Item m_Item = null;
        private List<Vector2> m_Direction = new List<Vector2>();
        #endregion

        private void Awake()
        {
            Init(1000, EntityType.NormalBlock, null);
        }

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

        private void Destroy()
        {
            if (m_IsDestroyed) return;
            m_IsDestroyed = true;
            Destroy(gameObject);
            //������ �ִ� �������� �ʵ忡 ���� -> �� ���� �� ȹ�� ���
        }

        public override void GetDamage(int damage)
        {
            Debug.Log("GetDamage");

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