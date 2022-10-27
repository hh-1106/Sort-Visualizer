using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : SortAlgorithm
{
    public override IEnumerator Sort(BaseArrayVisual A, float delay)
    {
        for (int i = 0; i < A.n; i++)
        {
            int j = Random.Range(1, A.n);
            A.Swap(0, j);
            A.States[j] = 1;
            yield return new WaitForSeconds(delay);
            A.States[j] = 0;
        }
    }
}
