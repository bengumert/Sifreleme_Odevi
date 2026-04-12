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

        #region Sayı Anahtarlı (Columnar Transposition)
        public static string NumericKeyEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int cols) || cols <= 0) cols = 3;
            int rows = (int)Math.Ceiling((double)text.Length / cols);
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

        public static string NumericKeyDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (!int.TryParse(key, out int cols) || cols <= 0) cols = 3;
            int rows = (int)Math.Ceiling((double)text.Length / cols);
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
            int top = 0, bottom = rows - 1, left = 0, right = cols - 1;
            while (top <= bottom && left <= right)
            {
                for (int i = bottom; i >= top; i--) result.Append(matrix[i, left]);
                left++;

                for (int i = left; i <= right; i++) result.Append(matrix[top, i]);
                top++;

                if (left <= right)
                {
                    for (int i = top; i <= bottom; i++) result.Append(matrix[i, right]);
                    right--;
                }

                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--) result.Append(matrix[bottom, i]);
                    bottom--;
                }
            }

            return result.ToString();
        }

        public static string RouteDecrypt(string text, string key)
        {
            text = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            if (!int.TryParse(key, out int rows) || rows <= 0) rows = 3;
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];

            int top = 0, bottom = rows - 1, left = 0, right = cols - 1;
            int k = 0;
            while (top <= bottom && left <= right && k < text.Length)
            {
                for (int i = bottom; i >= top; i--) if (k < text.Length) matrix[i, left] = text[k++];
                left++;

                for (int i = left; i <= right; i++) if (k < text.Length) matrix[top, i] = text[k++];
                top++;

                if (left <= right)
                {
                    for (int i = top; i <= bottom; i++) if (k < text.Length) matrix[i, right] = text[k++];
                    right--;
                }

                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--) if (k < text.Length) matrix[bottom, i] = text[k++];
                    bottom--;
                }
            }

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
            if (text.Length % 2 != 0) text += "X"; // Tek sayılıysa doldur

            string key1 = CleanInput(key);
            if (string.IsNullOrWhiteSpace(key1)) key1 = "ANAHTAR";
            string key2 = new string(key1.Reverse().ToArray()); // Çift kelime yerine aynı anahtarın tersini kullanıyoruz

            string normalSquare = AlphabetTR + "X"; // 30 karakter, 5x6 matris için
            string square1 = CreateSquare(key1);    // M2 (Sağ Üst)
            string square2 = CreateSquare(key2);    // M3 (Sol Alt)

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = text[i + 1];

                int indexA = normalSquare.IndexOf(a);
                int indexB = normalSquare.IndexOf(b);
                if (indexA == -1) indexA = 29;
                if (indexB == -1) indexB = 29;

                int r1 = indexA / 6; // M1 satır
                int c1 = indexA % 6; // M1 sütun
                int r2 = indexB / 6; // M4 satır
                int c2 = indexB % 6; // M4 sütun

                // Kesişim: r1,c2 -> M2 (Sağ Üst), r2,c1 -> M3 (Sol Alt)
                result.Append(square1[r1 * 6 + c2]);
                result.Append(square2[r2 * 6 + c1]);
            }
            return result.ToString();
        }

        private static string CreateSquare(string key)
        {
            string combined = key + AlphabetTR + "X";
            return new string(combined.Distinct().ToArray());
        }

        public static string FourSquareDecrypt(string text, string key)
        {
            text = CleanInput(text);
            if (text.Length % 2 != 0) text += "X";

            string key1 = CleanInput(key);
            if (string.IsNullOrWhiteSpace(key1)) key1 = "ANAHTAR";
            string key2 = new string(key1.Reverse().ToArray());

            string normalSquare = AlphabetTR + "X";
            string square1 = CreateSquare(key1); // M2
            string square2 = CreateSquare(key2); // M3

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = text[i + 1];

                int indexA = square1.IndexOf(a);
                int indexB = square2.IndexOf(b);
                if (indexA == -1) indexA = 29;
                if (indexB == -1) indexB = 29;

                int r1 = indexA / 6; // M2 satır
                int c2 = indexA % 6; // M2 sütun
                int r2 = indexB / 6; // M3 satır
                int c1 = indexB % 6; // M3 sütun

                result.Append(normalSquare[r1 * 6 + c1]);
                result.Append(normalSquare[r2 * 6 + c2]);
            }
            // Şifreleme sonrası dolgu amaçlı eklenen 'X' leri temizleyebiliriz
            return result.ToString().TrimEnd('X');
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

        #region 10. DSA Dijital İmza
        private static System.Security.Cryptography.DSAParameters? _dsaKeyParams = null;

        public static string DSAEncrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            try
            {
                using (var dsa = System.Security.Cryptography.DSA.Create())
                {
                    if (_dsaKeyParams == null)
                    {
                        _dsaKeyParams = dsa.ExportParameters(true);
                    }
                    dsa.ImportParameters(_dsaKeyParams.Value);

                    byte[] data = Encoding.UTF8.GetBytes(text);
                    byte[] signature = dsa.SignData(data, System.Security.Cryptography.HashAlgorithmName.SHA256);

                    return Convert.ToBase64String(signature) + "|||" + text;
                }
            }
            catch (Exception ex)
            {
                return "HATA: DSA İMZA OLUŞTURULAMADI - " + ex.Message;
            }
        }

        public static string DSADecrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            try
            {
                var parts = text.Split(new string[] { "|||" }, StringSplitOptions.None);
                if (parts.Length != 2) return "HATA: GEÇERSİZ DSA VERİSİ (Format: İmza|||Metin)";

                string sigBase64 = parts[0];
                string originalText = parts[1];

                using (var dsa = System.Security.Cryptography.DSA.Create())
                {
                    if (_dsaKeyParams == null) return "HATA: SİSTEMDE DSA ANAHTARI YOK (Önce Imzalama yapın veya uygulama baştan başladıysa anahtar sıfırlanmıştır)";
                    
                    dsa.ImportParameters(_dsaKeyParams.Value);

                    byte[] data = Encoding.UTF8.GetBytes(originalText);
                    byte[] signature = Convert.FromBase64String(sigBase64);

                    bool isValid = dsa.VerifyData(data, signature, System.Security.Cryptography.HashAlgorithmName.SHA256);

                    if (isValid)
                        return "[DOĞRULANDI] Başarılı! Metin: " + originalText;
                    else
                        return "[HATA] İMZA DOĞRULANAMADI! VERİ DEĞİŞTİRİLMİŞ VEYA ANAHTAR FARKLI OLABİLİR.";
                }
            }
            catch (Exception ex)
            {
                return "HATA: DSA ÇÖZÜMLEME HATASI - " + ex.Message;
            }
        }
        #endregion
    }
}
