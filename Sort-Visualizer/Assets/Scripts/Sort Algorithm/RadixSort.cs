using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixSort : ISortAlgorithm
{
    public IEnumerator Sort(BaseArrayVisual A, float delay)
    {
        int max = 0;
        for (int i = 0; i < A.n; i++) max = Mathf.Max(max, A[i]);
        yield return radixSort((int)Mathf.Floor(Mathf.Log10(max) + 1));

        IEnumerator radixSort(int d)
        {
            for (int i = 0; i < d; i++)
            {
                int pow = (int)Mathf.Pow(10, i);
                yield return countingSort(pow);
            }
        }

        IEnumerator countingSort(int pow)
        {
            int[] B = new int[A.n];

            // 计数
            int[] C = new int[10];
            for (int i = 0; i < C.Length; i++) C[i] = 0;
            for (int j = 0; j < A.n; j++)
            {
                C[(A[j] / pow) % 10]++;    //取相应阶位的数
            }
            for (int i = 1; i < 10; i++) C[i] += C[i - 1];

            for (int j = A.n - 1; j >= 0; j--)
            {
                A.States[j] = 2;
                int i = (A[j] / pow) % 10;
                B[--C[i]] = A[j];
                yield return new WaitForSeconds(delay);
                A.States[j] = 0;
            }

            for (int i = 0; i < A.n; i++)
            {
                A.States[i] = 1;
                A[i] = B[i];
                yield return new WaitForSeconds(delay);
                A.States[i] = 0;
            }
        }
    }
}
