﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine.UI;

public class TestMediator : Mediator
{
    public new const string NAME = "TestMediator";

    private Text levelText;
    private Button levelUpButton;

    public TestMediator(GameObject root) : base(NAME)
    {
        levelText = GameUtility.GetChildComponent<Text>(root, "Text/LevelText");
        levelUpButton = GameUtility.GetChildComponent<Button>(root, "LevelUpButton");

        levelUpButton.onClick.AddListener(OnClickLevelUpButton);
    }

    private void OnClickLevelUpButton()
    {
        SendNotification(NotificationConstant.LevelUp);
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(NotificationConstant.LevelChange);
        return list;
    }

    public override void HandleNotification(PureMVC.Interfaces.INotification notification)
    {
        switch (notification.Name)
        {
            case NotificationConstant.LevelChange:
                CharacterInfo ci = notification.Body as CharacterInfo;
                levelText.text = ci.Level.ToString();
                break;
            default:
                break;
        }
    }
}