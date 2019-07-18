using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlackTree : MonoBehaviour
{
    TreeNode root = null;
    private int[] array = new int[9] { 3, 2, 1, 5, 6, 10, 9, 7, 8 };
    public static RedBlackTree instance;
    public void Start()
    {
        instance = this;
        //TreeManager.instance.ShowTree(InitRBTree(array));
    }
    public List<TreeNode> InitRBTree(int[] array)
    {
        root = new TreeNode(true, array[0], null,1);
        for (int i = 1; i < array.Length; i++)
        {
            AddNode(array[i]);
            while (root.parent != null) root = root.parent;
        }
        List<TreeNode> trees = new List<TreeNode>();
        Queue queue = new Queue();

        queue.Enqueue(root);
        while (queue.Count != 0)
        {
            var node = (TreeNode)queue.Dequeue();
            trees.Add(node);
            if (node.leftNode != null) queue.Enqueue(node.leftNode);
            if (node.rightNode != null) queue.Enqueue(node.rightNode);
        }
        return trees;
    }
    public void JudgeNode(TreeNode node)
    {
        var parentNode = node.parent;
        if (parentNode == null || parentNode.color == 1) return;
        else if (node.parent.color == 0)
        {
            var broNode = parentNode.GetBroNode();
            if (broNode == null || (broNode != null && node.color == 0 && broNode.color == 1))
            {
                if (parentNode.isLeft && node.isLeft)
                {
                    //Debug.LogError("R");
                    RightRotate(parentNode.parent);
                }
                else if (!parentNode.isLeft && !node.isLeft)
                {
                    //Debug.LogError("L");
                    LeftRotate(parentNode.parent);
                }
                else if (parentNode.isLeft && !node.isLeft)
                {
                    //Debug.LogError("LR");
                    LeftRotate(parentNode);
                    RightRotate(parentNode.parent.parent);
                }
                else
                {
                    //Debug.LogError("RL");
                    RightRotate(parentNode);
                    LeftRotate(parentNode.parent.parent);
                }
            }
            else if (broNode.color == 0)
            {
                broNode.color = 1;
                parentNode.color = 1;
                Debug.LogError("bbbb");

                if (parentNode.parent.parent != null)
                {
                    Debug.LogError("aaa");
                    parentNode.parent.color = 0;
                }
                node = parentNode;
                JudgeNode(node);
            }
        }
    }
    public void AddNode(int x)
    {
        var node = root;
        TreeNode parentNode = null;
        while (node != null)
        {
            parentNode = node;
            node = (x < node.value) ? node.leftNode : node.rightNode;
        }
        node = new TreeNode(x < parentNode.value, x, parentNode);
        JudgeNode(node);
    }
    public void LeftRotate(TreeNode preRoot)
    {
        //取得是最高节点
        var newRoot = preRoot.rightNode;

        newRoot.ChangeParent(preRoot.parent, preRoot.isLeft);

        preRoot.rightNode = newRoot.leftNode;
        if (newRoot.leftNode != null)
        {
            newRoot.leftNode.ChangeParent(preRoot, false);
        }
        preRoot.ChangeParent(newRoot, true);

        int temp = newRoot.color;
        newRoot.color = preRoot.color;
        preRoot.color = temp;
    }
    public void RightRotate(TreeNode preRoot)
    {
        var newRoot = preRoot.leftNode;

        newRoot.ChangeParent(preRoot.parent, preRoot.isLeft);

        preRoot.leftNode = newRoot.rightNode;

        if (newRoot.rightNode != null)
        {
            newRoot.rightNode.ChangeParent(preRoot, true);
        }
        preRoot.ChangeParent(newRoot, false);

        int temp = newRoot.color;
        newRoot.color = preRoot.color;
        preRoot.color = temp;
    }
}
