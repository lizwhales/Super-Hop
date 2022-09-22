using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePreview : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    Ray ray;
    Vector3 centerPointer = new Vector3(0.5F, 0.5F, 0);
    float maxBuildDistance = 100.0F;
    Vector3 cubeOffsets = new Vector3(2F, 0.5F, 2F);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ViewportPointToRay(centerPointer);
        if (Physics.Raycast(ray, out hit, maxBuildDistance, LayerMask.GetMask("Ground"))) {
            transform.position = hit.point + Vector3.Scale(hit.normal, cubeOffsets);
        }

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
