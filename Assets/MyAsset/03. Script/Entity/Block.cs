using LaserCrush.Entity;
using System.Collections.Generic;
using UnityEngine;
using LaserCrush.Data;
using System;
using TMPro;
using LaserCrush.Manager;

namespace LaserCrush
{
    public sealed class Block : MonoBehaviour, ICollisionable
    {
        #region Variable
        [SerializeField] private BlockData m_BlockData;

        private DroppedItem m_DroppedItem;
        private SpriteRenderer m_SpriteRenderer;
        private TextMeshProUGUI m_Text;

        private EEntityType m_EntityType;

        private int m_HP;
        private int m_AttackCount;
        private bool m_IsDestroyed;

        private Action<Block, DroppedItem> m_RemoveBlockAction;
        #endregion

        public int BlockScore { get; private set; }
        public int RowNumber { get; private set; }
        public int ColNumber { get; private set; }

        private void Awake()
        {
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        /// <summary>
        /// ȭ�鿡 ���� �� �ݵ�� �ʱ�ȭ�Լ� ȣ���� ��
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="entityType"></param>
        /// <param name="droppedItem">��� �������� ���� ��� �ΰ��� ����</param>
        public void Init(int hp, int rowNumber, int colNumber, EEntityType entityType, DroppedItem droppedItem, Action<Block, DroppedItem> removeBlockAction)
        {
            m_HP = hp;
            BlockScore = hp;
            RowNumber = rowNumber;
            ColNumber = colNumber;
            m_EntityType = entityType;
            m_DroppedItem = droppedItem;
            m_AttackCount = 0;
            m_Text.text = GetHP().ToString();
            m_SpriteRenderer.color = (m_EntityType == EEntityType.NormalBlock) ?
                m_BlockData.NormalBlockColor :
                m_BlockData.ReflectBlockColor;

            m_RemoveBlockAction = removeBlockAction;
        }

        private void Destroy()
        {
            Manager.AudioManager.AudioManagerInstance.PlayOneShotNormalSE("BlockDestroy");
            m_IsDestroyed = true;
            m_RemoveBlockAction?.Invoke(this, m_DroppedItem);
            Destroy(gameObject);
        }

        public void DestoryGameOver()
        {
            m_IsDestroyed = true;
            Destroy(gameObject);
        }

        public bool GetDamage(int damage)
        {
            m_AttackCount++;
            // Ÿ��Ƚ�� ��� �����
            //damage = damage * (int)(1.5 * (m_AttackCount / 15));

            //���� �������� ��� ����� ���
            damage = damage * (int)(GameManager.m_StageNum + 1 / 2);

            if (m_IsDestroyed) return false;

            if (m_HP <= damage) // ���� �ǰ� ���������� ���� ���
            {
                Energy.UseEnergy(m_HP);
                Destroy();
                return false;
            }
            else m_HP -= Energy.UseEnergy(damage);

            if (GetHP() == 0)
            {
                Energy.UseEnergy(m_HP);
                Destroy();
                return false;
            }
            m_Text.text = GetHP().ToString();
            return true;
        }

        public bool IsGetDamageable()
        {
            return true;
        }

        public bool Waiting()
        {
            return true;
        }

        public List<LaserInfo> Hitted(RaycastHit2D hit, Vector2 parentDirVector, Laser laser)
        {
            List<LaserInfo> answer = new List<LaserInfo>();
            laser.ChangeLaserState(ELaserStateType.Hitting);
            if (m_EntityType == EEntityType.ReflectBlock)//�ݻ� ���� ��츸 �ڽ� ����
            {
                //Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
                LaserInfo info = new LaserInfo
                {
                    Direction = Vector2.Reflect(parentDirVector, hit.normal),
                    Position = hit.point
                };
                return new List<LaserInfo>() { info };
            }
            m_Text.text = GetHP().ToString();
            return answer;
        }

        public void MoveDown(Vector3 moveDownVector)
        {
            transform.position += moveDownVector;
            RowNumber++;
        }

        private void OnDestroy()
            => m_RemoveBlockAction = null;

        private int GetHP()
        {
            return m_HP / 100;
        }

        EEntityType ICollisionable.GetEEntityType()
        {
            return EEntityType.NormalBlock;
        }
    }
}