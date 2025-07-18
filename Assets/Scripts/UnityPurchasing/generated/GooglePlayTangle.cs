#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("kVAVfELRZQn1Tp6PW2DjgcnPumy3eXKox6en4sUff3eecwPaBs8WKk/DPCdo98+9Ekp+kgQaAs1bpGgMuYkZFghXfCuQvTFCnDAJP4MCK+VCvWqrgyj3SIQrfKNgSQ1awKp4ZUL04weKPxtExSAipVVJEw+YOKILgDKxkoC9trmaNvg2R72xsbG1sLPanFpJyLMIhelWXIHw14ij/dSjvvjajp70CLc1cpySLnccjfLHMC/sOAUg72lO/SYN76gXvv3Mn9rvfZgysb+wgDKxurIysbGwGILKDdABBmO0M0nBimh8kvJP2IwSWKTU2D8LeCKgOUQYa/ms+TWBDtIvZipIqdg+Ra5lD0aS9lXUOZPQZiwUXWoL62iks/Z5fVUcmbKzsbCx");
        private static int[] order = new int[] { 3,10,6,4,7,13,7,12,9,13,10,11,12,13,14 };
        private static int key = 176;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
