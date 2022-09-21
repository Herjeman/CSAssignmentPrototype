
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _name;

    public List<GameObject> worms;

    public string GetName() { return _name; }
    public void SetName(string name) { _name = name; }
}
