using System;

public class Program {
    public static void Main() {
        System.Console.WriteLine("Hello");
        Merge_1(new int[] { 2, 0 }, 1, new int[] { 1 }, 1);
    }
    #region 合并两个有序数组
    public static void Merge(int[] nums1, int m, int[] nums2, int n) {
        int mIndex = 0;
        int nIndex = 0;
        int[] nums = new int[m + n];
        int index = 0;
        while (index < m + n) {
            if (mIndex >= m) {
                nums[index] = nums2[nIndex];
                index++;
                nIndex++;
                continue;
            } else if (nIndex >= n) {
                nums[index] = nums1[mIndex];
                index++;
                mIndex++;
                continue;
            }
            if (nums1[mIndex] > nums2[nIndex]) {
                while (mIndex < m && nIndex < n && nums1[mIndex] > nums2[nIndex]) {
                    nums[index++] = nums2[nIndex++];
                }
            } else {
                while (mIndex < m && nIndex < n && nums1[mIndex] <= nums2[nIndex]) {
                    nums[index++] = nums1[mIndex++];
                }
            }
        }

        for (int i = 0; i < nums1.Length; i++) {
            nums1[i] = nums[i];
        }

        string s = "";
        foreach (var num in nums1) {
            s += num + ", ";
        }

        System.Console.WriteLine(s);
    }

    // 首尾拼接 整体排序 最简单暴力
    public static void Merge_1(int[] nums1, int m, int[] nums2, int n) {
        for (int i = 0; i < n; i++) {
            nums1[m + i] = nums2[i];
        }
        Array.Sort(nums1);
        string s = "";

        foreach (var num in nums1) {
            s += num + ", ";
        }
        System.Console.WriteLine(s);
    }

    // 双指针
    public static void Merge_2(int[] nums1, int m, int[] nums2, int n) {
        int mIndex = 0;
        int nIndex = 0;
        int index = 0;
        int[] res = new int[m + n];
        while (mIndex < m || nIndex < n) {
            if (mIndex == m) {
                res[index++] = nums2[nIndex++];
            } else if (nIndex == n) {
                res[index++] = nums1[mIndex++];
            } else if (nums1[mIndex] > nums2[nIndex]) {
                res[index++] = nums2[nIndex++];
            } else if (nums1[mIndex] <= nums2[nIndex]) {
                res[index++] = nums1[mIndex++];
            }
        }

        for (int i = 0; i < nums1.Length; i++) {
            nums1[i] = res[i];
        }

    }

    #endregion
}