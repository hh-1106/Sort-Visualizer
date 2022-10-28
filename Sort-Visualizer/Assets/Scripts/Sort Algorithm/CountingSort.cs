using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingSort : ISortAlgorithm
{
    public IEnumerator Sort(TriangleArrayVisual A, float delay)
    {
        // 存放排序的输出数组
        int[] B = new int[A.n];
        int k = 0;

        for (int i = 0; i < A.n; i++) k = Mathf.Max(k, A[i]);
        yield return countingSort(A, B, k, delay);

        for (int i = 0; i < A.n; i++)
        {
            A.States[i] = 3;
            A[i] = B[i];
            yield return new WaitForSeconds(delay);
            A.States[i] = 0;
        }
    }

    IEnumerator countingSort(TriangleArrayVisual A, int[] B, int k, float delay)
    {
        // 计数数组C初始化全0
        int[] C = new int[k + 1];
        for (int i = 0; i <= k; i++) C[i] = 0;

        // 计数
        for (int j = 0; j < A.n; j++)
        {
            A.States[j] = 2;
            yield return new WaitForSeconds(delay);
            C[A[j]]++;
            A.States[j] = 0;
        }

        // 加总
        for (int i = 1; i <= k; i++) C[i] += C[i - 1];

        // 把每个元素A[j]放到它在B中的正确位置
        for (int j = A.n - 1; j >= 0; j--)
        {
            A.States[j] = 1;
            B[C[A[j]] - 1] = A[j];
            C[A[j]]--;
            yield return new WaitForSeconds(delay);
            A.States[j] = 0;
        }
    }
}
