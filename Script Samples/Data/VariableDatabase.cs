using System;
using System.Collections;
using System.Collections.Generic;

public class VariableDatabase
{
    private Dictionary<string, object> _variables = new();


    public void SetValue(string variableName, string stringValue)
    {
        _variables[variableName] = stringValue;
    }

    public void SetValue(string variableName, float floatValue)
    {
        _variables[variableName] = floatValue;
    }

    public void SetValue(string variableName, int intValue)
    {
        _variables[variableName] = intValue;
    }

    public void SetValue(string variableName, bool boolValue)
    {
        _variables[variableName] = boolValue;
    }

    public void AddValue(string variableName, int intValue)
    {
        int oldValue = (int)_variables[variableName];
        _variables[variableName] = oldValue + intValue;
    }

    public string GetStringValue(string variableName)
    {
        var result = _variables[variableName];

        return (string)result;
    }
    public float GetFloatValue(string variableName)
    {
        var result = _variables[variableName];

        return (float)result;
    }
    public int GetIntValue(string variableName)
    {
        if (_variables.ContainsKey(variableName))
        {
            var result = _variables[variableName];
            return (int)result;
        }
        else
        {
            return 0;
        }
    }

    public bool GetBoolValue(string variableName)
    {   
        if (_variables.TryGetValue(variableName, out var result))
        {
            if (result is bool boolResult)
            {
                return boolResult;
            }
        }

        return false;
    }

    public bool Contains(string variableName)
    {
        return _variables.ContainsKey(variableName);
    }

    public float CreateOrGetFloatValue(string variableName)
    {
        if (!_variables.ContainsKey(variableName))
        {
            // Default float value
            _variables[variableName] = 0.0f;
        }

        return (float)_variables[variableName];
    }

    public bool CreateOrGetBoolValue(string variableName)
    {
        if (!_variables.ContainsKey(variableName))
        {
            // Default bool value
            _variables[variableName] = true;
        }

        return (bool)_variables[variableName];
    }

    public int CreateOrGetIntValue(string variableName)
    {
        if (!_variables.ContainsKey(variableName))
        {
            // Default int value
            _variables[variableName] = 0;
        }

        return (int)_variables[variableName];
    }

    public object GetTypeByKey(string key)
    {
        if (_variables.TryGetValue(key, out object foundType))
        {
            return foundType;
        }

        return null;
    }

    public Dictionary<string, object> GetDict()
    {
        return _variables;
    }
}
