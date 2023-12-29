using Laser.Entity;
using Laser.Manager;
using System.Collections.Generic;
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
        Debug.Log("���� �浹 �� �ڽĻ���");
        Debug.Log("���� �浹 �� ������ ���");
        Energy.CollidWithWall();
        Vector2 dir = (hit.normal + parentDirVector + hit.normal).normalized;
        return new List<Vector2>() { dir };
    }
}
