using Laser.Manager;
using Laser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laser.Object;
namespace Laser.Manager
{
    public class BlockManager : MonoBehaviour
    {
        #region Property
        private static List<Block> m_Blocks = new List<Block>();
        #endregion

        private void Awake()
        {
        }

        public static void GenerateBlock(int num)
        {
            //TODO//
            /*
             * num���� ��ŭ �� ����
             * ���� �� ����Ʈ�� �߰�
             */
        }

        public static int GetBlockCount()
        {
            return m_Blocks.Count;
        }
    }
}
