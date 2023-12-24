using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum CollisionType
{
    Wall,
    NormalBlock,
    ReflectBlock,
    Floor,
    Item,
    Launcher
}

public class Lazer : MonoBehaviour
{
    /// <summary>
    /// m_StartPoint : ������ ������ -> �Ҹ�� �������� �̵���
    /// m_EndPoint : ������ ���� -> �߻� �� ������ �̵���
    /// </summary>
    #region Property
    private Vector2 m_StartPoint;
    private Vector2 m_EndPoint;
    private Vector2 m_DirectionVector;
    private float m_EraseVelocity;
    private float m_ShootingVelocity;
    private List<Lazer> m_ChildLazers = new List<Lazer>();
    private bool m_IsComplite;
    #endregion

    public void GenerateLazer(Vector2 direction)
    {
        //������ ����
    }

    public bool HasChild()
    {
        if(m_ChildLazers.Count == 0)
        {
            return false;
        }
        return true;
    }

    public List<Lazer> GetChildLazer()
    {
        return m_ChildLazers;
    }

    /// <summary>
    /// ���� �ӵ���ŭ startPoint�� endPoint�������� �̵�
    /// </summary>
    public void Erase()
    {
        if(Vector2.Distance(m_StartPoint, m_EndPoint) <= m_EraseVelocity)
        {
            //����
            m_StartPoint = m_EndPoint;
            return;
        }
        m_StartPoint += (m_DirectionVector * m_EraseVelocity);
    }
     
    public void Shoot()
    {
        List<ICollisionable> obj;
        obj = new List<ICollisionable>();
        for (int i = 0; i < obj.Count; i++) 
        {
            //�̵��Ÿ��ȿ� ������ 
            if (Vector2.Distance(obj[i].GetPosition(), m_EndPoint) <= m_ShootingVelocity)
            {

            }
        }
        m_EndPoint += (m_DirectionVector * m_ShootingVelocity);
    }

    //�̵��Ÿ��ȿ� ��ü�� ���� ���
    public bool Collision(Vector2 obj)
    {
        if (Vector2.Distance(obj, m_EndPoint) <= float.Epsilon)
        {
            return true;
        }
        return false;
    }

    public bool IsComplite()
    {
        return m_IsComplite;
    }

    public void Init(Vector2 start, Vector2 end, Vector2 dir)
    {
        m_StartPoint = start;
        m_EndPoint = end;
        m_DirectionVector = dir;
    }
}

