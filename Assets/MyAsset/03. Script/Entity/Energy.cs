using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Energy : MonoBehaviour
{
    #region Property
    [SerializeField] private TextMeshProUGUI m_Text;

    private static event UnityAction m_TextUpdate;

    private static int m_Energy = 10000;
    private static Vector2 m_Postion;
    #endregion
    private void Awake()
    {
        m_TextUpdate = null;
        m_TextUpdate += () => m_Text.text = m_Energy.ToString();
        m_TextUpdate?.Invoke();
    }

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

    public static int GetEnergy()
    {
        return m_Energy;
    }

    public static Vector2 GetPosion()
    {
        return m_Postion;
    }

    /// <summary>
    /// ��ȯ���� �� ����� �������� ���̴�.
    /// ���Գ�
    /// </summary>
    /// <param name="energy">
    /// ����� ������
    /// </param>
    /// <returns></returns>
    public static int UseEnergy(int energy)
    {
        if(energy <= m_Energy) 
        {
            m_Energy -= energy;
        }
        else
        {
            energy = m_Energy;
            m_Energy = 0;
        }
        m_TextUpdate?.Invoke();
        return energy;
    }
    
    public static bool CheckEnergy()
    {
        return m_Energy > 0;
    }

    public static bool IsAvailable()
    {
        return m_Energy > 0;
    }

    /// <summary>
    /// �ϴ� �ε����� ���� 30�� ����
    /// </summary>
    public static void CollidWithWall()
    {
        m_Energy -= (m_Energy / 3);
        m_TextUpdate?.Invoke();
    }
}
