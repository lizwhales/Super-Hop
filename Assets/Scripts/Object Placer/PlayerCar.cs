using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : AbstractPlayerObject {
    private int cost = 1;
    private Vector3 offset = new Vector3(2F, 0.5F, 2F);
    public KeyCode buildKey = KeyCode.Alpha4;
    public GameObject PreviewPlayerObject;
    public GameObject PlayerObject;
    private bool instantiated = false;

    public static PlayerCar Instance;

    public override bool getInstantiated() {
        return instantiated;
    }

    public override void setInstantiated(bool b) {
        instantiated = b;
    }

    public override int getCost() {
        return cost;
    }

    public override Vector3 getOffset() {
        return offset;
    }

    private void Awake() {
        Instance = this;
    }

    public override KeyCode getKeyCode() {
        return buildKey;
    }

    public override GameObject getPreviewPlayerObject() {
        return PreviewPlayerObject;
    }

    public override GameObject getGameObject() {
        return PlayerObject;
    }

    public override AbstractPlayerObject getInstance() {
        return Instance;
    }

}