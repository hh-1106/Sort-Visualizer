using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSort : SortAlgorithm
{
    public override IEnumerator Sort(ArrayVisual A, float delay)
    {
        for (int gap = A.n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < A.n; i++)
            {
                A.States[i] = 2;
                int temp = A[i];
                int j;
                for (j = i; j >= gap && A[j - gap] > temp; j -= gap)
                {
                    A[j] = A[j - gap];
                    A.States[j] = 1;
                    yield return new WaitForSeconds(delay);
                    A.States[j] = 0;
                }
                A[j] = temp;
                A.States[i] = 0;
            }
        }
    }
}
