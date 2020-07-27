using System;
using UnityEngine;
using System.Collections.Generic;
public class FindTree : MonoBehaviour
{
    public void PreOrderTraversal(TreeNode root)  //先遍历根节点，然后左子树，右子树
    {
        Stack<TreeNode> Nodes = new Stack<TreeNode>();
        if(root == null) return;
        TreeNode node = root;
        while(node != null || Nodes.Count > 0)
        {
            Debug.Log(node.value);
            Nodes.Push(node);      //将左子树压入栈中
            node = node.left;
            
            if(node == null && Nodes.Count > 0)
            {
                node = Nodes.Pop();
                node = node.right;
            }

        }
    }
}
public class TreeNode
{
    public int value;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int value, TreeNode left, TreeNode right)
    {
        this.value = value;
        this.left = left;
        this.right = right;
    }
}