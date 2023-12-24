using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������� �浹 �� Ȱ��ȭ �Ǿ� ������ port�� vector�� �޾�
/// �ش� �������� �������� �߰� ����
/// </summary>

public class Prism : Item
{
    /// <summary>
    /// m_EjectionPorts : �� ���ⱸ�� ���⺤��
    /// </summary>
    #region Property
    private const int m_MaxUsingCount = 3;
    private const int m_CHARGINTIME = 10;
    private List<Vector3> m_EjectionPorts = new List<Vector3>();
    private int m_UsingCount = 0;
    private bool m_IsActivate = false;
    private int m_ChargingWait;
    #endregion

    /// <summary>
    /// ȭ�鿡�� ���� �Է��� �޾� ��ġ�Ҷ� ȣ���� �Լ�
    /// �����ۿ��� �������� ���� ������ �ð�ȭ ���ش�
    /// </summary>
    void SetEjectionPorts()
    {
        //�Է��� �޾� rot�� �����.
        for(int i = 0; i < m_EjectionPorts.Count; i++) 
        {
            m_EjectionPorts[i] = Quaternion.Euler(0f, 0f, 0f) * m_EjectionPorts[i];
        }
    }

    /// <summary>
    /// Ȱ��ȭ �Ǿ����� ������ Charging�� ���������� ���ο� ������ ����
    /// Ȱ��ȭ �� ���¸� ������ ���� x �̹� ������ ����
    /// </summary>
    /// <returns></returns>
    public bool IsActivate()
    {
        return m_IsActivate;
    }

    public List<Vector3> GetEjectionPorts()
    {
        return m_EjectionPorts;
    }

    public void Charging()
    {
        //�ð����
        m_ChargingWait++;
        if(m_ChargingWait >= m_CHARGINTIME)
        {
            m_IsActivate=true;
        }
    }

     
}