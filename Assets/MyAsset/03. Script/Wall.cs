using Laser.Entity;
using Laser.Manager;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;
using Debug = UnityEngine.Debug;

public class Wall : ICollisionable
{
    public void Awake()
    {
        m_Type = EntityType.Wall;
        Debug.Log("�� �ʱ�ȭ");
    }
    public override EntityType GetEntityType()
    {
        return EntityType.Wall;
    }


    public override void GetDamage(int damage)
    {
        return;
    }

    public override bool IsAttackable()
    {
        return false;
    }

    public override void Hitted(RaycastHit2D hit, Vector2 parentDirVector)
    {
        Debug.Log("���� �浹 �� �ڽĻ���");
        Vector2 dir = Vector2.Reflect(parentDirVector, hit.normal);
        Vector2 tem = new Vector2(-1, 0);
        Debug.Log("�ݻ纤�� : " + dir + "�θ� ���� : " + parentDirVector);
        /*
        Laser.Entity.Laser laser = Instantiate(gameObject).GetComponent<Laser.Entity.Laser>();
        laser.Init(hit.transform.position, tem);
        LaserManager.AddLaser(laser);
        */
    }
}
