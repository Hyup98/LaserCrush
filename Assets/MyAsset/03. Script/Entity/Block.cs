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
        #region Variable
        [SerializeField] private BlockData m_BlockData;

        private DroppedItem m_DroppedItem;
        private SpriteRenderer m_SpriteRenderer;
        private TextMeshProUGUI m_Text;

        private EEntityType m_EntityType;

        private int m_HP = 1000;
        private bool m_IsDestroyed;

        private Action<Block, DroppedItem> m_RemoveBlockAction;
        #endregion

        private void Awake()
        {
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="entityType"></param>
        /// <param name="droppedItem">��� �������� ���� ��� �ΰ��� ����</param>
        public void Init(int hp, EEntityType entityType, DroppedItem droppedItem, Action<Block, DroppedItem> removeBlockAction)
        {
            m_HP = hp;
            m_EntityType = entityType;
            m_DroppedItem = droppedItem;

            m_Text.text = m_HP.ToString();
            m_SpriteRenderer.color = (m_EntityType == EEntityType.NormalBlock) ? 
                m_BlockData.NormalBlockColor : 
                m_BlockData.ReflectBlockColor;

            m_RemoveBlockAction = removeBlockAction;
        }

        private void Destroy()
        {
            m_IsDestroyed = true;
            m_RemoveBlockAction?.Invoke(this, m_DroppedItem);
            Destroy(gameObject);
        }

        public bool GetDamage(int damage)
        {
            if (m_IsDestroyed) return false;

            if (m_HP <= damage) // ���� �ǰ� ���������� ���� ���
            {
                Energy.UseEnergy(m_HP);
                Destroy();
                return false;
            }
            else m_HP -= Energy.UseEnergy(damage);

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
                //Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
                Vector2 dir = Vector2.Reflect(parentDirVector, hit.normal);

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