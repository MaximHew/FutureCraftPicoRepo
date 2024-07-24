using UnityEngine;

namespace CustomTools
{
    public class AlignToTopCenter : MonoBehaviour
    {
        public void AlignToSurface()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Debug.Log($"{gameObject.name} hit {hit.collider.name} at {hit.point}");

                // Align to the center and top of the mesh object
                AlignObjectToTopCenter(hit);
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} did not hit any surface");
            }
        }

        private void AlignObjectToTopCenter(RaycastHit hit)
        {
            // Get the bounds of the mesh collider
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider != null && meshCollider.sharedMesh != null)
            {
                Bounds bounds = meshCollider.sharedMesh.bounds;

                // Calculate the top center position
                Vector3 topCenter = hit.collider.transform.position + hit.collider.transform.up * bounds.extents.y;
                topCenter.x = hit.collider.bounds.center.x;
                topCenter.z = hit.collider.bounds.center.z;

                // Align the object to the top center position
                transform.position = new Vector3(topCenter.x, topCenter.y, topCenter.z);
                Debug.Log($"{gameObject.name} aligned to top center of {hit.collider.name} at {transform.position}");
            }
            else
            {
                Debug.LogWarning($"{hit.collider.name} does not have a MeshCollider with a mesh");
            }
        }
    }
}
