using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class OperationConfig
{
    [SerializeField] private OperationType _operationType;
    [SerializeField] private int _digitCapacity;

    public Operation GetOperation()
    {
        var operation = Utility.GetAllInstances<Operation>()
            .FirstOrDefault(t => t.Type == _operationType);

        if (operation != null)
        {
            operation.DigitCapacity = _digitCapacity;
            return operation;
        }

        throw new KeyNotFoundException($"Not found operation of type {_operationType}");
    }
}