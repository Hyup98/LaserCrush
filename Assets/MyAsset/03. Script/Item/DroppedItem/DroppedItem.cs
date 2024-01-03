using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Entity
{
    public class DroppedItem : MonoBehaviour
    {
        #region Variable
        [SerializeField] private AcquiredItem m_AcquiredItem;
        
        private EItemState m_State;
        private EItemType m_Type;
        #endregion

        /// <summary>
        /// AcquiredItem ������Ʈ�� ���� �� ��ȯ
        /// </summary>
        /// <returns></returns>
        public AcquiredItem GetItem()
        {
            AcquiredItem acquiredItem = Instantiate(m_AcquiredItem);
            /*TODO
             *�ش� �ν��Ͻ��� �ı��ϰ� AcquiredItem ��ü�� �����Ѵ�
             *�� �������� �ִϸ��̼� ȿ�� ����ϸ� �ȴ�.
             */
            switch (m_Type) //�ִϸ��̼ǿ� �б�
            {
                case EItemType.Energy:
                    GetAnimationEnergy();
                    break;
                default:
                    GetAnimationPrism();
                    break;
            }
            return acquiredItem; // ���⼭ �ٷ� ���� ������ �ν��Ͻ� �ѱ���
        }

        private void GetAnimationEnergy()
        {

        }

        private void GetAnimationPrism()
        {

        }
    }
}
