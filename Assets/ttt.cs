using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ttt : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public Transform c;
    public float ff = 0.5f;
    public float _cellSize = 2;
   
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.normal.textColor = Color.green;
        guiStyle.fontSize = 12;

        if (a != null && b != null && c != null)
        {
            var min = a.position - b.position;
            var plust = a.position + b.position;
            var up =   new Vector3(1, -1, 0); 
            var left = new Vector3(-1, -1, 0); 
            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.DrawLine(Vector3.zero, a.position);
            UnityEditor.Handles.DrawLine(Vector3.zero, b.position);
            
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawLine(a.position,b.position);
            
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawLine(Vector3.zero,up);
            
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawLine(Vector3.zero, left);
            
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawLine(Vector3.zero, min);
            
            
            var dot = Vector3.Dot(up, min);
            var dot2 = Vector3.Dot(left, min);
            var upleft = Mathf.Sign(dot);
            float dowmright = Mathf.Sign(dot2);

            float sum = upleft + dowmright;
            
            float y = sum < 0 ? -1 : (sum > 0 ?  1 : 0);
            float x = sum !=0 ? 0 : (upleft > 0 ? -1 : 1);
            Vector3 direction = new Vector3(x, y, 0);

            float inputdistance = Vector3.Distance(a.position, b.position);
            var distance = Math.Min(inputdistance, _cellSize); 

            c.position = a.position + (direction * distance);
            UnityEditor.Handles.Label(c.position + Vector3.up, $"<b>dir: {direction}  dis:{inputdistance} =>{direction * distance}</b>" ,guiStyle);
            UnityEditor.Handles.Label(transform.position + Vector3.up, $"<b>{upleft} : {dowmright}</b>" ,guiStyle);
        }
    }
#endif
}
