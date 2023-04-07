using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuManagerScripts : MonoBehaviour {

    public MenuScripts currentMenu;

    public void Start()
    {
        ShowMenu(currentMenu);

    }

    public void ShowMenu(MenuScripts menu)
    {

        if (currentMenu != null)
            currentMenu.IsOpen = false;


        currentMenu = menu;
        currentMenu.IsOpen = true;
    }

    public void KeluarGame()
    {
        Application.Quit();
    }

}


