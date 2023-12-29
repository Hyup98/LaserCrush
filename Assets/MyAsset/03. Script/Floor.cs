using Laser.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : ICollisionable
{
    public void Awake()
    {
        m_Type = EntityType.Floor;
        Debug.Log("�ٴ� �ʱ�ȭ");
    }

    public override void GetDamage(int damage)
    {
        return;
    }

    public override bool IsAttackable()
    {
        return false;
    }

    public override List<Vector2> Hitted(RaycastHit2D hit, Vector2 parentDirVector)
    {
        Debug.Log("�ٴڰ� �浹 ������ ����");
        Energy.UseEnergy(int.MaxValue);//������ ��� ����
        Debug.Log(Energy.GetEnergy());
        List<Vector2> answer = new List<Vector2>();
        return answer;
    }
}
