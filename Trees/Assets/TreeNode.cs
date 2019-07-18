using System.Collections;
using UnityEngine;

public class TreeNode
{
    private float x;
    private float y;
    private int hierarchy;
    public TreeNode parent;
    public TreeNode leftNode;
    public TreeNode rightNode;
    public bool isLeft = true;
    public int color = 0;
    //只有value需要被修改,原因是array[i]的x,y,index,hierarchy,parent都是确定的,需要修改的只有value
    public float X
    {
        get
        {
            if (parent == null) return 0;
            this.hierarchy = parent.Hierarchy + 1;
            float temp = Mathf.Pow(2, parent.Hierarchy);
            float len = 960f / (temp * 2 + 1);
            x = isLeft ? parent.X - len : parent.X + len;
            return x;
        }
    }
    public float Y
    {
        get
        {
            if (parent == null) return 400;
            y = parent.Y - 150;
            return y;
        }
    }
    public int Hierarchy
    {
        get
        {
            if (parent == null) return 0;
            return parent.Hierarchy + 1;
        }
    }
    public int value;
    public void ChangeParent(TreeNode parent,bool isLeft)
    {
        this.isLeft = isLeft;
        this.parent = parent;

        if (parent != null)
        {
            if (isLeft) parent.leftNode = this;
            else parent.rightNode = this;
        }
    }
    #region AVL树相关
    private int GetBalance(TreeNode judgeNode,TreeNode root)
    {
        if (judgeNode == null) return root.Hierarchy;
        Queue queue = new Queue();
        TreeNode node = null;
        queue.Enqueue(judgeNode);
        while (queue.Count > 0)
        {
            node = (TreeNode)queue.Dequeue();
            if (node.leftNode != null) queue.Enqueue(node.leftNode);
            if (node.rightNode != null) queue.Enqueue(node.rightNode);
        }
        return node.Hierarchy;
    }
    public TreeNode GetPreUnBalanceNode()
    {
        var node = parent;
        while (node!=null)
        {
            int leftHeight = GetBalance(node.leftNode,node);
            int rightHeight = GetBalance(node.rightNode,node);
            if (Mathf.Abs(leftHeight - rightHeight) > 1) return node;
            node = node.parent;
        }
        return null;
    }
    public bool ContainInLeftNode(TreeNode judgeNode)
    {
        if (leftNode == null) return false;
        Queue queue = new Queue();
        queue.Enqueue(leftNode);
        while (queue.Count > 0)
        {
            var node = (TreeNode)queue.Dequeue();
            if (node == judgeNode) return true;
            if (node.leftNode != null) queue.Enqueue(node.leftNode);
            if (node.rightNode != null) queue.Enqueue(node.rightNode);
        }
        return false;
    }
    #endregion
    #region RB树相关
    public TreeNode GetBroNode()
    {
        if (parent == null) return null;
        if (isLeft) return parent.rightNode;
        return parent.leftNode;
    }
    #endregion
    public TreeNode(bool isLeft, int value, TreeNode parent, int color = 0)
    {
        this.color = color;
        this.parent = parent;
        this.value = value;
        this.isLeft = isLeft;
        ChangeParent(parent,isLeft);
    }
}
