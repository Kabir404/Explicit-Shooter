using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandHeldItem : MonoBehaviour
{
    public TMP_Text ammoCounter1;
    public TMP_Text ammoCounter2;

    // Update is called once per frame
    void Update()
    {
        ammoCounter1.text = "";
        ammoCounter2.text = "";
    }
}
