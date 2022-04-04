using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseWindow : MonoBehaviour
{    
    public void CloseWindow() {
        this.transform.localScale = Vector3.zero;
    }

    public static void OpenWindow() {
        GameObject.Find("Purchase_Window").transform.localScale = Vector3.one;
    }
}
