using LaserCrush.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Floor : MonoBehaviour, ICollisionable
{
    #region Variable
    [SerializeField] private double weight = 1.5;
    #endregion
    public List<LaserInfo> Hitted(RaycastHit2D hit, Vector2 parentDirVector, Laser laser)
    {
        //Energy.UseEnergy(int.MaxValue);
        Energy.CollideWithFloor();//���⼭ ������ �Ŵ����� ������ ������ �ٴ� ������ �˾ƾ��Ѵ�.
        laser.ChangeLaserState(ELaserStateType.Hitting);
        return new List<LaserInfo>();
    }

    //������ �� ������ �ϰ� ������ true�� �ٲ��ָ��
    //weight�� ����ġ -> ������ ���������� ��踦 ���� ����
    public bool IsGetDamageable()
    {
        return true;
    }

    //�� ��� ������ �ϰ� ������ �ּ� Ǯ�� true��ȯ
    public bool GetDamage(int damage)
    {
        Energy.UseEnergy((int)(damage * weight));
        return true;
    }

    public bool Waiting()
    {
        return true;
    }

    public EEntityType GetEEntityType()
    {
        return EEntityType.Floor;
    }


}
