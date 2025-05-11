using UnityEngine;

namespace Code.Common
{
    public class MeshBoundsExtender:MonoBehaviour
    {
        private void Start()
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.bounds = new Bounds(mesh.bounds.center, mesh.bounds.size*2);

        
        }
    }
}