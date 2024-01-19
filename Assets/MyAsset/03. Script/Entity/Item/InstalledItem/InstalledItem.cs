using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LaserCrush.Extension;
using LaserCrush.Manager;
using TMPro;

public struct LaserInfo
{
    public Vector2 Position;
    public Vector2 Direction;

    public LaserInfo(Vector2 position, Vector2 direction)
    {
        Position = position;
        Direction = direction;
    }
}
namespace LaserCrush.Entity.Item
{
    public sealed class InstalledItem : MonoBehaviour, ICollisionable
    {
        #region Variable
        [SerializeField] private Transform[] m_EjectionPortsTransform;
        [SerializeField] private LineRenderer[] m_LineRenderers;
        [SerializeField] private GameObject[] m_SubRotationImage;
        [SerializeField] private TextMeshProUGUI m_CountText;

        private Animator m_Animator;
        private CircleCollider2D m_CircleCollider2D;
        private Action<bool> m_OnMouseItemAction;
        private readonly List<LaserInfo> m_EjectionPorts = new List<LaserInfo>();

        private Vector2 m_TextInitPos;
        private Color m_TextInitColor;
        private Color m_TextEndColor;

        private const string m_FixedNoticeAnimationKey = "FixedNotice";
        private const string m_DestroyAnimationKey = "Destroy";

        private const int m_MaxUsingCount = 3;
        private const float m_ChargingTime = 0.5f;
        private const float m_SubLineLength = 5;

        private int m_UsingCount = 0;
        private float m_ChargingWait;

        private bool m_IsActivate;
        private bool m_IsAdjustMode;
        #endregion

        #region Property
        public bool IsAdjustMode
        {
            get => m_IsAdjustMode;
            set
            {
                m_IsAdjustMode = value;

                for (int i = 0; i < m_SubRotationImage.Length; i++) 
                    m_SubRotationImage[i].SetActive(value);

                if (m_IsAdjustMode) PaintAdjustLine();
                else PaintNormalLine();
            }
        }
        public int RowNumber { get; private set; }
        public int ColNumber { get; private set; }
        public bool IsFixedDirection { get; private set; }
        #endregion

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_CircleCollider2D = GetComponent<CircleCollider2D>();
            m_CircleCollider2D.enabled = false;

            m_TextInitColor = m_CountText.color;
            m_TextEndColor = m_CountText.color;
            m_TextEndColor.a = 0;

            IsAdjustMode = true;
        }

        /// <summary>
        /// 해야할 역할
        /// 1. m_EjectionPorts 위치와 방향 초기화
        /// 2. 사용횟수 초기화
        /// </summary>
        public void Init(int rowNumber, int colNumber, Action<bool> onMouseItemAction)
        {
            RowNumber = rowNumber;
            ColNumber = colNumber;
            foreach (Transform tr in m_EjectionPortsTransform)
                m_EjectionPorts.Add(new LaserInfo(position: tr.position, direction: tr.up));

            m_TextInitPos = (Vector2)m_CountText.rectTransform.position + new Vector2(0, 7.5f);
            m_CircleCollider2D.enabled = true;
            m_UsingCount = m_MaxUsingCount;
            IsAdjustMode = false;
            IsFixedDirection = false;
            m_IsActivate = false;
            m_OnMouseItemAction = onMouseItemAction;
        }

        public void FixDirection()
        {
            if (IsFixedDirection) return;
            IsFixedDirection = true;

            for (int i = 0; i < m_EjectionPorts.Count; i++)
            {
                m_EjectionPorts[i] = new LaserInfo(
                    position: m_EjectionPortsTransform[i].position,
                    direction: m_EjectionPortsTransform[i].up
                    );
            }
            m_OnMouseItemAction?.Invoke(false);
        }

        public bool Waiting()
        {
            m_ChargingWait += Time.deltaTime;
            if (m_ChargingWait >= m_ChargingTime)
                m_IsActivate = true;

            return m_IsActivate;
        }

