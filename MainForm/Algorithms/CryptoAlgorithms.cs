using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainForm.Algorithms
{
    public static class CryptoAlgorithms
    {
        private const string AlphabetTR = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        #region Helper Methods
        private static string CleanInput(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            // Sadece A-Z ve Türkçe karakterli harfleri tutar (Rakam, Simge, Boşluk temizler)
            string cleaned = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR"));
            return System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^A-ZÇĞİÖŞÜ]", "");
        }

        private static int GetIndex(char c)
        {
            return AlphabetTR.IndexOf(c);
        }

        private static char GetChar(int index)
        {
            while (index < 0) index += AlphabetTR.Length;
            return AlphabetTR[index % AlphabetTR.Length];
        }

        private static int Mod(int n, int m)
        {
            return ((n % m) + m) % m;
        }

        private static int ExtendedGCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            int x1, y1;
            int gcd = ExtendedGCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return gcd;
        }

        private static int ModInverse(int a, int m)
        {
            int x, y;
            int g = ExtendedGCD(a, m, out x, out y);
            if (g != 1) return -1;
            return Mod(x, m);
        }
        #endregion

        #region 1. Kaydırmalı (Caesar)
        public static string CaesarEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int shift)) shift = 3;
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else result.Append(GetChar(index + shift));
            }
            return result.ToString();
        }

        public static string CaesarDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (!int.TryParse(key, out int shift)) shift = 3;
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else result.Append(GetChar(index - shift));
            }
            return result.ToString();
        }
        #endregion

        #region 2. Doğrusal (Affine)
        public static string AffineEncrypt(string text, string key)
        {
            text = CleanInput(text);
            int a = 5, b = 8;
            var parts = key.Replace(" ", "").Split(',');
            if (parts.Length == 2)
            {
                int.TryParse(parts[0], out a);
                int.TryParse(parts[1], out b);
            }

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else
                {
                    int encryptedIndex = Mod(a * index + b, AlphabetTR.Length);
                    result.Append(GetChar(encryptedIndex));
                }
            }
            return result.ToString();
        }

        public static string AffineDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            int a = 5, b = 8;
            var parts = key.Replace(" ", "").Split(',');
            if (parts.Length == 2)
            {
                int.TryParse(parts[0], out a);
                int.TryParse(parts[1], out b);
            }

            int aInv = ModInverse(a, AlphabetTR.Length);
            if (aInv == -1) return "HATA: GECERSIZ ANAHTAR";

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else
                {
                    int decryptedIndex = Mod(aInv * (index - b), AlphabetTR.Length);
                    result.Append(GetChar(decryptedIndex));
                }
            }
            return result.ToString();
        }
        #endregion

        #region 3. Yer Değiştirme (Simple Substitution)
        public static string SubstitutionEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (string.IsNullOrEmpty(key) || key.Replace(" ", "").Length < 29) 
                key = "ZYVÜUTŞSRPÖONMLKJIİHĞG FEDÇCBA".Replace(" ", "");
            
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else result.Append(key[index]);
            }
            return result.ToString();
        }

        public static string SubstitutionDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (string.IsNullOrEmpty(key) || key.Replace(" ", "").Length < 29) 
                key = "ZYVÜUTŞSRPÖONMLKJIİHĞG FEDÇCBA".Replace(" ", "");

            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int indexInKey = key.IndexOf(c);
                if (indexInKey == -1) result.Append(c);
                else result.Append(AlphabetTR[indexInKey]);
            }
            return result.ToString();
        }
        #endregion

        #region 4. Sayı Anahtarlı (Vigenere)
        public static string VigenereEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (string.IsNullOrEmpty(key)) key = "ANAHTAR";
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else
                {
                    int shift = GetIndex(key[keyIndex % key.Length]);
                    if (shift == -1) shift = 0;
                    result.Append(GetChar(index + shift));
                    keyIndex++;
                }
            }
            return result.ToString();
        }

        public static string VigenereDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (string.IsNullOrEmpty(key)) key = "ANAHTAR";
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else
                {
                    int shift = GetIndex(key[keyIndex % key.Length]);
                    if (shift == -1) shift = 0;
                    result.Append(GetChar(index - shift));
                    keyIndex++;
                }
            }
            return result.ToString();
        }
        #endregion

        #region 5. Permütasyon (Permutation)
        public static string PermutationEncrypt(string text, string key)
        {
            text = CleanInput(text);
            try
            {
                var parts = key.Replace(" ", "").Split(',').Select(int.Parse).ToArray();
                int blockSize = parts.Length;
                StringBuilder result = new StringBuilder();
                
                for (int i = 0; i < text.Length; i += blockSize)
                {
                    char[] block = new char[blockSize];
                    for (int j = 0; j < blockSize; j++)
                    {
                        if (i + j < text.Length) block[j] = text[i + j];
                        else block[j] = 'X'; // Boşluk yerine dolgu karakteri (X)
                    }

                    char[] encryptedBlock = new char[blockSize];
                    for (int j = 0; j < blockSize; j++)
                    {
                        encryptedBlock[parts[j]] = block[j];
                    }
                    result.Append(new string(encryptedBlock));
                }
                return result.ToString();
            }
            catch { return "HATA: GECERSIZ ANAHTAR"; }
        }

        public static string PermutationDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            try
            {
                var parts = key.Replace(" ", "").Split(',').Select(int.Parse).ToArray();
                int blockSize = parts.Length;
                int[] revParts = new int[blockSize];
                for (int j = 0; j < blockSize; j++) revParts[parts[j]] = j;
                
                string revKey = string.Join(",", revParts);
                return PermutationEncrypt(text, revKey).Replace("X", ""); // Dolgu karakterlerini temizle
            }
            catch { return "HATA: GECERSIZ ANAHTAR"; }
        }
        #endregion

        #region 6. Rota (Route Transposition)
        public static string RouteEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int rows) || rows <= 0) rows = 3;
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            
            int k = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = k < text.Length ? text[k++] : 'X';

            StringBuilder result = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    result.Append(matrix[r, c]);

            return result.ToString();
        }

        public static string RouteDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (!int.TryParse(key, out int rows) || rows <= 0) rows = 3;
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];

            int k = 0;
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    if (k < text.Length) matrix[r, c] = text[k++];

            StringBuilder result = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    result.Append(matrix[r, c]);

            return result.ToString().Replace("X", "");
        }
        #endregion

        #region 7. Zigzag (Rail Fence)
        public static string RailFenceEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int rails) || rails <= 1) rails = 2;
            List<char>[] fence = new List<char>[rails];
            for (int i = 0; i < rails; i++) fence[i] = new List<char>();

            int rail = 0;
            int direction = 1;
            foreach (char c in text)
            {
                fence[rail].Add(c);
                rail += direction;
                if (rail == rails - 1 || rail == 0) direction *= -1;
            }

            StringBuilder result = new StringBuilder();
            foreach (var r in fence) result.Append(new string(r.ToArray()));
            return result.ToString();
        }

        public static string RailFenceDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (!int.TryParse(key, out int rails) || rails <= 1) rails = 2;
            int n = text.Length;
            char[] result = new char[n];
            int[] posGird = new int[n];
            
            int rail = 0;
            int direction = 1;
            for (int i = 0; i < n; i++)
            {
                posGird[i] = rail;
                rail += direction;
                if (rail == rails - 1 || rail == 0) direction *= -1;
            }

            int k = 0;
            for (int r = 0; r < rails; r++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (posGird[i] == r && k < n)
                        result[i] = text[k++];
                }
            }
            return new string(result);
        }
        #endregion
        #region 8. 4 Kare (Four-Square)
        // Bu algoritma iki farklı anahtar kelime gerektirir: "ANAHTAR1,ANAHTAR2" formatında
        public static string FourSquareEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (text.Length % 2 != 0) text += "X";

            string key1 = "ANAHTAR";
            string key2 = "BILGI";
            var parts = key.Split(',');
            if (parts.Length == 2) { key1 = CleanInput(parts[0]); key2 = CleanInput(parts[1]); }

            string square1 = CreateSquare(key1);
            string square2 = CreateSquare(key2);
            string normalSquare = AlphabetTR; // 29 karakterli olduğu için doğrudan alfabe

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = text[i + 1];

                int r1 = AlphabetTR.IndexOf(a) / 5; // Not: 29 karakter 5x6'lık matris gerektirir
                int c1 = AlphabetTR.IndexOf(a) % 5;
                int r2 = AlphabetTR.IndexOf(b) / 5;
                int c2 = AlphabetTR.IndexOf(b) % 5;

                // 4 Kare mantığı: (r1, c2) ve (r2, c1) çapraz eşleşme
                // r1,c2 -> square1 | r2,c1 -> square2
                result.Append(square1[Mod(r1 * 5 + c2, 29)]);
                result.Append(square2[Mod(r2 * 5 + c1, 29)]);
            }
            return result.ToString();
        }

        private static string CreateSquare(string key)
        {
            string combined = key + AlphabetTR;
            return new string(combined.Distinct().ToArray());
        }

        public static string FourSquareDecrypt(string text, string key)
        {
            text = CleanInput(text);
            string key1 = "ANAHTAR";
            string key2 = "BILGI";
            var parts = key.Split(',');
            if (parts.Length == 2) { key1 = CleanInput(parts[0]); key2 = CleanInput(parts[1]); }

            string square1 = CreateSquare(key1);
            string square2 = CreateSquare(key2);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int r1 = square1.IndexOf(text[i]) / 5;
                int c2 = square1.IndexOf(text[i]) % 5;
                int r2 = square2.IndexOf(text[i + 1]) / 5;
                int c1 = square2.IndexOf(text[i + 1]) % 5;

                result.Append(AlphabetTR[Mod(r1 * 5 + c1, 29)]);
                result.Append(AlphabetTR[Mod(r2 * 5 + c2, 29)]);
            }
            return result.ToString();
        }
        #endregion

        #region 9. Hill Şifreleme (2x2 Matris)
        // Key formatı: "a,b,c,d" (Örn: "3,3,2,5") -> |a b|
        //                                            |c d|
        public static string HillEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (text.Length % 2 != 0) text += "X";

            int[] k = key.Split(',').Select(int.Parse).ToArray();
            if (k.Length < 4) k = new int[] { 3, 3, 2, 5 };

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int p1 = GetIndex(text[i]);
                int p2 = GetIndex(text[i + 1]);

                result.Append(GetChar((k[0] * p1 + k[1] * p2) % 29));
                result.Append(GetChar((k[2] * p1 + k[3] * p2) % 29));
            }
            return result.ToString();
        }

        public static string HillDecrypt(string text, string key)
        {
            text = CleanInput(text);
            int[] k = key.Split(',').Select(int.Parse).ToArray();
            if (k.Length < 4) k = new int[] { 3, 3, 2, 5 };

            // Determinant: (ad - bc)
            int det = Mod(k[0] * k[3] - k[1] * k[2], 29);
            int invDet = ModInverse(det, 29);

            if (invDet == -1) return "HATA: MATRISIN TERSI YOK (DET=0 VEYA ORTAK BOLEN VAR)";

            // Ters Matris: invDet * | d  -b|
            //                      |-c   a|
            int[] invK = new int[4];
            invK[0] = Mod(invDet * k[3], 29);
            invK[1] = Mod(invDet * -k[1], 29);
            invK[2] = Mod(invDet * -k[2], 29);
            invK[3] = Mod(invDet * k[0], 29);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int c1 = GetIndex(text[i]);
                int c2 = GetIndex(text[i + 1]);

                result.Append(GetChar((invK[0] * c1 + invK[1] * c2) % 29));
                result.Append(GetChar((invK[2] * c1 + invK[3] * c2) % 29));
            }
            return result.ToString();
        }
        #endregion
    }
}
