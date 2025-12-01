namespace GameTool
{
    public static class NetHelper
    {
        public static float BytesToMB(long bytes)
        {
            return bytes / (1024f * 1024f);
        }
    }
}