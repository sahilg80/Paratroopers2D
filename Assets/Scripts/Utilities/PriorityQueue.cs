using System;
using System.Collections.Generic;
using System.Linq;


namespace Assets.Scripts.Utilities
{
    public class PriorityQueue<T>
    {
        private List<T> heap;
        private IComparer<T> comparer;

        public int Count { get { return heap.Count; } }

        public PriorityQueue()
        {
            heap = new List<T>();
            comparer = Comparer<T>.Default;
        }

        public PriorityQueue(IComparer<T> comparer)
        {
            heap = new List<T>();
            this.comparer = comparer;
        }

        public void Enqueue(T item)
        {
            heap.Add(item);
            int i = Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (comparer.Compare(heap[parent], heap[i]) <= 0)
                    break;
                Swap(parent, i);
                i = parent;
            }
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T item = heap[0];
            heap[0] = heap[Count - 1];
            heap.RemoveAt(Count - 1);

            int i = 0;
            while (true)
            {
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                if (leftChild >= Count)
                    break;

                int minChild = (rightChild >= Count || comparer.Compare(heap[leftChild], heap[rightChild]) <= 0) ? leftChild : rightChild;

                if (comparer.Compare(heap[i], heap[minChild]) <= 0)
                    break;

                Swap(i, minChild);
                i = minChild;
            }

            return item;
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
