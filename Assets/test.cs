using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class test
{
    // Start is called before the first frame update
    private PP p = new PP();
    
    
    [SerializeField]int ss = 0;
    void Start()
    {
        int s = 10;
        p.MessageTarget += Fire(1);
        
        if (ss != 20)
        {
            Debug.Log($"grrrr");
        }
    }
    


    public int Fire(int x)
    {
        x++;
        Console.WriteLine("Fire");
        return x;
    }
}

public delegate void ActSSion<in T>(T obj);
class PP
{
    
    public event Action<int,int> MessageTarget;

    private delegate void Action<in T>(T obj);
    
}
