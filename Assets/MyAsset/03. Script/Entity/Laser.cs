using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Laser.Manager;
using static ICollisionable;
using Unity.Android.Types;
using Unity.Burst.CompilerServices;
using UnityEngine.UIElements;
using UnityEngineInternal;

namespace Laser.Entity
{
    enum LaserStateType // �̸� ���
    {
        Move,
        Hitting
    }

    /// <summary>
    /// ������ �Ŵ����� �� ������Ʈ���� �� ������ ��ü�� ������Ʈ����
    /// �浹���� ������ ��� ���������� ó�� �� �� ��ü�� �뺸�ϴ� ���
    ///     ex) �浹 ������Ʈ Ž�� �� hp, Ÿ�Ե� �о�� �� ���� ó�� �� �� ��ü�� �뺸
    /// </summary>
    public class Laser : MonoBehaviour
    {
        /// <summary>
        /// m_StartPoint : ������ ������ -> �Ҹ�� �������� �̵���
        /// m_EndPoint : ������ ���� -> �߻� �� ������ �̵���
        /// </summary>
        #region Property
        [SerializeField] private GameObject m_LaserObject;

        private Vector2 m_StartPoint;
        private Vector2 m_EndPoint;
        private Vector2 m_DirectionVector;
        private float m_EraseVelocity = 0.2f;
        private float m_ShootingVelocity = 0.1f;
        private List<Laser> m_ChildLazers = new List<Laser>();
        private LaserStateType m_State;
        private int m_Damage;//�����̸� ��� -> �ѹ� �Ҹ��� �������� ������ ����
        private ICollisionable m_Target = null; // �̺κе� ��� �غ�����

        private LineRenderer m_LineRenderer;
        #endregion

        private void Awake()
        {
            m_LineRenderer = GetComponent<LineRenderer>();
            if (m_LineRenderer is null) Debug.LogError("m_LineRenderer is Null");
        }

        public void Init(Vector2 posion, Vector2 dir)
        {
            m_StartPoint = posion;
            m_EndPoint = posion;
            m_DirectionVector = dir.normalized;

            m_LineRenderer.positionCount = 2;
            m_LineRenderer.SetPosition(0, posion);
            m_LineRenderer.SetPosition(1, dir);
        }

        /// <summary>
        /// ������ �� ����
        /// [������] : �浹 �� ���� ������Ʈ���� ���⺤�� �������� �̵�
        /// [�浹] : ���� �浹���� �ڽ� ������ ����, �ֱ⸶�� ������ üũ �� �浹 �� ���
        ///          �������� ����(�б�)�� ������ ���¿��� ���� �浹�� ������ ���� ����ȴ�.
        /// </summary>
        public void ManagedUpdate()
        {
            Debug.Log(m_State);
            switch (m_State) 
            {
                case LaserStateType.Move://������ �Ҹ�x �̵���
                    Move();
                    break;
                case LaserStateType.Hitting:
                    Hiting();
                    break;
                default:
                    Debug.Log("�߸��� ������ �����Դϴ�.");
                    break;
            }
        }

        public void GenerateLazer(Vector2 direction)
        {
            //������ ����
        }

