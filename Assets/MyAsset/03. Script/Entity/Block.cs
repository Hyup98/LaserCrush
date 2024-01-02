using LaserCrush.Entity;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LaserCrush
{
    public class Block : MonoBehaviour, ICollisionable
    {
        #region Property
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private EEntityType m_Type;
        private EItemType m_Item;

        private EEntityType m_EntityType;
        private int m_HP = 1000;
        private bool m_IsDestroyed;
        #endregion

        private void Awake()
        {
            Init(1000, m_Type, EItemType.None);
        }

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="droppedItem"></param>
        /// ��� �������� ���� ��� �ΰ��� ����
        /// <param name="entityType"></param>
        /// <param name="hp"></param>
        public void Init(int hp, EEntityType entityType, EItemType ItemType)
        {
            m_HP = hp;
            m_EntityType = entityType;
            m_Item = ItemType;
            m_Text.text = m_HP.ToString();
        }

        private void Destroy()
        {
            if (m_IsDestroyed) return;
            m_IsDestroyed = true;
            Destroy(gameObject);

            if (m_Item != EItemType.None)
            {
                /* TODO
                 * ������ �ִ� ���������� DroppedItem��ü �ν��Ͻÿ���Ʈ �ؾ���
                 * AddDroppedItem -> ��������Ʈ �Լ��� ����ؼ� �迭�� �߰��ؾ���.
                 */
            }
        }

        public bool GetDamage(int damage)
        {
            //ü�� �������� �μ��� ���� �ٷ� return �ϵ��� ����
            if (m_HP <= damage) // ���� �ǰ� ���������� ���� ���
            {
                int getDamage = Energy.UseEnergy(m_HP); //��� ������ �������� ��ȯ�޴´�. -> ������ ����
                if (m_HP - getDamage == 0)
                {
                    Destroy();
                    return false;
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
            return true;
        }

        public bool IsGetDamageable()
        {
            return true;
        }

        public List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
        {
            List<Vector2> answer = new List<Vector2>();
            if (m_EntityType == EEntityType.ReflectBlock)//�ݻ� ���� ��츸 �ڽ� ����
            {
                Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
                return new List<Vector2>() { dir };
            }
            m_Text.text = m_HP.ToString();
            return answer;
        }

        public void MoveDown()
        {

        }
    }
}