        public List<LaserInfo> Hitted(RaycastHit2D hit, Vector2 parentDirVector, Laser laser)
        {
            if (m_IsActivate) return new List<LaserInfo>();

            GameManager.s_ValidHit++;
            laser.ChangeLaserState(ELaserStateType.Wait);
            m_IsActivate = true;
            m_UsingCount--;
            return m_EjectionPorts;
        }

        public bool IsOverloaded()
        {
            m_IsActivate = false;
            m_ChargingWait = 0;

            return m_UsingCount == 0;
        }

        public bool IsGetDamageable()
            => false;


        public bool GetDamage(int damage)
            => false;


        public EEntityType GetEEntityType()
            => EEntityType.Prisim;


        private void PaintNormalLine()
        {
            for (int i = 0; i < m_LineRenderers.Length; i++)
                PaintLine(i, m_SubLineLength);
        }

        public void PaintAdjustLine()
        {
            for (int i = 0; i < m_LineRenderers.Length; i++)
                PaintLine(i, Mathf.Infinity);
        }

        private void PaintLine(int i, float length)
        {
            Vector2 linePos = m_LineRenderers[i].transform.position;
            Vector2 lineDir = m_LineRenderers[i].transform.up;

            RaycastHit2D hit = Physics2D.Raycast(linePos, lineDir, length, RayManager.s_LaserHitableLayer);
            Vector2 secondPos = hit.collider is null ? linePos + lineDir * length : hit.point;

            m_LineRenderers[i].positionCount = 2;
            m_LineRenderers[i].SetPosition(0, linePos);
            m_LineRenderers[i].SetPosition(1, secondPos);
        }

        public void SetDirection(Vector2 pos)
        {
            Vector2 direction = (pos - (Vector2)transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(transform.forward, direction.DiscreteDirection(5));
            PaintAdjustLine();
        }

        public void PlayFixNoticeAnimation()
            => m_Animator.SetTrigger(m_FixedNoticeAnimationKey);

        public void PlayDestroyAnimation()
        {
            m_CircleCollider2D.enabled = false;

            for(int i = 0; i < m_SubRotationImage.Length;i++)
                m_SubRotationImage[i].SetActive(false);

            for (int i = 0; i < m_EjectionPortsTransform.Length; i++)
                m_EjectionPortsTransform[i].gameObject.SetActive(false);

            m_Animator.SetTrigger(m_DestroyAnimationKey);
        }

        public void PlayUsingCountDisplay()
        {
            StartCoroutine(UsingCountCoroutine(2));
        }

        private IEnumerator UsingCountCoroutine(float time)
        {
            if (m_UsingCount == 3) m_CountText.fontSize = 2.25f;
            if (m_UsingCount == 2) m_CountText.fontSize = 2.75f;
            else m_CountText.fontSize = 3.25f;

            m_CountText.text = m_UsingCount.ToString();
            m_CountText.gameObject.SetActive(true);
            m_CountText.rectTransform.SetPositionAndRotation(m_TextInitPos, Quaternion.LookRotation(Vector3.forward, Vector3.up));
            m_CountText.color = m_TextInitColor;

            Vector2 endPos = m_TextInitPos + new Vector2(0, 7.5f);

            float elapsedTime = 0;
            float t;
            while (elapsedTime <= time)
            {
                elapsedTime += Time.deltaTime;
                t = elapsedTime / time;

                m_CountText.color = Color.Lerp(m_TextInitColor, m_TextEndColor, t);
                m_CountText.rectTransform.position = Vector2.Lerp(m_TextInitPos, endPos, t);

                yield return null;
            }

            m_CountText.gameObject.SetActive(false);
        }

        public void DestroySelf()
            => Destroy(gameObject);

        private void OnDestroy()
            => m_OnMouseItemAction = null;
    }
}