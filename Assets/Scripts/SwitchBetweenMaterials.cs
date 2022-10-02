using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwitchBetweenMaterials : MonoBehaviour
{
    public Material[] materialList;
    public GameObject thisObject;

    [SerializeField]
    private float timeBeforeChange = 2;
    


    // Update is called once per frame
    void Update()
    {

       
        
            for (int i = 0; i < materialList.Length; i++)
            {
          
            timeBeforeChange -= Time.deltaTime;

            if (timeBeforeChange < 0)
                {

                thisObject.GetComponent<MeshRenderer>().material = materialList[i];
                    
                    timeBeforeChange = 2;
                }

            if(i >= materialList.Length)
            {
                i = 0;
            }

            }
        



    }
}
