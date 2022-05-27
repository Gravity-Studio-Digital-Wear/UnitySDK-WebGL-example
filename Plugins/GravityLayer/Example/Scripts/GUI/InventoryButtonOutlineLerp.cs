using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonOutlineLerp : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private bool _isOn = true;
    private Color _startColor;
    private Color _endColor;
    private Color _lerpedColor;

    void Start()
    {
        _startColor = _image.color;
        _endColor = _startColor;
        _endColor.a = 0f;
    }

    void Update()
    {
        if (!_isOn) return;
        _lerpedColor = Color.Lerp(_startColor, _endColor, Mathf.PingPong(Time.time, 1));
        _image.color = _lerpedColor;
    }

    public void ToggleOff()
    {
        _isOn = false;
        _image.color = _endColor;
    }
}
