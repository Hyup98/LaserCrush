using LaserCrush.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������� �浹 �� Ȱ��ȭ �Ǿ� ������ port�� vector�� �޾�
/// �ش� �������� �������� �߰� ����
/// </summary>

public class Prism : ICollisionable
{
    /// <summary>
    /// m_EjectionPorts : �� ���ⱸ�� ���⺤��
    /// </summary>
    #region Property
    private List<Vector2> m_EjectionPorts = new List<Vector2>();

    private const int m_MaxUsingCount = 3;
    private const int m_ChargingTime = 10;
    
    private int m_UsingCount = 0;
    private int m_ChargingWait;

    private bool m_IsActivate = false;
    #endregion

    /// <summary>
    /// ȭ�鿡�� ���� �Է��� �޾� ��ġ�Ҷ� ȣ���� �Լ�
    /// �����ۿ��� �������� ���� ������ �ð�ȭ ���ش�
    /// ���� �ʿ�
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

    public List<Vector2> GetEjectionPorts()
    {
        return m_EjectionPorts;
    }

    public void Charging()
    {
        //�ð����
        m_ChargingWait++;
        if(m_ChargingWait >= m_ChargingTime)
        {
            m_IsActivate = true;
        }
    }

    public override void GetDamage(int damage)
    {
        return;
    }

    public override bool IsAttackable()
    {
        return false;
    }

    /// <summary>
    /// �ش� �Լ��� ȣ��Ǹ� �������� Ȱ��ȭ�Ǹ� ��� Ƚ����1ȸ ����
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="parentDirVector"></param>
    /// <returns></returns>
    public override List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
    {
        m_IsActivate = true;
        m_UsingCount--;
        List<Vector2> answer = new List<Vector2>();
        return answer;
    }

    public bool IsOverloaded()
    {
        if(m_UsingCount == 0)
        {
            return true;
        }
        return false; 

    }
}