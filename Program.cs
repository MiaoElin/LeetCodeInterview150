﻿using System;
using System.Linq;

public class Program {
    public static void Main() {
        System.Console.WriteLine("Hello");
        Merge_1(new int[] { 2, 0 }, 1, new int[] { 1 }, 1);

        System.Console.WriteLine(RemoveDuplicates(new int[] { 1, 1, 1, 2, 2, 3 }, 2));
        Rotate_1(new int[] { -1, -100, 3, 99 }, 2);
        System.Console.WriteLine("股票赚了：" + MaxProfit_2(new int[] { 7, 1, 5, 3, 6, 4 }));

        AddBinary("1010", "1011");
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

    #region 删除有序数组中的重复项II
    // 最多可重复k项
    public static int RemoveDuplicates(int[] nums, int k) {
        // 长度在2以内，重复元素不会超过2;
        if (nums.Length <= k) {
            return nums.Length;
        }

        int left = k;
        int right = k;
        while (right < nums.Length) {
            // 当前是nums[left], 当前退两个位置是 left-2；找到跟left-2位置不一样的right才能覆盖当前，否者right++;
            if (nums[left - k] != nums[right]) {
                nums[left] = nums[right];
                left++;
            }
            right++;
        }
        return left;
    }
    #endregion

    #region 多数元素
    // 暴力拆解
    public static int MajorityElement(int[] nums) {
        float count = nums.Length / 2;
        Dictionary<int, int> all = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++) {
            var num = nums[i];
            if (all.ContainsKey(num)) {
                all[num]++;
                if (all[num] > count) {
                    return num;
                }
            } else {
                all.Add(num, 1);
            }
        }

        foreach (var value in all) {
            if (value.Value > count) {
                return value.Key;
            }
        }
        return default;
    }

    // 下标的中位数 因为这个多数会大于N/2 个，所以如果排序以后，这个数一定超过下标的中位数
    //  1 1 1 1 2 3 4  7个元素，1至少要4个 大于n/2个 
    //  0 1 2 3 4 5 6          下标至少是从3开始  下标是大于等于n/2(会向下取整)
    public static int MajorityElement_1(int[] nums) {

        Array.Sort(nums);
        int index = nums.Length / 2;
        return nums[index];

    }
    #endregion

    #region 轮转数组
    public static void Rotate(int[] nums, int k) {
        int[] temp = new int[nums.Length];
        nums.CopyTo(temp, 0);
        int start = temp.Length - k % temp.Length;
        for (int i = 0; i < temp.Length; i++) {
            nums[i] = temp[(start + i) % temp.Length];
        }
    }

    // 方法二，节省空间（o（1）)
    public static void Rotate_1(int[] nums, int k) {
        {
            k %= nums.Length; // 防止k大于数组长度的时候
            int groupCount = GCD(nums.Length, k);
            int groupSize = nums.Length / groupCount;

            for (int i = 0; i < groupCount; i++) {
                int curIndex = i;
                int curNum = nums[curIndex];
                for (int j = 0; j < groupSize; j++) {
                    int nextIndex = (curIndex + k) % nums.Length;
                    int temp = nums[nextIndex];
                    nums[nextIndex] = curNum;
                    curNum = temp;
                    curIndex = nextIndex;
                }
            }

            string s = "";

            foreach (var num in nums) {
                s += num + ", ";
            }
            System.Console.WriteLine(s);
        }
    }

    // 最大公约数（能分别被这两个数整除的最大数）
    public static int GCD(int a, int b) {
        if (b == 0) {
            return a;
        }
        if (a == 0) {
            return b;
        }
        return GCD(b, a % b);
    }

    #endregion

    #region 买卖股票的最佳时机
    public static int MaxProfit(int[] prices) {
        int res = 0;
        int minStart = int.MaxValue;
        for (int i = 0; i < prices.Length; i++) {
            if (prices[i] >= minStart) {
                continue;
            }
            for (int j = i + 1; j < prices.Length; j++) {
                if (prices[j] < prices[i]) {
                    continue;
                }
                if (prices[j] - prices[i] > res) {
                    res = prices[j] - prices[i];
                }
            }
            if (prices[i] < minStart) {
                minStart = prices[i];
            }
        }
        return res;
    }

    public static int MaxProfit_1(int[] prices) {
        int res = 0;
        int lowest = int.MaxValue;

        for (int i = 0; i < prices.Length; i++) {
            if (prices[i] < lowest) {
                lowest = prices[i];
            } else {
                res = Math.Max(res, prices[i] - lowest);
            }
        }
        return res;
    }

    #endregion

    #region 买卖股票的最佳时机II

    // 7, 1, 5, 3, 6, 4   只要比前一天高的都累加，
    public static int MaxProfit_2(int[] prices) {
        int res = 0;
        for (int i = 1; i < prices.Length; i++) {
            res += Math.Max(prices[i] - prices[i - 1], 0);
        }
        return res;
    }
    #endregion

    #region 单次规律
    public static bool WordPattern(string pattern, string s) {
        bool isTrue = false;
        return isTrue;
    }
    #endregion

    #region  二进制求和
    // 11
    public static string AddBinary(string a, string b) {
        if (a == "0" && b == "0") {
            return "0";
        }
        string s = "";
        int leave = 0;
        int index = 0;
        while (a.Length - index >= 0 || b.Length - index >= 0) {
            if (a.Length - 1 - index >= 0) {
                char cur = a[a.Length - 1 - index];
                if (cur == '0') {
                    leave += 0;
                } else {
                    leave += 1;
                }
            }
            if (b.Length - 1 - index >= 0) {
                char cur = b[b.Length - 1 - index];
                if (cur == '0') {
                    leave += 0;
                } else {
                    leave += 1;
                }
            }
            index++;
            if (leave == 0 && index > a.Length && index > b.Length) {
                break;
            }
            s += (leave % 2).ToString();
            leave /= 2;
        }
        string res = "";
        for (int i = s.Length - 1; i >= 0; i--) {
            res += s[i];
        }
        System.Console.WriteLine(res);
        return res;
    }
    #endregion










}