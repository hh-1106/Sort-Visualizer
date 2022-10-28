using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : ISortAlgorithm
{
    public IEnumerator Sort(ArrayVisual A, float delay)
    {

        yield return randomizedQuickSort(0, A.n - 1);

        // 对子数组A[p..r]原址重排
        IEnumerator partition(int p, int r, System.Action<int> callback)
        {
            int x = A[r];      // pivot element
            int i = p;

            for (int j = p; j < r; j++)
            {
                A.States[j] = 2;
                A.States[i] = 1;
                if (A[j] <= x)
                {
                    A.States[i] = 1;
                    A.Swap(i, j);
                    yield return new WaitForSeconds(delay);
                    A.States[i] = 0;
                    i++;
                }
                A.States[j] = 0;
            }
            A.States[r] = 0;
            A.Swap(i, r);

            callback(i);
        }

        // 随机抽样
        IEnumerator randomizedPartition(int p, int r, System.Action<int> callback)
        {
            // 将A[r] 与从A[p..r]中随机选出的一个元素交换, 以保证主元是随机选取的
            // 我们期望在平均情况下对输入数组的划分是比较均衡的
            int i = (int)Mathf.Floor(Random.Range(p, r + 1));
            A.Swap(i, r);

            //? https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html?childToView=971941#answer-971941
            int q = 0;
            yield return partition(p, r, (value) =>
            {
                q = value;
            });
            callback(q);
        }

        IEnumerator randomizedQuickSort(int p, int r)
        {
            if (p < r)
            {
                int q = 0;
                yield return randomizedPartition(p, r, (value) =>
                {
                    q = value;
                });

                A.States[q] = 3;
                yield return randomizedQuickSort(p, q - 1);
                yield return randomizedQuickSort(q + 1, r);
                A.States[q] = 0;
            }
        }
    }
}
