using System;
using UnityEngine;


public class CubeInterface : MonoBehaviour
{
    public float edgeOffset = 0.5f;

    public ConnectionPoint[] connectionPoints;

 
    [Serializable]
    public struct ConnectionPoint
    {
        public Vector3 position;
        public Vector3 direction;
        public bool IsConnectable;
    }
#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        var mat = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        if (connectionPoints != null)
        {
            for(int i = 0; i < connectionPoints.Length; i++)
            {
                var cp = connectionPoints[i];
                
                if (cp.IsConnectable)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(cp.position, cp.position + cp.direction * 0.8f);
                Gizmos.DrawSphere(cp.position, 0.4f);
            }
        }
        Gizmos.matrix = mat;
    }
#endif
}
