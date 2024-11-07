using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener(this Button button, Action OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick();
        });
    }
}
