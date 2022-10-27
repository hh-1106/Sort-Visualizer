using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : ISortAlgorithm
{
    public IEnumerator Sort(BaseArrayVisual A, float delay)
    {
        for (int j = 1; j < A.n; j++)
        {
            A.States[j] = 2;
            int k = A[j];
            // Insert A[j] into the sorted sequence A[0..j-1]
            int i = j - 1;
            while (i >= 0 && A[i] > k)
            {
                A.States[i] = 1;
                yield return new WaitForSeconds(delay);
                A.States[i] = 0;

                A[i + 1] = A[i];
                i--;
            }
            A[i + 1] = k;
            A.States[j] = 0;
        }
    }
}
