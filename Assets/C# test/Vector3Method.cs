using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 07/09/2019 13:50:12
//DESC : 这个脚本是对vecttor3的unity方法的一些学习和理解
public class Vector3Method : MonoBehaviour
{
    public Transform sunRise;
    public Transform sunSet;
    private float startTime;
    private float needTime = 1.0f;
    void Awake()
    {
    }
    // Start is called before the first frame updat
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //method 1:
        Vector3 center = (sunRise.position + sunSet.position) * 0.5f;
        center -= new Vector3(0, 2, 0);

        //interpolate over the arc relative to center
        //Vector3 riseRelCenter = center - sunRise.position; // 这样是不行的，夹脚不对；
       // Vector3 setRelCenter = center - sunSet.position;
        Vector3 riseRelCenter = sunRise.position - center;
        Vector3 setRelCenter = sunSet.position - center;
        float fracComplete = (Time.time - startTime) / needTime;
        sunRise.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        Debug.DrawLine(center, sunRise.position, Color.red);
        Debug.DrawLine(center, sunSet.position, Color.blue);

        //Method 2:
        
    }
}
