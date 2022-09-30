using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePreview : MonoBehaviour
{

    bool instantiated = false;
    RaycastHit hit;
    public GameObject PreviewPlayerCube;
    public GameObject PlayerCube;
    GameObject previewPlayerCubeInstance;
    GameObject playerCubeInstance;
    Ray ray;
    Vector3 centerPointer = new Vector3(0.5F, 0.5F, 0);
    float maxBuildDistance = 100.0F;
    Vector3 cubeOffsets = new Vector3(2F, 0.5F, 2F);
    public KeyCode buildCubeKey = KeyCode.Alpha1;
    public KeyCode confirmBuild = KeyCode.Mouse0;
    public KeyCode cancelBuild = KeyCode.Mouse1;

    // Start is called before the first frame update
    void Start()
    {
        CoinCounter.addCoins(5);
        Debug.Log("Added 5 coins");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(buildCubeKey) && instantiated == false) {
            previewPlayerCubeInstance = Instantiate(PreviewPlayerCube);
            instantiated = true;
        }

        if (instantiated == true) {
            ray = Camera.main.ViewportPointToRay(centerPointer);
            if (Physics.Raycast(ray, out hit, maxBuildDistance, LayerMask.GetMask("Ground"))) {
                previewPlayerCubeInstance.transform.position = hit.point + Vector3.Scale(hit.normal, cubeOffsets);
            }

            if (Input.GetKeyDown(confirmBuild)) {
                if (CoinCounter.removeCoins(1)) {
                    playerCubeInstance = Instantiate(PlayerCube, previewPlayerCubeInstance.transform.position, previewPlayerCubeInstance.transform.rotation);
                    Destroy(previewPlayerCubeInstance);
                    instantiated = false;
                    Debug.Log("Spent 1 coin");
                    Debug.Log(CoinCounter.coins + " coins remaining.");
                } else {
                    Debug.Log("Not enough coins");
                    Destroy(previewPlayerCubeInstance);
                    instantiated = false;
                }

            }

            if (Input.GetKeyDown(cancelBuild)) {
                Destroy(previewPlayerCubeInstance);
                instantiated = false;
            }

        }

    }
}
