using UnityEngine;
using System.Collections;

public class InventoryItem
{
    private Inventory inventory;
    private int level;
    private int count;

    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
  
    public int Count
    {
        get { return count; }
        set { count = value; }
    }



}
