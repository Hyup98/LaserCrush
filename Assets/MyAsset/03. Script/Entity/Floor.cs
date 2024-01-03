using LaserCrush.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, ICollisionable
{
    #region Variable
    private EEntityType m_EntityType;
    #endregion
    public void Awake()
    {
        m_EntityType = EEntityType.Floor;
        Debug.Log("�ٴ� �ʱ�ȭ");
    }


    public List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
    {
        Debug.Log("�ٴڰ� �浹 ������ ����");
        Energy.UseEnergy(int.MaxValue);//������ ��� ����
        return new List<Vector2>();
    }

    public bool IsGetDamageable()
    {
        return false;
    }

    public bool GetDamage(int  damage)
    {
        return false;
    }
}