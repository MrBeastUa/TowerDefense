using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInputCanvasInfo : MonoBehaviour
{
    [SerializeField]
    private Text _mapName, _width, _height;
    [SerializeField]
    private Text _errorString;
    public uint Width
    {
        get {
            uint num = 0;
            if (uint.TryParse(_width.text, out num))
            {
                if (num < 100)
                {
                    return num;
                }
                else
                {
                    _errorString.text = "Width must be less then 100!";
                    return 0;
                }
            }
            else
            {
                _errorString.text = "Width must be a positive integer number!";
                return 0;
            }
        }
    }

    public uint Height
    {
        get
        {
            uint num = 0;
            if (uint.TryParse(_height.text, out num))
            {
                if (num < 100)
                {
                    return num;
                }
                else
                {
                    _errorString.text = "Height must be less then 100!";
                    return 0;
                }
            }
            else
            {
                _errorString.text = "Height must be a positive integer number!";
                return 0;
            }
        }
    }

    public string Name
    {
        get
        {
            if (_mapName.text == "")
                _errorString.text = "Map name must be non empty!";
            return _mapName.text;
        }
    }
}
