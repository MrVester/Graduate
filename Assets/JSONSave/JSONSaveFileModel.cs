using System;
using System.Linq;


[Serializable]
internal class JSONSaveFileModel
{
    public StringItem[] StringData = new StringItem[0];
    public IntItem[] IntData = new IntItem[0];
    public FloatItem[] FloatData = new FloatItem[0];
    public BoolItem[] BoolData = new BoolItem[0];
    public Vector2Item[] Vector2Data = new Vector2Item[0];
    public Vector3Item[] Vector3Data = new Vector3Item[0];

    [Serializable]
    public class StringItem
    {
        public string Key;
        public string Value;

        public StringItem(string K, string V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class IntItem
    {
        public string Key;
        public int Value;

        public IntItem(string K, int V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class FloatItem
    {
        public string Key;
        public float Value;

        public FloatItem(string K, float V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class BoolItem
    {
        public string Key;
        public bool Value;

        public BoolItem(string K, bool V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class Vector2Item
    {
        public string Key;
        public float X;
        public float Y;

        public Vector2Item(string K, float x, float y)
        {
            Key = K;
            X = x;
            Y = y;
        }
    }

    [Serializable]
    public class Vector3Item
    {
        public string Key;
        public float X;
        public float Y;
        public float Z;

        public Vector3Item(string K, float x, float y, float z)
        {
            Key = K;
            X = x;
            Y = y;
            Z = z;
        }
    }
    

    public object GetValueForKey(string key, object defaultValue)
    {
        if (defaultValue is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return StringData[i].Value;
                }
            }
        }
        if (defaultValue is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return IntData[i].Value;
                }
            }
        }
        if (defaultValue is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return FloatData[i].Value;
                }
            }
        }
        if (defaultValue is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return BoolData[i].Value;
                }
            }
        }
        if (defaultValue is UnityEngine.Vector2)
        {
            for (int i = 0; i < Vector2Data.Length; i++)
            {
                if (Vector2Data[i].Key.Equals(key))
                {
                    return new UnityEngine.Vector2(Vector2Data[i].X, Vector2Data[i].Y);
                }
            }
        }
        if (defaultValue is UnityEngine.Vector3)
        {
            for (int i = 0; i < Vector3Data.Length; i++)
            {
                if (Vector3Data[i].Key.Equals(key))
                {
                    return new UnityEngine.Vector3(Vector3Data[i].X, Vector3Data[i].Y, Vector3Data[i].Z);
                }
            }
        }
        return defaultValue;
    }

    public void UpdateOrAddData(string key, object value)
    {
        if (HasKeyFromObject(key, value))
        {
            SetValueForExistingKey(key, value);
        }
        else
        {
            SetValueForNewKey(key, value);
        }
    }

    private void SetValueForNewKey(string key, object value)
    {
        if (value is string)
        {
            var dataAsList = StringData.ToList();
            dataAsList.Add(new StringItem(key, (string) value));
            StringData = dataAsList.ToArray();
        }
        if (value is int)
        {
            var dataAsList = IntData.ToList();
            dataAsList.Add(new IntItem(key, (int) value));
            IntData = dataAsList.ToArray();
        }
        if (value is float)
        {
            var dataAsList = FloatData.ToList();
            dataAsList.Add(new FloatItem(key, (float) value));
            FloatData = dataAsList.ToArray();
        }
        if (value is bool)
        {
            var dataAsList = BoolData.ToList();
            dataAsList.Add(new BoolItem(key, (bool) value));
            BoolData = dataAsList.ToArray();
        }
        if (value is UnityEngine.Vector2)
        {
            var dataAsList = Vector2Data.ToList();
            var vector2 = (UnityEngine.Vector2) value;
            dataAsList.Add(new Vector2Item(key, vector2.x, vector2.y));
            Vector2Data = dataAsList.ToArray();
        }
        if (value is UnityEngine.Vector3)
        {
            var dataAsList = Vector3Data.ToList();
            var vector3 = (UnityEngine.Vector3) value;
            dataAsList.Add(new Vector3Item(key, vector3.x, vector3.y, vector3.z));
            Vector3Data = dataAsList.ToArray();
        }
    }

    private void SetValueForExistingKey(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    StringData[i].Value = (string) value;
                }
            }
        }
        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    IntData[i].Value = (int) value;
                }
            }
        }
        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    FloatData[i].Value = (float) value;
                }
            }
        }
        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    BoolData[i].Value = (bool) value;
                }
            }
        }
        if (value is UnityEngine.Vector2)
        {
            for (int i = 0; i < Vector2Data.Length; i++)
            {
                if (Vector2Data[i].Key.Equals(key))
                {
                    var vector2 = (UnityEngine.Vector2) value;
                    Vector2Data[i].X = vector2.x;
                    Vector2Data[i].Y = vector2.y;
                }
            }
        }
        if (value is UnityEngine.Vector3)
        {
            for (int i = 0; i < Vector3Data.Length; i++)
            {
                if (Vector3Data[i].Key.Equals(key))
                {
                    var vector3 = (UnityEngine.Vector3) value;
                    Vector3Data[i].X = vector3.x;
                    Vector3Data[i].Y = vector3.y;
                    Vector3Data[i].Z = vector3.z;
                }
            }
        }
    }

    public bool HasKeyFromObject(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }
        if (value is UnityEngine.Vector2)
        {
            for (int i = 0; i < Vector2Data.Length; i++)
            {
                if (Vector2Data[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }
        if (value is UnityEngine.Vector3)
        {
            for (int i = 0; i < Vector3Data.Length; i++)
            {
                if (Vector3Data[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DeleteKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < Vector2Data.Length; i++)
        {
            if (Vector2Data[i].Key.Equals(key))
            {
                var dataAsList = Vector2Data.ToList();
                dataAsList.RemoveAt(i);
                Vector2Data = dataAsList.ToArray();
            }
        }
        for (int i = 0; i < Vector3Data.Length; i++)
        {
            if (Vector3Data[i].Key.Equals(key))
            {
                var dataAsList = Vector3Data.ToList();
                dataAsList.RemoveAt(i);
                Vector3Data = dataAsList.ToArray();
            }
        }
    }

    public void DeleteString(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteInt(string key)
    {
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteFloat(string key)
    {
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteBool(string key)
    {
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteVector2(string key)
    {
        for (int i = 0; i < Vector2Data.Length; i++)
        {
            if (Vector2Data[i].Key.Equals(key))
            {
                var dataAsList = Vector2Data.ToList();
                dataAsList.RemoveAt(i);
                Vector2Data = dataAsList.ToArray();
            }
        }
    }

    public void DeleteVector3(string key)
    {
        for (int i = 0; i < Vector3Data.Length; i++)
        {
            if (Vector3Data[i].Key.Equals(key))
            {
                var dataAsList = Vector3Data.ToList();
                dataAsList.RemoveAt(i);
                Vector3Data = dataAsList.ToArray();
            }
        }
    }

    public bool HasKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < Vector2Data.Length; i++)
        {
            if (Vector2Data[i].Key.Equals(key))
            {
                return true;
            }
        }
        for (int i = 0; i < Vector3Data.Length; i++)
        {
            if (Vector3Data[i].Key.Equals(key))
            {
                return true;
            }
        }
        return false;
    }
}