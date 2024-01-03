using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaserCrush.Entity;
using LaserCrush.UI;

namespace LaserCrush.Manager
{
    [System.Serializable]
    public class ItemManager
    {
        #region Variable
        [SerializeField] private ToolbarController m_ToolbarController;

        private List<DroppedItem> m_DroppedItems;
        private List<AcquiredItem> m_AcquiredItems;
        private List<InstalledItem> m_Prisms = new List<InstalledItem>();

        private List<InstalledItem> m_PrismRemoveBuffer = new List<InstalledItem>();
        #endregion

        public void Init()
        {
            m_DroppedItems = new List<DroppedItem>();
            m_AcquiredItems = new List<AcquiredItem>();
            m_Prisms = new List<InstalledItem>();
            m_PrismRemoveBuffer = new List<InstalledItem>();

            m_ToolbarController.m_AddPrismAction += AddPrism;
        }

        public void GetDroppedItems()
        {
            for(int i = 0; i < m_DroppedItems.Count; i++)
            {
                AcquiredItem acquiredItem = m_DroppedItems[i].GetItem();
                m_AcquiredItems.Add(acquiredItem);
                m_ToolbarController.AcquireItem(acquiredItem);

            }
            //m_DroppedItems.Clear();
        }

        public void CheckDestroyPrisms()
        {
            for(int i = 0; i < m_Prisms.Count; i++)
            {
                if (m_Prisms[i].IsOverloaded())
                {
                    m_PrismRemoveBuffer.Add(m_Prisms[i]);
                }
            }
            RemoveBufferFlush();
        }

        /*TODO
         * �ش� �Լ��� ��������Ʈ�� ������ ������ �������ٶ� Prism�迭�� �־��־�� �Ѵ�.
         */
        public void AddPrism(InstalledItem prism, AcquiredItem acquiredItem)
        {
            m_AcquiredItems.Remove(acquiredItem);
            m_Prisms.Add(prism);
        }

        public void AddDroppedItem(DroppedItem item)
        {
            m_DroppedItems.Add(item);
        }

        private void RemoveBufferFlush()
        {
            for (int i = 0; i < m_PrismRemoveBuffer.Count; i++)
            {
                m_Prisms.Remove(m_PrismRemoveBuffer[i]);
            }
            m_PrismRemoveBuffer.Clear();
        }
    }
}
