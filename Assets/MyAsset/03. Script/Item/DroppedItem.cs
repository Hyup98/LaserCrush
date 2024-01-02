using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Entity
{
    public class DroppedItem : Item
    {
        #region Property
        private AcquiredItem m_AcquiredItem; // �̰� ��� �ɵ� �����ڷ� �ٷ� �����ؼ� �ѱ���� ��
        #endregion

        public AcquiredItem GetItem()
        {
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
            return m_AcquiredItem; // ���⼭ �ٷ� ���� ������ �ν��Ͻ� �ѱ���
        }

        private void GetAnimationEnergy()
        {

        }

        private void GetAnimationPrism()
        {

        }

    }
}
