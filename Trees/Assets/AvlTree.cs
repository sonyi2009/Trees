using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvlTree : MonoBehaviour
{
    TreeNode root = null;
    private int[] array = new int[9] { 3, 2, 1, 5, 6, 10, 9, 7, 8 };
    public static AvlTree instance;
    public void Start()
    {
        instance = this;
        //TreeManager.instance.ShowTree(InitAvlTree(array));
    }
    public void JudgeNode(TreeNode node)
    {
        var preUnBalanceNode = node.GetPreUnBalanceNode();
        if (preUnBalanceNode != null)
        {
            if (preUnBalanceNode.ContainInLeftNode(node) && preUnBalanceNode.leftNode.ContainInLeftNode(node))
            {
                //Debug.LogError("R旋转");
                RightRotate(preUnBalanceNode);
            }
            else if (!preUnBalanceNode.ContainInLeftNode(node) && !preUnBalanceNode.rightNode.ContainInLeftNode(node))
            {
                //Debug.LogError("L旋转");
                LeftRotate(preUnBalanceNode);
            }
            else if (preUnBalanceNode.ContainInLeftNode(node) && !preUnBalanceNode.leftNode.ContainInLeftNode(node))
            {
                //Debug.LogError("LR旋转");
                LeftRotate(preUnBalanceNode.leftNode);
                RightRotate(preUnBalanceNode);
            }
            else
            {
                //Debug.LogError("RL旋转");
                RightRotate(preUnBalanceNode.rightNode);
                LeftRotate(preUnBalanceNode);
            }
        }
    }
    public void AddNode(int x)
    {
        var node = root;
        TreeNode parentNode = null;
        while (node!=null)
        {
            parentNode = node;
            node = (x < node.value)?node.leftNode:node.rightNode;
        }
        node = new TreeNode(x < parentNode.value, x, parentNode,-1);
        JudgeNode(node);
    }
   
    public void LeftRotate(TreeNode preRoot)
    {
        var newRoot = preRoot.rightNode;

        var parent = preRoot.parent;

        newRoot.ChangeParent(parent, preRoot.isLeft);

        preRoot.rightNode = newRoot.leftNode;
        if (newRoot.leftNode != null)
        {
            newRoot.leftNode.ChangeParent(preRoot, false);
        }
        preRoot.ChangeParent(newRoot,true);
    }
    public void RightRotate(TreeNode preRoot)
    {
        var newRoot = preRoot.leftNode;

        var parent = preRoot.parent;

        newRoot.ChangeParent(parent, preRoot.isLeft);

        preRoot.leftNode = newRoot.rightNode;

        if (newRoot.rightNode != null)
        {
            newRoot.rightNode.ChangeParent(preRoot, true);
        }
        preRoot.ChangeParent(newRoot, false);
    }
    public List<TreeNode> InitAvlTree(int[] array)
    {
        root = new TreeNode(true, array[0], null,-1);
        for (int i = 1; i < array.Length; i++)
        {
            AddNode(array[i]);
            while (root.parent != null) root = root.parent;
        }
        List<TreeNode> trees = new List<TreeNode>();
        Queue queue = new Queue();
        
        queue.Enqueue(root);
        while (queue.Count!=0)
        {
            var node = (TreeNode)queue.Dequeue();
            trees.Add(node);
            if (node.leftNode != null) queue.Enqueue(node.leftNode);
            if (node.rightNode != null) queue.Enqueue(node.rightNode);
        }
        return trees;
    }
}
