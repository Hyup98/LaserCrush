using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Entity
{
    public enum ItemType
    {
        Energy,
        Prism_1,
        Prism_2,
        Prism_3,
        Prism_4,
        Prism_5,
    }

    public enum ItemState
    {
        Dropped,
        Installing,
        InMyBag
    }
    /// <summary>
    /// ���� �ν� ȹ���� ���, �ʵ忡 ������ ���¿� ���濡 �ִ� ����
    /// ��ġ ���� ���� �� ��ġ�Ǳ� ���� ������ ��ġ�Ǹ� �ش� �������� ȣ���ؼ� �ν��Ͻÿ���Ʈ ���ش�.
    /// Item != Prism
    /// </summary>
    public class Item : MonoBehaviour
    {
        #region Property
        private Vector2 m_Posion { get; }
        private ItemType m_Type;
        private ItemState m_State;
        #endregion


        public void GetItem()
        {
            if(m_State == ItemState.Dropped)
            {
                m_State = ItemState.InMyBag;
            }   
        }

        /// <summary>
        /// �ش� ��ġ�� ������ü �ν��Ͻÿ���Ʈ �� �ʱ�ȭ ���ֱ�
        /// </summary>
        public void InstallPrism()
        {

        }
    }

}