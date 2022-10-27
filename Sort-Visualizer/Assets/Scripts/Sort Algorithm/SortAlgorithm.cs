using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class SortAlgorithm
{
    public virtual IEnumerator Sort(BaseArrayVisual A, float delay)
    {
        for (int i = 0; i < A.n; i++)
        {
            A[i] = 1;
            yield return new WaitForSeconds(delay);
        }
    }
}