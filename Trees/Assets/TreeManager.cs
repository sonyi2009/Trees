using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class TreeManager : MonoBehaviour
{
    public Text[] texts;
    public GameObject node;
    public static TreeManager instance;
    public List<GameObject> lineList = new List<GameObject>();
    List<GameObject> treeNodeList = new List<GameObject>();
    public Dropdown dropDown;
    private void Start()
    {
        instance = this;
        dropDown.onValueChanged.AddListener(ChangeTree);
        ChangeTree(0);
    }
    void ChangeNodeValue(GameObject gb,TreeNode node)
    {
        gb.GetComponentInChildren<Text>().text = node.value + "";
        if(node.color == 0) gb.GetComponentInChildren<Image>().color = new Color(1,0.5f,0.5f);
        if(node.color == 1) gb.GetComponentInChildren<Image>().color = Color.black;
        if(node.color == -1) gb.GetComponentInChildren<Image>().color = Color.black;
    }
    void DrawNodeLine(TreeNode root)
    {
        var leftNode = root.leftNode;
        if (leftNode != null)
        {
            GameObject gb = new GameObject("writeLine");
            var line = gb.AddComponent<LineRenderer>();
            lineList.Add(gb);
            line.startColor = Color.white;
            line.endColor = Color.white;
            line.startWidth = 5f;
            line.endWidth = 5f;
            line.positionCount = 2;//设置两点
            line.SetPosition(0, new Vector3(root.X + 960, root.Y + 540, 0));
            line.SetPosition(1, new Vector3(leftNode.X + 960, leftNode.Y + 540, 0));
        }
        var rightNode = root.rightNode;

        if (rightNode != null)
        {
            GameObject gb = new GameObject("writeLine");
            var line = gb.AddComponent<LineRenderer>();
            lineList.Add(gb);
            line.startColor = Color.white;
            line.endColor = Color.white;
            line.startWidth = 5f;
            line.endWidth = 5f;
            line.positionCount = 2;//设置两点
            line.SetPosition(0, new Vector3(root.X + 960, root.Y + 540, 0));
            line.SetPosition(1, new Vector3(rightNode.X + 960, rightNode.Y + 540, 0));
        }
    }
    public void ShowTree(List<TreeNode> trees)
    {
        for (int i = 0; i < treeNodeList.Count; i++)
        {
            Destroy(treeNodeList[i]);
        }
        for (int i = 0; i < lineList.Count; i++)
        {
            Destroy(lineList[i]);
        }
        lineList.Clear();
        treeNodeList.Clear();
        foreach (var treeNode in trees)
        {
            var gb = GameObject.Instantiate(node, node.transform.parent);
            gb.transform.localPosition = new Vector3(treeNode.X, treeNode.Y, 0);
            ChangeNodeValue(gb, treeNode);
            DrawNodeLine(treeNode);
            treeNodeList.Add(gb);
        }
        ShowOrderInfo(trees[0]);
    }

    public StringBuilder orderInfo = new StringBuilder();
    public void Preorder(TreeNode root)
    {
        if (root!=null)
        {
            orderInfo.Append(root.value+",");
            Preorder(root.leftNode);
            Preorder(root.rightNode);
        }
    }
    public void Inorder(TreeNode root)
    {
        if (root != null)
        {
            Preorder(root.leftNode);
            orderInfo.Append(root.value + ",");
            Preorder(root.rightNode);
        }
    }
    public void Postorder(TreeNode root)
    {
        if (root != null)
        {
            Preorder(root.leftNode);
            Preorder(root.rightNode);
            orderInfo.Append(root.value + ",");
        }
    }
    public void ShowOrderInfo(TreeNode root)
    {
        orderInfo.Clear();
        Preorder(root);
        var str = orderInfo.ToString();
        texts[0].text = "先序遍历：" + str.Substring(0, str.LastIndexOf(","));
        orderInfo.Clear();
        Inorder(root);
        str = orderInfo.ToString();
        texts[1].text = "中序遍历：" + str.Substring(0, str.LastIndexOf(","));
        orderInfo.Clear();
        Postorder(root);
        str = orderInfo.ToString();
        texts[2].text = "后序遍历：" + str.Substring(0, str.LastIndexOf(","));
    }
    private int[] array = new int[9] { 3, 2, 1, 5, 6, 10, 9, 7, 8 };
    public void ChangeTree(int x)
    {
        switch (x)
        {
            case 0:
                ShowTree(BinaryStack.instance.InitBinaryStack(array));
                break;
            case 1:
                ShowTree(RedBlackTree.instance.InitRBTree(array));
                break;
            case 2:
                ShowTree(AvlTree.instance.InitAvlTree(array));
                break;
        }
    }
}
