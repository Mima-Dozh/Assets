using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitFabrica : MonoBehaviour
{
    [SerializeField] private int unitCount = 16;
    [SerializeField] private int unitCost = 5;
    [SerializeField] private FabricUnitType type;
    private bool a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var len = mousePosition - transform.position;
        if(Input.GetMouseButtonDown(0)
        && unitCost <= ResourcesFabric.resourcesCount
        && unitCount > 0
        &&  Mathf.Abs(len.x) < 1
        && Mathf.Abs(len.y) < 1)
        {
            unitCount--;
            ResourcesFabric.resourcesCount -= unitCost;
            var x = GameObject.Find("PapaPotato");
            Instantiate(x, transform.position + Vector3.left, Quaternion.identity);
        }
    }
}
