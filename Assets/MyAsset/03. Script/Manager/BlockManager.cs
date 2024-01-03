using System.Collections.Generic;
using UnityEngine;
using System;
using LaserCrush.Entity;
using TMPro.EditorUtilities;

namespace LaserCrush.Manager
{
    [Serializable]
    public class BlockManager
    {
        #region Variable
        [SerializeField] private Transform m_BlockTransform;
        [SerializeField] private GameObject m_BlockObject;
        [SerializeField] private Vector2 m_InitPos;
        [SerializeField] private Vector2 m_Offset;

        private Vector3 m_MoveDownVector;

        private ItemManager m_ItemManager;
        private List<Block> m_Blocks;
        private event Func<GameObject, GameObject> m_InstantiateFunc;
        private int m_WidthBlocksCapacity = 6;
        #endregion

        public void Init(Func<GameObject, GameObject> instantiateFunc, ItemManager itemManager)
        {
            m_Blocks = new List<Block>();
            m_InstantiateFunc = instantiateFunc;
            m_ItemManager = itemManager;

            m_MoveDownVector = new Vector3(0, m_Offset.y, 0);
        }

        public void GenerateBlock()
        {
            GameObject obj;
            Block block;
            /* todo
             * ���⼭ GenerateBlockOffset�� ȣ���ؼ� 
             * �콬���� ��ȸ�ϸ鼭 �ش� �����¿� �� �����ϸ� ��
             */
            /*for (int i = 0; i < 4; i++)
            {
                obj = m_InstantiateFunc?.Invoke(m_BlockObject);
                obj.transform.SetParent(m_BlockTransform);

                block = obj.GetComponent<Block>();
                block.transform.position = new Vector3(m_InitPos.x + m_Offset.x * i, m_InitPos.y, 0);
                block.Init(GenerateBlockHP(), GenerateEntityType(), GenerateItemType(), RemoveBlock);
                m_Blocks.Add(block);
            }*/
            HashSet<int> index = GenerateBlockOffset();
            foreach (int i in index)
            {
                obj = m_InstantiateFunc?.Invoke(m_BlockObject);
                obj.transform.SetParent(m_BlockTransform);

                block = obj.GetComponent<Block>();
                block.transform.position = new Vector3(m_InitPos.x + m_Offset.x * i, m_InitPos.y, 0);
                block.Init(GenerateBlockHP(), GenerateEntityType(), GenerateItemType(), RemoveBlock);
                m_Blocks.Add(block);
            }
        }

        private void RemoveBlock(Block block)
        {
            //m_ItemManager.AddDroppedItem(block.DroppedItem);
            m_Blocks.Remove(block);
        }

        public void MoveDownAllBlocks()
        {
            for (int i = 0; i < m_Blocks.Count; i++)
            {
                m_Blocks[i].transform.position += m_MoveDownVector;
            }
        }

        /// <summary>
        /// ������ �ܼ��� ������ Ȯ���� �� ��ġ�� ������ ����
        /// </summary>
        /// <returns></returns>
        private HashSet<int> GenerateBlockOffset()
        {
            int randomSize = UnityEngine.Random.Range(1,7);//1~6���� ����
            HashSet<int> result = new HashSet<int>();

            while(result.Count < randomSize)
            {
                result.Add(UnityEngine.Random.Range(0, m_WidthBlocksCapacity));//0 ~ m_WidthBlocksCapacity ���� ���� ����
            }
            return result;
        }

        /// <summary>
        /// 3������������ ����ġ �ο�
        /// ������ 
        /// </summary>
        /// <returns></returns>
        private int GenerateBlockHP()
        {
            int end = ((GameManager.m_StageNum + 2) / 3) * 10;
            int start = end - (end / 2);
            return UnityEngine.Random.Range(start, end + 1);
        }

        /// <summary>
        /// Ȯ�� ǥ
        /// None = 50
        /// Energy = 30
        /// Prism_1 = 10
        /// Prism_2 = 10
        /// </summary>
        /// <returns></returns>
        private EItemType GenerateItemType()
        {
            //0~99���� ���� ����
            int prob = UnityEngine.Random.Range(0, 100);

            if (prob < 50)
            {
                return EItemType.None;
            }
            else if (prob < 80)
            {
                return EItemType.Energy;
            }
            else if (prob < 90)
            {
                return EItemType.Prism_1;
            }
            else
            {
                return EItemType.Prism_2;
            }
        }

        /// <summary>
        /// Ȯ�� ǥ
        /// �Ϲ� �� = 50
        /// �ݻ� �� = 50
        /// </summary>
        /// <returns></returns>
        private EEntityType GenerateEntityType()
        {
            int prob = UnityEngine.Random.Range(0, 100);

            if (prob < 50)
            {
                return EEntityType.NormalBlock;
            }
            else
            {
                return EEntityType.ReflectBlock;
            }
        }
    }
}
