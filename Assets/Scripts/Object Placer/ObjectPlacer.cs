using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    RaycastHit hit;
    GameObject previewPlayerObjectInstance;
    GameObject playerObjectInstance;
    AbstractPlayerObject PlayerObject;
    Ray ray;
    Vector3 centerPointer = new Vector3(0.5F, 0.5F, 0);
    float maxBuildDistance = 100.0F;
    public KeyCode confirmBuild = KeyCode.Mouse0;
    public KeyCode cancelBuild = KeyCode.Mouse1;
    List<AbstractPlayerObject> buildables = new List<AbstractPlayerObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Will need to remove this later v
        //CoinCounter.addCoins(100);
        buildables.Add(PlayerCube.Instance);
        buildables.Add(PlayerRamp.Instance);
        buildables.Add(PlayerColumn.Instance);
        buildables.Add(PlayerCar.Instance);
        PlayerObject = PlayerCube.Instance;
    }

    // Update is called once per frame 
    void Update()
    {

        foreach(AbstractPlayerObject b in buildables) {
            if (Input.GetKeyDown(b.getKeyCode()) && b.getInstantiated() == false) {
                // Remove any current previews
                foreach(AbstractPlayerObject current in buildables) {
                    if (b != current && current.getInstantiated() == true) {
                        Destroy(previewPlayerObjectInstance);
                        current.setInstantiated(false);
                    }
                }
                previewPlayerObjectInstance = Instantiate(b.getPreviewPlayerObject());
                b.setInstantiated(true);
                PlayerObject = b.getInstance();
            }
        }

        if (PlayerObject.getInstantiated() == true) {
            ray = Camera.main.ViewportPointToRay(centerPointer);
            if (Physics.Raycast(ray, out hit, maxBuildDistance, LayerMask.GetMask("Ground"))) {
                previewPlayerObjectInstance.transform.position = hit.point + Vector3.Scale(hit.normal, PlayerObject.getOffset());
            }

            if (Input.GetKeyDown(confirmBuild)) {
                if (CoinCounter.removeCoins(PlayerObject.getCost())) {
                    playerObjectInstance = Instantiate(PlayerObject.getGameObject(), previewPlayerObjectInstance.transform.position, previewPlayerObjectInstance.transform.rotation);
                    Destroy(previewPlayerObjectInstance);
                    PlayerObject.setInstantiated(false);
                } else {
                    Destroy(previewPlayerObjectInstance);
                    PlayerObject.setInstantiated(false);
                }

            }

            if (Input.GetKeyDown(cancelBuild)) {
                Destroy(previewPlayerObjectInstance);
                PlayerObject.setInstantiated(false);
            }

        }

    }
}
