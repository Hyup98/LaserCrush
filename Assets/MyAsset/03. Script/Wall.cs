using Laser.Entity;
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
}
