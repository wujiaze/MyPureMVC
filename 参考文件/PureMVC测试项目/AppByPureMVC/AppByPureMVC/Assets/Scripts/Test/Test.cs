using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Debug.Log("sssss");
    }
}
