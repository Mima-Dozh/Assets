using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilBrain : MonoBehaviour
{
    [SerializeField] MainBilding Player;

    private static UnitComponent units;

    public static void AddUnit(UnitComponent comp)
    {
        units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
    }
    void Awake()
    {
        units = new UnitComponent();
    }

    void Update()
    {
        foreach (var comp in units)
        {
            comp.finishPosition = Player.transform.position;
            foreach (var good in UnitSelect.units)
                if((comp.transform.position - good.transform.position).magnitude < 5)
                    comp.finishPosition = good.transform.position;
        }
    }
}