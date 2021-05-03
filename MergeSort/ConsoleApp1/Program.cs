namespace ConsoleApp1
{
    internal class Solution
    {
        private static void Main(string[] args)
        {
            var input = new[] {10, 9, 8, 7, 6, 5, 4, 3, 2, 1};
            MergeSort(input, 0, input.Length - 1);
        }

        public static void Merge(int[] arr, int l, int m, int r)
        {
            //l is zero based and is inclusive
            //r is zero based and is inclusive

            var temp = new int[r - l + 1];
            var leftArray = l;
            var middle = m;
            var rightArray = middle + 1;
            var tempArrayIndex = 0;
            while (leftArray <= middle && rightArray <= r)
                //swap logic here
                if (arr[leftArray] <= arr[rightArray])
                {
                    temp[tempArrayIndex] = arr[leftArray];
                    tempArrayIndex++;
                    leftArray++;
                }
                else
                {
                    temp[tempArrayIndex] = arr[rightArray];
                    tempArrayIndex++;
                    rightArray++;
                }

            while (leftArray <= middle)
            {
                temp[tempArrayIndex] = arr[leftArray];
                tempArrayIndex++;
                leftArray++;
            }

            while (rightArray <= r)
            {
                temp[tempArrayIndex] = arr[rightArray];
                tempArrayIndex++;
                rightArray++;
            }

            for (var i = 0; i < temp.Length; i++)
            {
                var value = temp[i];
                arr[i + l] = value;
            }
        }

        public static void MergeSort(int[] arr, int l, int r)
        {
            //r is zero based
            //l is zero based
            //code here
            if (l < r)
            {
                var m = l + (r - l) / 2;

                MergeSort(arr, l, m);
                MergeSort(arr, m + 1, r);
                Merge(arr, l, m, r);
            }
        }
    }
}