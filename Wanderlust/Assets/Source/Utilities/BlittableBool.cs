public struct BlittableBool {
    public byte Value { get; private set; }

    public BlittableBool(byte value) {
        Value = value;
    }

    public BlittableBool(bool value) {
        Value = value ? (byte)1 : (byte)0;
    }

    public static implicit operator bool(BlittableBool boolean) {
        return boolean.Value != 0;
    }

    public static implicit operator BlittableBool(bool boolean) {
        return new BlittableBool(boolean);
    }
}