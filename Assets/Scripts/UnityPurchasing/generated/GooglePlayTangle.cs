// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ackl9MQdFNSF1Q0EQbNNjWan6rrhcDDRMobb58KrtwfbqoQV8FVTpg8R4nSwbDZbUHcRv+bohVsLNVSNqfV73tooEXzYely8zRJTs56Fgyl6kSD+k1YdY2whWB6FW4Mopsh2KSnZ9T8ORstSxxnGTiMstSztPJbNs9si3cKQP9AWBjwXjU1invq9PEa3NDo1Bbc0Pze3NDQ1rI4B4Yrj26wpQPYl1gih4gzR2CbwHioyBqwvBszFLolcaVIdt7jtAl/dVHc7XsGJ/npZ1Bc/9x2AbfVWtp5ctX/zkwW3NBcFODM8H7N9s8I4NDQ0MDU2HHLJagreeKjUBxyOZ+EFfoE/QelETcSqva1UTjVRRz4ZDxx1L2pPNtoKwFPcfXYCaDc2NDU0");
        private static int[] order = new int[] { 8,12,9,3,9,5,10,12,12,9,10,12,13,13,14 };
        private static int key = 53;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
