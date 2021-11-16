using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enviroment
{
    public class Wave_Controller : MonoBehaviour
    {
      
        private MeshFilter meshFilter { get; set; }
        
       


        [SerializeField]
        private float amplitude;
        [SerializeField]
        private float length;
        [SerializeField]
        private float speed;
      
        private float offset=0f;

        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh.MarkDynamic();
        }

       
        void Update()
        {
            offset += speed * Time.deltaTime;
            Vector3[] vertecies = meshFilter.mesh.vertices;
            for (int i = 0; i < vertecies.Length; i++)
            {
                vertecies[i].y = GetHeighty(this.transform.position.x + vertecies[i].x);
              

            }

            meshFilter.mesh.vertices = vertecies;
            meshFilter.mesh.RecalculateNormals();
        }
        private float GetHeighty(float x)
        {
            return amplitude * Mathf.Sin(x / length + offset);
        }
    
    }
}
