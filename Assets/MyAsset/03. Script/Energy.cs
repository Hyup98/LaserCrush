using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    #region Property
    private int m_Energy;
    private float m_DecreaseRate;
    private static Vector2 m_Postion;
    #endregion

    /// <summary>
    /// ������ �������� 1�� ����Ѵ�.
    /// �������� �Ҹ��� �� ���� ��� �� ��ȯ
    /// </summary>
    /// <param name="energy"></param>
    /// <returns></returns>
    public bool UseEnergy()
    {
        if(m_Energy <= 0)
        {
            return false;
        }
        m_Energy -= 1;
        return true;
    }

    public int GetEnergy()
    {
        return m_Energy;
    }

    public static Vector2 GetPosion()
    {
        return m_Postion;
    }
}
