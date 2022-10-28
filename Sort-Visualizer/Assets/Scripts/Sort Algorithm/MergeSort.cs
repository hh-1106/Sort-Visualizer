using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : ISortAlgorithm
{
    public IEnumerator Sort(TriangleArrayVisual A, float delay)
    {
        yield return mergeSort(0, A.n - 1);

        IEnumerator mergeSort(int p, int r)
        {
            if (p < r)
            {
                int q = (int)Mathf.Floor((p + r) / 2);
                yield return mergeSort(p, q);
                yield return mergeSort(q + 1, r);

                A.States[q] = 2;
                A.States[r] = 3;
                yield return merge(p, q, r);
                A.States[q] = 0;
                A.States[r] = 0;
            }
        }

        // 合并两个已排序的子序列
        // pqr是数组下标   p <= q < r
        IEnumerator merge(int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            // 每个堆底部放一张哨兵牌
            int[] L = new int[n1 + 1];
            int[] R = new int[n2 + 1];
            for (int i_ = 0; i_ < n1; i_++)
            {
                L[i_] = A[p + i_];
            }
            for (int j_ = 0; j_ < n2; j_++)
            {
                R[j_] = A[q + 1 + j_];
            }
            L[n1] = int.MaxValue;
            R[n2] = int.MaxValue;

            int i = 0;
            int j = 0;
            for (int k = p; k <= r; k++)
            {
                A.States[k] = 1;
                if (L[i] <= R[j])
                {
                    A[k] = L[i];
                    i++;
                }
                else
                {
                    A[k] = R[j];
                    j++;
                }
                yield return new WaitForSeconds(delay);
                A.States[k] = 0;
            }
        }
    }
}
