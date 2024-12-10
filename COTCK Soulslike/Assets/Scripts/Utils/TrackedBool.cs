using UnityEngine;

[System.Serializable]
public class TrackedBool
{
    [SerializeField] private bool _value;

    // Delegate and event to notify when the int has changed
    public delegate void OnValueChangedDelegate(bool previousValue, bool newValue);
    public event OnValueChangedDelegate OnValueChanged;

    // Property for the value, with change detection
    public bool Value
    {
        get { return _value; }
        set
        {
            if (_value != value)
            {
                bool oldValue = _value;
                _value = value;
                Debug.Log($"TrackedBool value changed from {oldValue} to {_value}");

                // Trigger the event and pass both old and new values
                OnValueChanged?.Invoke(oldValue, _value);
            }
        }
    }

    // Constructor to initialize the value
    public TrackedBool(bool initialValue = false)
    {
        _value = initialValue;
    }
}
