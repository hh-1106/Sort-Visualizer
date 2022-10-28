using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : ISortAlgorithm
{
    public IEnumerator Sort(ArrayVisual A, float delay)
    {
        yield return buildMaxHeap(A, delay);

        for (int i = A.n - 1; i >= 0; i--)
        {
            A.States[i] = 2;
            yield return new WaitForSeconds(delay);
            A.Swap(i, 0);
            yield return maxHeapify(A, i, 0, delay);
            A.States[i] = 0;
        }
    }

    private IEnumerator buildMaxHeap(ArrayVisual A, float delay)
    {
        for (int i = A.n / 2 - 1; i >= 0; i--)
        {
            yield return maxHeapify(A, A.n, i, delay);
        }
    }

    // 用于维护最大堆性质
    // 让A[i]的值在最大堆中 逐级下降
    private IEnumerator maxHeapify(ArrayVisual A, int n, int i, float delay)
    {
        int root = i;
        int l = i * 2 + 1;  // left
        int r = i * 2 + 2;  // right

        if (l < n && A[l] > A[root])
        {
            root = l;
        }
        if (r < n && A[r] > A[root])
        {
            root = r;
        }

        if (root != i)
        {
            A.States[i] = 1;
            yield return new WaitForSeconds(delay);
            A.Swap(root, i);
            yield return maxHeapify(A, n, root, delay);
            A.States[i] = 0;
        }

    }
}
