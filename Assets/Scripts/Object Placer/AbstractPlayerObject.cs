using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerObject : MonoBehaviour {

    public abstract bool getInstantiated();

    public abstract void setInstantiated(bool b);

    public abstract int getCost();

    public abstract Vector3 getOffset();

    public abstract KeyCode getKeyCode();

    public abstract GameObject getPreviewPlayerObject();

    public abstract GameObject getGameObject();
    
    public abstract AbstractPlayerObject getInstance();
}