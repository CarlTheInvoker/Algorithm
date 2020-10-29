namespace Algorithm.Sort
{
    using System;
    public static class MergeSortExtension
    {
        public static void MergeSort<TElement>(this TElement[] elements) where TElement : IComparable<TElement>
        {
            MergeSort(elements, new TElement[elements.Length], 0, elements.Length);
        }

        /// <summary>
        /// Sort elements[start, end)
        /// </summary>
        /// <typeparam name="TElement">Element type</typeparam>
        /// <param name="elements">The original elements</param>
        /// <param name="temp">A temp array used when merge</param>
        /// <param name="start">Start index</param>
        /// <param name="end">End index</param>
        private static void MergeSort<TElement>(TElement[] elements, TElement[] temp, int start, int end) where TElement : IComparable<TElement>
        {
            if (start == end - 1) return;
            int middle = (start + end) / 2;

            // Sort sub array
            MergeSort(elements, temp, start, middle);
            MergeSort(elements, temp, middle, end);

            // Merge sorted sub array and store the result in temp
            int left = start, right = middle;
            for (int i = 0; i < end - start; ++i)
            {
                if (right == end) temp[i] = elements[left++];
                else if (left == middle) temp[i] = elements[right++];
                else if (elements[left].CompareTo(elements[right]) <= 0) temp[i] = elements[left++];
                else temp[i] = elements[right++];
            }

            // Copy temp result back into original array
            for (int i = 0; i < end - start; ++i)
            {
                elements[start + i] = temp[i];
            }
        }
    }
}
