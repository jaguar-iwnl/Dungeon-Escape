using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEvent : MonoBehaviour
{
    //handle to the spider
    private Spider _spider;

    private void Start()
    {
        //assign handle to the spider component
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        //Tell spider to fire
        //use handle to call Attack method on the spider
        _spider.Attack();
    }
}
