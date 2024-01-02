using LaserCrush.Entity;
using System.Collections.Generic;
using UnityEngine;
using LaserCrush.Data;
using System;
using TMPro;

namespace LaserCrush
{
    public class Block : MonoBehaviour, ICollisionable
    {
        #region Property
        [SerializeField] private BlockData m_BlockData;

        private DroppedItem m_DroppedItem;
        private SpriteRenderer m_SpriteRenderer;
        private TextMeshProUGUI m_Text;

        private EItemType m_Item;
        private EEntityType m_EntityType;

        private int m_HP = 1000;
        private bool m_IsDestroyed;

        private Action<Block> m_RemoveBlockAction;
        #endregion

        public DroppedItem DroppedItem { get => m_DroppedItem; }

        private void Awake()
        {
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="droppedItem"></param>
        /// ��� �������� ���� ��� �ΰ��� ����
        /// <param name="entityType"></param>
        /// <param name="hp"></param>
        public void Init(int hp, EEntityType entityType, EItemType itemType, Action<Block> removeBlockAction)
        {
            m_HP = hp;
            m_EntityType = entityType;
            m_Item = itemType;

            m_Text.text = m_HP.ToString();
            m_SpriteRenderer.color = (m_EntityType == EEntityType.NormalBlock) ? 
                m_BlockData.NormalBlockColor : 
                m_BlockData.ReflectBlockColor;

            m_RemoveBlockAction = removeBlockAction;
        }

        private void Destroy()
        {
            m_IsDestroyed = true;
            m_RemoveBlockAction?.Invoke(this);
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
            if (m_IsDestroyed) return false;

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

        private void OnDestroy()
        {
            m_RemoveBlockAction = null;
        }
    }
}