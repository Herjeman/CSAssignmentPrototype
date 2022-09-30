using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector
{
    private int _selectionIndex;
    public static List<int> unavailableIndexes;
    public static List<Color> _colors = new List<Color>();

    public ColorSelector(int index = 0)
    {
        _selectionIndex = index;
        SkipUnavailableIndexes();
        unavailableIndexes.Add(_selectionIndex);
    }

    public Color NextColor()
    {
        ColorSelector.unavailableIndexes.Remove(_selectionIndex);
        _selectionIndex++;
        if (_selectionIndex >= _colors.Count)
        {
            _selectionIndex = 0;
        }

        SkipUnavailableIndexes();

        ColorSelector.unavailableIndexes.Add(_selectionIndex);
        return _colors[_selectionIndex];
    }
    public static void SetColorList(List<Color> colors)
    {
        _colors = colors;
    }

    public Color GetColor()
    {
        return _colors[_selectionIndex];
    }
    
    private void SkipUnavailableIndexes()
    {
        while (unavailableIndexes.Contains(_selectionIndex))
        {
            _selectionIndex++;
            if (_selectionIndex >= _colors.Count)
            {
                _selectionIndex = 0;
            }
        }
    }
}
