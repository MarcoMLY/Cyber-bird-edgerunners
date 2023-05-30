using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Events
{
    [System.Serializable]
    public class UnityEventGeneric<T> : UnityEvent<T> { }
    [System.Serializable]
    public class UnityEventGeneric<T1, T2> : UnityEvent<T1, T2> { }
    [System.Serializable]
    public class UnityEventGeneric<T1, T2, T3> : UnityEvent<T1, T2, T3> { }
    [System.Serializable]
    public class UnityEventGeneric<T1, T2, T3, T4> : UnityEvent<T1, T2, T3, T4> { }
    [System.Serializable]
    public class UnityEventIntiger : UnityEvent<int> { }
    [System.Serializable]
    public class UnityEventVariables : UnityEvent<Component, int> { }
    [System.Serializable]
    public class UnityEventColor : UnityEvent<Color> { }
}
