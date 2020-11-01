using Sort.Entities.Models;
using Sort.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Sort.Service
{
    public class SortService : ISortService
    {
        public SortService()
        {

        }

        #region InsertionSort
        public SortReport InsertionSort(int arraySize)
        {
            int[] array = SetArray(arraySize);
            int[] unsortedArray = new int[arraySize];
            array.CopyTo(unsortedArray,0);

            var startTime = DateTime.Now;
            for (int j = 1; j < array.Length; j++)
            {
                var key = array[j];
                var i = (j - 1);
                while (i >= 0 && array[i] > key)
                {
                    array[(i + 1)] = array[i];
                    i -= 1; 
                }
                array[i + 1] = key;
            }
            var endTime = DateTime.Now;

            return new SortReport()
            {
                ArraySize = arraySize,
                SortType = EnumSortType.InsertSort.ToString(),
                SortedArray = array,
                UnsortedArray = unsortedArray,
                SecondsElapsed = (endTime - startTime).TotalSeconds,
                MillisecondsElapsed = (endTime - startTime).TotalMilliseconds
            };
        }
        #endregion

        #region MergeSort
        public SortReport MergeSort(int arraySize)
        {
            int[] array = SetArray(arraySize);
            int[] unsortedArray = new int[arraySize];
            array.CopyTo(unsortedArray, 0);

            var startTime = DateTime.Now;
            Merge(ref array, 0, (arraySize - 1));
            var endTime = DateTime.Now;

            return new SortReport()
            {
                ArraySize = arraySize,
                SortType = EnumSortType.MergeSort.ToString(),
                SortedArray = array,
                UnsortedArray = unsortedArray,
                SecondsElapsed = (endTime - startTime).TotalSeconds,
                MillisecondsElapsed = (endTime - startTime).TotalMilliseconds
            };
        }

        private static void Merge(ref int[] array, int firstPosition, int lastPosition)
        {
            if (firstPosition < lastPosition)
            {
                var middlePosition = (firstPosition + lastPosition) / 2;
                Merge(ref array, firstPosition, middlePosition);
                Merge(ref array, (middlePosition + 1), lastPosition);
                Sort(ref array, firstPosition, middlePosition, lastPosition);
            }
        }

        private static void Sort(ref int[] array, int firstPosition, int middlePosition, int lastPosition)
        {
            var leftSize = middlePosition - firstPosition + 1;
            var rightSize = lastPosition - middlePosition;
            
            var left = new int[leftSize + 1];
            var right = new int[rightSize + 1];
            
            left[leftSize] = int.MaxValue;
            right[rightSize] = int.MaxValue;

            int leftIndex;
            int rightIndex;

            for (leftIndex = 0; leftIndex < leftSize; leftIndex++)
            {
                left[leftIndex] = array[firstPosition + leftIndex];
            }

            for (rightIndex = 0; rightIndex < rightSize; rightIndex++)
            {
                right[rightIndex] = array[middlePosition + rightIndex + 1];
            }

            leftIndex = 0;
            rightIndex = 0;

            for (var i = firstPosition; i <= lastPosition; i++)
            {
                if (left[leftIndex] < right[rightIndex])
                {
                    array[i] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    array[i] = right[rightIndex];
                    rightIndex++;
                }
            }
        }
        #endregion

        #region QuickSort
        public SortReport QuickSort(int arraySize)
        {
            int[] array = SetArray(arraySize);
            int[] unsortedArray = new int[arraySize];
            array.CopyTo(unsortedArray, 0);

            var startTime = DateTime.Now;
            Sort(ref array, 0, (array.Length - 1));
            var endTime = DateTime.Now;

            return new SortReport()
            {
                ArraySize = arraySize,
                SortType = EnumSortType.QuickSort.ToString(),
                SortedArray = array,
                UnsortedArray = unsortedArray,
                SecondsElapsed = (endTime - startTime).TotalSeconds,
                MillisecondsElapsed = (endTime - startTime).TotalMilliseconds
            };
        }

        private static void Sort(ref int[] array, int firstPosition, int lastPosition)
        {
            if(firstPosition < lastPosition)
            {
                int pivot = SetPivot(ref array, firstPosition, lastPosition);
                Sort(ref array, firstPosition, (pivot - 1));
                Sort(ref array, (pivot +1), lastPosition);
            }
        } 

        private static int SetPivot(ref int[] array, int firstPosition, int lastPosition)
        {
            var left = (firstPosition + 1);
            var right = lastPosition;
            var pivot = array[firstPosition];
            
            while (left <= right)
            {
                if (array[left] <= pivot)
                    left++;
                else if (array[right] > pivot)
                    right--;
                else if (left <= right)
                {
                    Exchange(ref array, left, right);
                    left++;
                    right--;
                }
            }
            Exchange(ref array, firstPosition, right);
            return right;
        }

        private static void Exchange(ref int[] array, int left, int right)
        {
            var aux = array[left];
            array[left] = array[right];
            array[right] = aux;
        }
        #endregion

        #region MultiSort
        public MultiSortReport MultiSort(int arraySize, int iterations, int sortType)
        {
            List<SortReport> sortReportList = new List<SortReport>();
            switch (sortType)
            {
                case 1:
                    for (int count = 0; count < iterations; count++)
                    {
                        sortReportList.Add(InsertionSort(arraySize));
                    }
                    break;
                case 2:
                    for (int count = 0; count < iterations; count++)
                    {
                        sortReportList.Add(MergeSort(arraySize));
                    }
                    break;
                case 3:
                    for (int count = 0; count < iterations; count++)
                    {
                        sortReportList.Add(QuickSort(arraySize));
                    }
                    break;
            }

            return new MultiSortReport()
            {
                BestIteration = sortReportList.OrderBy(x => x.MillisecondsElapsed).FirstOrDefault(),
                MediumIteration = SetMediumIteration(sortReportList, sortType),
                WorstIteration = sortReportList.OrderByDescending(x => x.MillisecondsElapsed).FirstOrDefault()
            };
        }
        #endregion

        #region Util
        private static SortReport SetMediumIteration(List<SortReport> sortReportList, int sortType)
        {
            return new SortReport()
            {
                ArraySize = (sortReportList.Sum(x => x.ArraySize) / sortReportList.Count()),
                SortType = ((EnumSortType)sortType).ToString(),
                SecondsElapsed = (sortReportList.Sum(x => x.SecondsElapsed) / sortReportList.Count()),
                MillisecondsElapsed = (sortReportList.Sum(x => x.MillisecondsElapsed) / sortReportList.Count())
            };
        }

        private static int[] SetArray(int arraySize)
        {
            int[] array = new int[arraySize];
            Random randomNumber = new Random();
            var multiplier = arraySize > 1000 ? 10 : 100;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = randomNumber.Next(1, (arraySize * multiplier));
            }

            return array;
        }
        #endregion
    }
}
