using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketSort : SortAlgorithm
{
    public override IEnumerator Sort(ArrayVisual A, float delay)
    {
        // 设置10个桶
        int num = 10;
        List<int>[] buckets = new List<int>[num];
        foreach (int a in A.A)
        {
            int i = (int)Mathf.Floor(a / (A.n / num));    // 桶的序号
            i = Mathf.Clamp(i, 0, num - 1);
            if (buckets[i] == null) buckets[i] = new List<int>();
            buckets[i].Add(a);
        }
        int j = 0;
        foreach (List<int> b in buckets)
        {
            if (b != null)
            {
                // b.sort(null);
                A.States[j] = 1;
                yield return new WaitForSeconds(delay);
                A.States[j] = 0;

                foreach (int a in b)
                {
                    A.States[j] = 2;
                    A[j] = a;
                    yield return new WaitForSeconds(delay);
                    A.States[j++] = 0;
                }
            }
        }
    }
}
