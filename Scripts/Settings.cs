/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static GameObject settings;
    public static bool isSettings;
    // Start is called before the first frame update
    void Start()
    {
        settings = GameObject.Find("Setting").transform.GetChild(0).gameObject;
    }

    public static void ActiveSettings()
    {
        PauseMenu.pauseMenuUI.SetActive(false);
        isSettings = true;
        settings.SetActive(isSettings);
    }

    public static void DisableSettings()
    {
        PauseMenu.pauseMenuUI.SetActive(true);
        isSettings = false;
        settings.SetActive(isSettings);
    }
}
*/