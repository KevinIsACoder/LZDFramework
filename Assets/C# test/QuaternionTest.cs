using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 07/26/2019 17:08:29
//DESC : ****
//Quaternion.FromToRotation(from, to) 旋转到指定角度
public class QuaternionTest : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
