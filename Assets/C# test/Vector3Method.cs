using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 07/09/2019 13:50:12
//DESC : 这个脚本是对vecttor3的unity方法的一些学习和理解
//vector3.Angle()是无符号的， 只能计算两个两个向量的锐角,经实验，范围只能是0到180
//vector3.Slerp(float a, float b, float t) 球形插值，和线性插值的区别就是球形插值是将角度插值，
public class Vector3Method : MonoBehaviour
{
    public Transform sunRise;
    public Transform sunSet;
    private float startTime;
    private float needTime = 10.0f;
   
   //method 2
    private float speed = 10; //速度
    private float distanceTarget; //两者之间的距离
    private bool move = true;
    
    //test Angle
    public Transform center;
    public Transform objA;
    public Transform objB;
    //test rotateTowards 转向指定物体
    public Transform targetTransform;
    public Transform tank;
    public float rotateSpeed = 10.0f;
    void Awake()
    {
    }
    // Start is called before the first frame updat
    void Start()
    {
        startTime = Time.time;
        //计算两者之间的距离
        distanceTarget = Vector3.Distance(sunRise.position, sunSet.position);

    }

    // Update is called once per frame
    void Update()
    {
        //method 1:
        Vector3 center = (sunRise.position + sunSet.position) * 0.5f;
        center -= new Vector3(0, 1, 0);

        //interpolate over the arc relative to center
        //Vector3 riseRelCenter = center - sunRise.position; // 这样是不行的，夹脚不对；
       // Vector3 setRelCenter = center - sunSet.position;
        // Vector3 riseRelCenter = sunRise.position - center;
        // Vector3 setRelCenter = sunSet.position - center;
        // float fracComplete = (Time.time - startTime) / needTime;
        // sunRise.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        // transform.position += center;

        // Debug.DrawLine(center, sunRise.position, Color.red);
        // Debug.DrawLine(center, sunSet.position, Color.blue);
        
        //method 2
       // StartCoroutine(Shoot());
        // float angle = Vector3.Angle(objA.position, objB.position);
        // objA.RotateAround(objB.position, -objB.forward, Time.deltaTime * 30.0f);
        // Debug.Log(angle);
        
        //for rotateTowards
        // Vector3 targetDir = targetTransform.position - tank.position;
        // float step = speed * Time.deltaTime;
        // Vector3 newDir = Vector3.RotateTowards(tank.forward, targetDir, step, 0);
        // Debug.DrawRay(tank.position, newDir, Color.red);
        // tank.rotation = Quaternion.LookRotation(newDir);
        //for slerp
        Vector3 riseRelCenter = sunRise.position - center;
        Vector3 setRelCenter = sunSet.position - center;
        float fracComplete = (Time.time - startTime) / needTime;
        sunRise.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        sunRise.position += center;
    }
    IEnumerator Shoot()
    {
        yield return new WaitForEndOfFrame();
        while(move)
        {
            //让始终看向目标
            sunRise.LookAt(sunSet.transform.position);
            //计算弧线中夹脚
            float angle = Mathf.Min(1, Vector3.Distance(sunRise.position, sunSet.position) / distanceTarget * 45);
            
            //改变物体角度
            sunRise.rotation = sunRise.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
            float currentdistance = Vector3.Distance(sunRise.position, sunSet.position);
            if(currentdistance <= 0.5f) 
                move = false;
            sunRise.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime * currentdistance));
            yield return null;
        } 

    }
}
