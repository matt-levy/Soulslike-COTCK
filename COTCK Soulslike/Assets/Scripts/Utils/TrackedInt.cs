public class TrackedInt
{
    private int _value;

    // Delegate and event to notify when the int has changed
    public delegate void OnValueChangedDelegate(int previousValue, int newValue);
    public event OnValueChangedDelegate OnValueChanged;

    // Property for the value, with change detection
    public int Value
    {
        get { return _value; }
        set
        {
            if (_value != value)
            {
                int oldValue = _value;
                _value = value;

                // Trigger the event and pass both old and new values
                OnValueChanged?.Invoke(oldValue, _value);
            }
        }
    }

    // Constructor to initialize the value
    public TrackedInt(int initialValue = 0)
    {
        _value = initialValue;
    }
}
