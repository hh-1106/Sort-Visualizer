using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortAlgorithm
{
    public IEnumerator Sort(ArrayVisual A, float delay)
    {
        for (int i = 0; i < A.n; i++)
        {
            A[i] = 1;
            A.States[i] = 0;
            yield return new WaitForSeconds(delay);
        }
    }
}