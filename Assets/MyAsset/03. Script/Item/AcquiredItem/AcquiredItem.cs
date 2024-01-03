using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace LaserCrush.Entity
{
    public class AcquiredItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Variable
        [SerializeField] private GameObject m_ItemObject;

        private EItemState m_EItemType;
        private EItemState m_EState;

        private InstalledItem m_PrismItem;
        #endregion

        #region Delegate
        private UnityAction<AcquiredItem> m_PointerDownAction;
        private UnityAction<Vector2> m_PointerUpAction;
        private UnityAction<Vector2> m_DragAction;
        #endregion

        #region Property
        public event UnityAction<AcquiredItem> PointerDownAction
        {
            add => m_PointerDownAction += value;
            remove => m_PointerDownAction -= value;
        }

        public event UnityAction<Vector2> PointerUpAction
        {
            add => m_PointerUpAction += value;
            remove => m_PointerUpAction -= value;
        }

        public event UnityAction<Vector2> DragAction
        {
            add => m_DragAction += value;
            remove => m_DragAction -= value;
        }

        public GameObject ItemObject { get => m_ItemObject; }
        #endregion

        public void OnPointerDown(PointerEventData eventData)
            => m_PointerDownAction?.Invoke(this);

        public void OnPointerUp(PointerEventData eventData)
            => m_PointerUpAction?.Invoke(eventData.position);

        public void OnDrag(PointerEventData eventData)
            => m_DragAction?.Invoke(eventData.delta);



        /// <summary>
        /// �̸��� �ٲ� ������ ��������Ʈ�� ����ؼ� ItemManager�� ������ �迭�� �߰��ؾ���
        /// </summary>
        public void InstalledPrism()
        {
            /* Todo
             * AddPrism() -> ��������Ʈ�� �߰�
             */
        }

        private void OnDestroy()
        {
            m_PointerDownAction = null;
            m_PointerUpAction = null;
            m_DragAction = null;
        }
    }
}