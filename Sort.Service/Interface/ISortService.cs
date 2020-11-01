using Sort.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Service.Interface
{
    public interface ISortService
    {
        public SortReport InsertionSort(int arraySize);
        public MultiSortReport MultiSort(int arraySize, int iterations, int sortType);
        public SortReport MergeSort(int arraySize);
        public SortReport QuickSort(int arraySize);
    }
}
