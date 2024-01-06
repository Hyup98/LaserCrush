using LaserCrush.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// �������� ������� �浹 �� Ȱ��ȭ �Ǿ� ������ port�� vector�� �޾�
/// �ش� �������� �������� �߰� ����
/// </summary>

public struct LaserInfo
{
    public Vector2 Posion;
    public Vector2 Direction;
}


public class InstalledItem : MonoBehaviour, ICollisionable
{
    #region Variable
    /// <summary>
    /// m_EjectionPorts : �� ���ⱸ�� ���⺤��
    /// </summary>
    protected List<Vector2> m_EjectionPorts = new List<Vector2>();

    private const int m_MaxUsingCount = 3;
    private const int m_ChargingTime = 10;
    
    private int m_UsingCount = 0;
    private int m_ChargingWait;

    protected bool m_IsActivate = false;
    #endregion


    /// <summary>
    /// ȭ�鿡�� ���� �Է��� �޾� ��ġ�Ҷ� ȣ���� �Լ�
    /// �����ۿ��� �������� ���� ������ �ð�ȭ ���ش�
    /// ���� �ʿ�
    /// </summary>
    void RotateEjectionPorts()
    {
    }

    /// <summary>
    /// �̰� �����Լ��� �ڽ� Ŭ�������� �Լ��� ȣ���� �ȵ� ���� �˸� �˷���
    /// </summary>
    public virtual void Init()     {    }

    /// <summary>
    /// Ȱ��ȭ �Ǿ����� ������ Charging�� ���������� ���ο� ������ ����
    /// Ȱ��ȭ �� ���¸� ������ ���� x �̹� ������ ����
    /// </summary>
    /// <returns></returns>
    public bool IsActivate()
    {
        return m_IsActivate;
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

    /// <summary>
    /// �ش� �Լ��� ȣ��Ǹ� �������� Ȱ��ȭ�Ǹ� ��� Ƚ����1ȸ ����
    /// �߸ŷ� ó�� �浹 ƨ��°� �� ó���ؾ��ҵ�
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="parentDirVector"></param>
    /// <returns></returns>
    public List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
    {
        if (m_IsActivate)
        {
            List<Vector2> answer = new List<Vector2>();
            return answer;
        }
        m_IsActivate = true;
        m_UsingCount--;
        return m_EjectionPorts;
    }

    public bool IsOverloaded()
    {
        m_IsActivate = false;
        if (m_UsingCount == 0)
        {
            return true;
        }
        return false; 
    }

    public bool IsGetDamageable()
    {
        return false;
    }

    public bool GetDamage(int damage)
    {
        return false;
    }
}