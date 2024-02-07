using System.Collections.Generic;
using UnityEngine;
using LaserCrush.Entity.Interface;

namespace LaserCrush.Entity
{
    public sealed class Floor : MonoBehaviour, ICollisionable
    {
        [SerializeField] private float weight = 1.5f;

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
            => true;

        //�� ��� ������ �ϰ� ������ �ּ� Ǯ�� true��ȯ
        public bool GetDamage(int damage)
        {
            Energy.UseEnergy((int)(damage * weight));
            return true;
        }

        public bool Waiting()
            => true;
        
        public EEntityType GetEEntityType()
            => EEntityType.Floor;
    }
}
