using System.Collections.Generic;
using UnityEngine;

public class BinaryStack : MonoBehaviour
{
    //private int[] array = new int[9] { 1, 3, 2, 6, 5, 7, 8, 9, 10 }; //下面的树排序后的结果
    private int[] array = new int[9] { 3, 2, 1, 5, 6, 10, 9, 7, 8 };
    private int[] tree = new int[100];//假设地图大小最大为100
    private int count = 0;
    public static BinaryStack instance;
    public void Start()
    {
        instance = this;
        //TreeManager.instance.ShowTree(InitBinaryStack(array));
    }
    public void AddNode(int x)
    {
        tree[count++] = x;
        UpAdjust();
    }
    private void UpAdjust()
    {
        int childIndex = count - 1;
        int parentIndex = (childIndex - 1) / 2;
        int temp = tree[childIndex];
        while (childIndex > 0 && tree[parentIndex] < temp)
        {
            tree[childIndex] = tree[parentIndex];
            childIndex = parentIndex;
            parentIndex = (childIndex - 1) / 2;
        }
        tree[childIndex] = temp; 
    }
    public int GetNode()
    {
        int value = tree[0];
        tree[0] = tree[count--];
        DownAdjust();
        return value;
    }
    private void DownAdjust()
    {
        int parentIndex = 0;
        int temp = tree[parentIndex];
        int childIndex = parentIndex * 2 + 1;
        while (childIndex < count)
        {
            if (childIndex + 1 < count && tree[childIndex + 1] > tree[childIndex] && tree[childIndex + 1] > tree[parentIndex])
            {
                childIndex++;
            }
            else if(tree[parentIndex]>tree[childIndex])
            {
                return;
            }
            tree[parentIndex] = tree[childIndex];
            parentIndex = childIndex;
            childIndex = parentIndex * 2 + 1;
        }
        tree[parentIndex] = temp;
    }
    public List<TreeNode> InitBinaryStack(int[] array)
    {
        count = 0;
        foreach (var key in array)
        {
            AddNode(key);
        }
        List<TreeNode> trees = new List<TreeNode>();
        TreeNode root = new TreeNode(true, tree[0], null,-1);
        trees.Add(root);
        int current = 0;
        while (current++ < count - 1)
        {
            int parentIndex = (current - 1) / 2;
            trees.Add(new TreeNode(current % 2 == 1, tree[current], trees[parentIndex],-1));
        }
        return trees;
    }
}
