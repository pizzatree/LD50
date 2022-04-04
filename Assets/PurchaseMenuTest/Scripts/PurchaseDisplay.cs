using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseDisplay : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(Vector3.down*Time.deltaTime*10,Space.World);
    }
}
