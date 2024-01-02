using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaserCrush.Entity
{
    
    
    /// <summary>
    /// ���� �ν� ȹ���� ���, �ʵ忡 ������ ���¿� ���濡 �ִ� ����
    /// ��ġ ���� ���� �� ��ġ�Ǳ� ���� ������ ��ġ�Ǹ� �ش� �������� ȣ���ؼ� �ν��Ͻÿ���Ʈ ���ش�.
    /// Item != Prism
    /// </summary>
    public class Item : MonoBehaviour
    {
        #region Property
        protected EItemState m_State;
        protected EItemType m_Type;
        protected Vector2 m_Posion { get; }
        #endregion

    }

}