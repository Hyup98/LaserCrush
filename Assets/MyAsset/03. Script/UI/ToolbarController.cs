using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LaserCrush.Entity;
using LaserCrush.Manager;

namespace LaserCrush.UI
{
    public class ToolbarController : MonoBehaviour
    {
        [SerializeField] private Transform m_BatchedItemTransform;
        [SerializeField] private RectTransform m_ContentTransform;
        [SerializeField] private List<AcquiredItem> m_Items;

        private ItemManager m_ItemManager;
        private AcquiredItem m_CurrentItem;
        private RectTransform m_CurrentItemTransform;
        private Vector2 m_InitPos;

        private void Awake()
        {
            AcquireItem();
        }

        public void Init(ItemManager itemManager)
        {
            m_ItemManager = itemManager;
        }

        private void AcquireItem()
        {
            foreach (AcquiredItem item in m_Items)
            {
                item.PointerDownAction += OnPointerDown;
                item.PointerUpAction += OnPointerUp;
                item.DragAction += OnDrag;
            }
        }

        private void OnPointerDown(AcquiredItem clickedItem)
        {
            m_CurrentItem = clickedItem;
            m_CurrentItemTransform = clickedItem.GetComponent<RectTransform>();
            m_CurrentItem.GetComponent<Image>().maskable = false;

            m_InitPos = m_CurrentItemTransform.anchoredPosition;
        }

        private void OnPointerUp(Vector2 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (!CanBatch(ray.origin))
            {
                m_CurrentItem.GetComponent<Image>().maskable = true;
                m_CurrentItemTransform.anchoredPosition = m_InitPos;
            }
            else
            {
                GameObject obj = Instantiate(m_CurrentItem.ItemObject);
                obj.transform.SetParent(m_BatchedItemTransform);
                obj.transform.position = ray.origin;

                if (!obj.TryGetComponent(out Prism prism)) Debug.LogError("Prism is null");
                m_ItemManager.AddPrism(prism);

                Destroy(m_CurrentItem.gameObject);
            }
        }

        private void OnDrag(Vector2 delta)
        {
            m_CurrentItemTransform.anchoredPosition += delta;
        }

        private bool CanBatch(Vector2 pos)
        {
            //Raycast �� �ȵɱ�?
            if (Mathf.Abs(pos.x) <= 51 && Mathf.Abs(pos.y) <= 67) return true;
            return false;
        }
    }
}
