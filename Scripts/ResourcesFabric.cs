using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesFabric : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private FabricResourceType type;
    private bool overflowed;
    [SerializeField] private int secondsWait = 5;
    [SerializeField] private int countAddAfterWait = 3;
    private float lastTime;
    private float deltaTime;
    public static int resourcesCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPause)
        {
            deltaTime = Time.realtimeSinceStartup - lastTime;
            return;
        }
        var timeNow = Time.realtimeSinceStartup;
        if(timeNow - lastTime - deltaTime > secondsWait)
        {
            lastTime = timeNow;
            resourcesCount ++;
        }
        text.text = resourcesCount.ToString();
    }
}