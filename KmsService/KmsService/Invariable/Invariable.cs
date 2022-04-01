/*
 * 创建人：王梦杰
 * 创建日期：2022年1月12日19:45:39
 * 描述：常用字段封装
 */
namespace KmsService
{
    /// <summary>
    /// 常用字段封装类
    /// </summary>
    public static class Invariable
    {
        private static int zero = 0;

        public static int Zero
        {
            get { return zero; }
        }

        private static int one = 1;

        public static int One
        {
            get { return one; }
        }
    }
}