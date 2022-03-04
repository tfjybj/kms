namespace KmsService
{
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