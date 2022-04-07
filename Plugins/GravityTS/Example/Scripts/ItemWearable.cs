using System;
using XEntity;
using GravityTS.Wearables;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "XEntity/ItemWearable")]
public class ItemWearable : Item
{
    public Wearable wearable;
}
