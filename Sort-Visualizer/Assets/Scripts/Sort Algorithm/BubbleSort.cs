using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : ISortAlgorithm
{
    public IEnumerator Sort(BaseArrayVisual A, float delay)
    {
        for (int i = 0; i < A.n; i++)
        {
            for (int j = A.n - 1; j > i; j--)
            {
                A.States[j] = 2;
                A.States[j - 1] = 1;

                yield return new WaitForSeconds(delay);

                if (A[j] < A[j - 1])
                {
                    A.Swap(j, j - 1);
                }
                A.States[j] = 0;
                A.States[j - 1] = 0;
            }
        }
    }
}