        public bool HasChild()
        {
            if (m_ChildLazers.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<Laser> GetChildLazer()
        {
            return m_ChildLazers;
        }

        /// <summary>
        /// ���� �ӵ���ŭ startPoint�� endPoint�������� �̵�
        /// �������� �������� true ��ȯ
        /// </summary>
        public bool Erase()
        {
            if (Vector2.Distance(m_StartPoint, m_EndPoint) <= m_EraseVelocity)
            {
                //����
                m_StartPoint = m_EndPoint;
                return true;
            }
            m_StartPoint += m_DirectionVector * m_EraseVelocity;
            return false;
        }

        /// <summary>
        /// �������� ������ �����̴� �Լ�
        /// 1.  �浹 Ž��
        /// 1.1 �浹�� ��ü�� �޾ƿ´�
        /// 1.2 ��ü�� �´� �Լ��� ȣ���Ѵ�.
        /// </summary> 
        public void Move()
        {
            if(!Energy.CheckEnergy()) { return; }
            
            RaycastHit2D hit = Physics2D.Raycast(m_StartPoint, m_DirectionVector, Mathf.Infinity, 1 << LayerMask.NameToLayer("Reflectable") | 1 << LayerMask.NameToLayer("Absorbable"));
            float dist = Vector2.Distance(m_EndPoint, hit.point);
            if (hit.collider != null && dist <= m_ShootingVelocity)//�浹 ��
            {
                Debug.Log("Move" + hit.transform.name);
                m_Target = hit.transform.GetComponent<ICollisionable>();

                Vector2 temVec = m_DirectionVector + hit.normal;
                temVec = (hit.normal + temVec).normalized;

                //CreateChildRaser(hit.transform.position, temVec);

                //������ ���X -> �ڽ� ������ ������ ���
                //switch (m_Target.GetEntityType())
                //{
                //    case EntityType.NormalBlock:
                //        CollideNormalBlock();
                //        break;
                //    case EntityType.ReflectBlock:
                //        CollideReflectBlock(hit);
                //        break;
                //    case EntityType.Prisim:
                //        CollidePrisim(hit);
                //        break;
                //    case EntityType.Floor:
                //        CollideFloor();
                //        break;
                //    case EntityType.Wall:
                //        CollideWall(hit);
                //        break;
                //    case EntityType.Launcher:

                //    default:
                //        Debug.Log("�浹 ��ü�� Ÿ���� �ùٸ��� ����"); break;
                //}
                m_State = LaserStateType.Hitting;
            }
            m_EndPoint += m_DirectionVector * m_ShootingVelocity;
            m_LineRenderer.SetPosition(1, m_EndPoint);
        }

        /// <summary>
        /// �۵� ����
        /// 1. �浹 ���� ���� Ÿ���� Ȯ���ϰ� ���� �Ұ����� ��� ������Ʈ ����
        /// 2. ������ ������ ��� ������ �ܷ��� Ȯ�� �� 0�� �ƴϸ� ���� ����
        /// 3. �ش� ���� GetDamage�Լ��� ȣ���� �������� �ְ� Energy��ü�� �������� ���ҽ�Ų��.
        /// </summary>
        public void Hiting()
        {
            if (!m_Target.IsAttackable())
            {
                return;
            }

            if (Energy.CheckEnergy())//�߻��� ������ ��밡�ɿ��� Ȯ��
            {
                m_Target.GetDamage(m_Damage);
            }
        }

        public void Complete()
        {

        }

        public void CollideNormalBlock()
        {
            return;
        }

        public void CollideReflectBlock(RaycastHit2D hit)
        {
            //���ο� �ڽ� ������ ����
            Vector2 temDir = m_DirectionVector + hit.normal;
            temDir = (hit.normal + temDir).normalized;

            CreateChildRaser(hit.transform.position, temDir);
        }

        public void CollidePrisim(RaycastHit2D hit)
        {
            List<Vector2> dir =  hit.collider.GetComponent<Prism>().GetEjectionPorts();
            for(int i = 0; i < dir.Count; ++i) 
            {
                //TODO//
                //dir�� ���⺤�ͷ� �ϰ� ��ġ�� hit �����ͷ� �ϴ� ������ ����
                //������ ���� �� manager�� add�Լ��� ȣ���� �߰��� �־�� �Ѵ�.
            }
        }

        public void CollideWall(RaycastHit2D hit)
        {
            //���ο� �ڽ� ������ ����

            Vector2 temDir = m_DirectionVector + hit.normal;
            temDir = (hit.normal + temDir).normalized;
        }

        public void CollideFloor()
        {
            return;
        }

        public void CollideLauncher(RaycastHit2D hit)
        {
            Vector2 dir = hit.collider.GetComponent<Launcher>().GetDirectionVector();
            //TODO//
            //dir�� ���⺤�ͷ� �ϰ� ��ġ�� hit �����ͷ� �ϴ� ������ ����
            //������ ���� �� manager�� add�Լ��� ȣ���� �߰��� �־�� �Ѵ�.
            CreateChildRaser(hit.transform.position, dir);
        }

        /// <summary>
        /// �ڽ� ������ ����� ��ġ, ���� ���� +
        /// LaserManager�� ������ ����Ʈ�� �߰�
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        private void CreateChildRaser(Vector2 pos, Vector2 rot)
        {
            Debug.Log("CreateChildRaser");
            Laser laser = Instantiate(gameObject).GetComponent<Laser>();
            laser.Init(pos, rot);
            LaserManager.AddLaser(laser);
        }
    }
}
