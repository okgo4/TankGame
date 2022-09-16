using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField]
    private GameObject buffPrefab;
    [SerializeField]
    private GameObject anchor;
    private bool exist;
    public float renewCount;
    public float disappearCount;

    // Start is called before the first frame update
    void Start()
    {
        exist=false;
        renewCount = 15;
        disappearCount = 10;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!exist)
        {
            renewCount -= Time.deltaTime;
            if (renewCount <= 0)
            {
                renew();
            }
        }
        else
        {
            disappearCount -= Time.deltaTime;
            if (disappearCount <= 0)
            {   
                renewCount=45;
                exist = false;
                disappearCount = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            renew();
        }
    }

    private void renew()
    {
        GameObject buff = Instantiate(buffPrefab);
        buff.transform.position = anchor.transform.position;
        buff.transform.position += new Vector3(0, 1, 0);
        exist = true;
        disappearCount = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (exist == true))
        {
            renewCount=45;
            exist=false;
        }
    }
}
