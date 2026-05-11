using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MainForm.Algorithms
{
    public static class CryptoAlgorithms
    {
        private const string AlphabetTR = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        // --- Statik Anahtar Parametreleri ---
        private static RSAParameters _rsaParams;
        private static DSAParameters? _dsaParams = null;

        #region Helper Methods
        private static string CleanInput(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string cleaned = text.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR"));
            return System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^A-ZÇĞİÖŞÜ]", "");
        }

        private static int GetIndex(char c) => AlphabetTR.IndexOf(c);

        private static char GetChar(int index)
        {
            while (index < 0) index += AlphabetTR.Length;
            return AlphabetTR[index % AlphabetTR.Length];
        }

        private static int Mod(int n, int m) => ((n % m) + m) % m;

        private static int ExtendedGCD(int a, int b, out int x, out int y)
        {
            if (a == 0) { x = 0; y = 1; return b; }
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
            if (!int.TryParse(key, out int shift)) shift = 3;
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
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
            if (parts.Length == 2) { int.TryParse(parts[0], out a); int.TryParse(parts[1], out b); }
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else result.Append(GetChar(Mod(a * index + b, AlphabetTR.Length)));
            }
            return result.ToString();
        }

        public static string AffineDecrypt(string text, string key)
        {
            int a = 5, b = 8;
            var parts = key.Replace(" ", "").Split(',');
            if (parts.Length == 2) { int.TryParse(parts[0], out a); int.TryParse(parts[1], out b); }
            int aInv = ModInverse(a, AlphabetTR.Length);
            if (aInv == -1) return "HATA: GECERSIZ ANAHTAR";
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
            {
                int index = GetIndex(c);
                if (index == -1) result.Append(c);
                else result.Append(GetChar(Mod(aInv * (index - b), AlphabetTR.Length)));
            }
            return result.ToString();
        }
        #endregion

        #region 3. Yer Değiştirme (Simple Substitution)
        public static string SubstitutionEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (string.IsNullOrEmpty(key) || key.Replace(" ", "").Length < 29) key = "ZYVÜUTŞSRPÖONMLKJIİHĞG FEDÇCBA".Replace(" ", "");
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
            if (string.IsNullOrEmpty(key) || key.Replace(" ", "").Length < 29) key = "ZYVÜUTŞSRPÖONMLKJIİHĞG FEDÇCBA".Replace(" ", "");
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            foreach (char c in text.ToUpper())
            {
                int indexInKey = key.IndexOf(c);
                if (indexInKey == -1) result.Append(c);
                else result.Append(AlphabetTR[indexInKey]);
            }
            return result.ToString();
        }
        #endregion

        #region 4. Vigenere
        public static string VigenereEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (string.IsNullOrEmpty(key)) key = "ANAHTAR";
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            int kIdx = 0;
            foreach (char c in text)
            {
                int index = GetIndex(c);
                if (index != -1) { result.Append(GetChar(index + GetIndex(key[kIdx % key.Length]))); kIdx++; }
                else result.Append(c);
            }
            return result.ToString();
        }

        public static string VigenereDecrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(key)) key = "ANAHTAR";
            key = key.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr-TR")).Replace(" ", "");
            StringBuilder result = new StringBuilder();
            int kIdx = 0;
            foreach (char c in text.ToUpper())
            {
                int index = GetIndex(c);
                if (index != -1) { result.Append(GetChar(index - GetIndex(key[kIdx % key.Length]))); kIdx++; }
                else result.Append(c);
            }
            return result.ToString();
        }
        #endregion

        #region 5. Permütasyon
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
                    for (int j = 0; j < blockSize; j++) block[j] = (i + j < text.Length) ? text[i + j] : 'X';
                    char[] encBlock = new char[blockSize];
                    for (int j = 0; j < blockSize; j++) encBlock[parts[j]] = block[j];
                    result.Append(new string(encBlock));
                }
                return result.ToString();
            }
            catch { return "HATA: GECERSIZ ANAHTAR"; }
        }

        public static string PermutationDecrypt(string text, string key)
        {
            try
            {
                var parts = key.Replace(" ", "").Split(',').Select(int.Parse).ToArray();
                int[] revParts = new int[parts.Length];
                for (int j = 0; j < parts.Length; j++) revParts[parts[j]] = j;
                return PermutationEncrypt(text, string.Join(",", revParts)).Replace("X", "");
            }
            catch { return "HATA: GECERSIZ ANAHTAR"; }
        }
        #endregion

        #region 6. Sayı Anahtarlı (Columnar)
        public static string NumericKeyEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int cols) || cols <= 0) cols = 3;
            int rows = (int)Math.Ceiling((double)text.Length / cols);
            char[,] matrix = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++) matrix[r, c] = k < text.Length ? text[k++] : 'X';
            StringBuilder result = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++) result.Append(matrix[r, c]);
            return result.ToString();
        }

        public static string NumericKeyDecrypt(string text, string key)
        {
            if (!int.TryParse(key, out int cols) || cols <= 0) cols = 3;
            int rows = (int)Math.Ceiling((double)text.Length / cols);
            char[,] matrix = new char[rows, cols];
            int k = 0;
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++) if (k < text.Length) matrix[r, c] = text[k++];
            StringBuilder result = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++) result.Append(matrix[r, c]);
            return result.ToString().Replace("X", "");
        }
        #endregion

        #region 7. Rota & Zigzag (Rail Fence)
        public static string RouteEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int rows) || rows <= 0) rows = 3;
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++) matrix[r, c] = k < text.Length ? text[k++] : 'X';
            StringBuilder res = new StringBuilder();
            int top = 0, bottom = rows - 1, left = 0, right = cols - 1;
            while (top <= bottom && left <= right)
            {
                for (int i = bottom; i >= top; i--) res.Append(matrix[i, left]); left++;
                for (int i = left; i <= right; i++) res.Append(matrix[top, i]); top++;
                if (left <= right) { for (int i = top; i <= bottom; i++) res.Append(matrix[i, right]); right--; }
                if (top <= bottom) { for (int i = right; i >= left; i--) res.Append(matrix[bottom, i]); bottom--; }
            }
            return res.ToString();
        }

        public static string RouteDecrypt(string text, string key)
        {
            if (!int.TryParse(key, out int rows) || rows <= 0) rows = 3;
            int cols = (int)Math.Ceiling((double)text.Length / rows);
            char[,] matrix = new char[rows, cols];
            int top = 0, bottom = rows - 1, left = 0, right = cols - 1, k = 0;
            while (top <= bottom && left <= right && k < text.Length)
            {
                for (int i = bottom; i >= top; i--) if (k < text.Length) matrix[i, left] = text[k++]; left++;
                for (int i = left; i <= right; i++) if (k < text.Length) matrix[top, i] = text[k++]; top++;
                if (left <= right) { for (int i = top; i <= bottom; i++) if (k < text.Length) matrix[i, right] = text[k++]; right--; }
                if (top <= bottom) { for (int i = right; i >= left; i--) if (k < text.Length) matrix[bottom, i] = text[k++]; bottom--; }
            }
            StringBuilder res = new StringBuilder();
            for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) res.Append(matrix[r, c]);
            return res.ToString().Replace("X", "");
        }

        public static string RailFenceEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (!int.TryParse(key, out int rails) || rails <= 1) rails = 2;
            List<char>[] fence = new List<char>[rails];
            for (int i = 0; i < rails; i++) fence[i] = new List<char>();
            int rail = 0, dir = 1;
            foreach (char c in text) { fence[rail].Add(c); rail += dir; if (rail == rails - 1 || rail == 0) dir *= -1; }
            StringBuilder res = new StringBuilder();
            foreach (var r in fence) res.Append(new string(r.ToArray()));
            return res.ToString();
        }

        public static string RailFenceDecrypt(string text, string key)
        {
            if (!int.TryParse(key, out int rails) || rails <= 1) rails = 2;
            int n = text.Length, rail = 0, dir = 1, k = 0;
            int[] pos = new int[n]; char[] res = new char[n];
            for (int i = 0; i < n; i++) { pos[i] = rail; rail += dir; if (rail == rails - 1 || rail == 0) dir *= -1; }
            for (int r = 0; r < rails; r++) for (int i = 0; i < n; i++) if (pos[i] == r && k < n) res[i] = text[k++];
            return new string(res);
        }
        #endregion

        #region 8. 4 Kare (Four-Square)
        public static string FourSquareEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (text.Length % 2 != 0) text += "X";
            string k1, k2;
            if (key.Contains(",")) { var ks = key.Split(','); k1 = CleanInput(ks[0]); k2 = CleanInput(ks[1]); }
            else { k1 = CleanInput(key); if (string.IsNullOrWhiteSpace(k1)) k1 = "ANAHTAR"; k2 = new string(k1.Reverse().ToArray()); }
            string norm = AlphabetTR + "X", s1 = CreateSquare(k1), s2 = CreateSquare(k2);
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int idx1 = norm.IndexOf(text[i]), idx2 = norm.IndexOf(text[i + 1]);
                if (idx1 == -1) idx1 = 29; if (idx2 == -1) idx2 = 29;
                res.Append(s1[(idx1 / 6) * 6 + (idx2 % 6)]); res.Append(s2[(idx2 / 6) * 6 + (idx1 % 6)]);
            }
            return res.ToString();
        }

        private static string CreateSquare(string key) => new string((key + AlphabetTR + "X").Distinct().ToArray());

        public static string FourSquareDecrypt(string text, string key)
        {
            string k1, k2;
            if (key.Contains(",")) { var ks = key.Split(','); k1 = CleanInput(ks[0]); k2 = CleanInput(ks[1]); }
            else { k1 = CleanInput(key); if (string.IsNullOrWhiteSpace(k1)) k1 = "ANAHTAR"; k2 = new string(k1.Reverse().ToArray()); }
            string norm = AlphabetTR + "X", s1 = CreateSquare(k1), s2 = CreateSquare(k2);
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int idx1 = s1.IndexOf(text[i]), idx2 = s2.IndexOf(text[i + 1]);
                if (idx1 == -1) idx1 = 29; if (idx2 == -1) idx2 = 29;
                res.Append(norm[(idx1 / 6) * 6 + (idx2 % 6)]); res.Append(norm[(idx2 / 6) * 6 + (idx1 % 6)]);
            }
            return res.ToString().TrimEnd('X');
        }
        #endregion

        #region 9. Hill (2x2)
        public static string HillEncrypt(string text, string key)
        {
            text = CleanInput(text);
            if (text.Length % 2 != 0) text += "X";
            int[] k = key.Split(',').Select(s => int.TryParse(s, out int v) ? v : 0).ToArray();
            if (k.Length < 4) k = new int[] { 3, 3, 2, 5 };
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int p1 = GetIndex(text[i]), p2 = GetIndex(text[i + 1]);
                res.Append(GetChar((k[0] * p1 + k[1] * p2) % 29)); res.Append(GetChar((k[2] * p1 + k[3] * p2) % 29));
            }
            return res.ToString();
        }

        public static string HillDecrypt(string text, string key)
        {
            int[] k = key.Split(',').Select(s => int.TryParse(s, out int v) ? v : 0).ToArray();
            if (k.Length < 4) k = new int[] { 3, 3, 2, 5 };
            int det = Mod(k[0] * k[3] - k[1] * k[2], 29);
            int invDet = ModInverse(det, 29);
            if (invDet == -1) return "HATA: MATRISIN TERSI YOK";
            int[] invK = { Mod(invDet * k[3], 29), Mod(invDet * -k[1], 29), Mod(invDet * -k[2], 29), Mod(invDet * k[0], 29) };
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                int c1 = GetIndex(text[i]), c2 = GetIndex(text[i + 1]);
                res.Append(GetChar((invK[0] * c1 + invK[1] * c2) % 29)); res.Append(GetChar((invK[2] * c1 + invK[3] * c2) % 29));
            }
            return res.ToString();
        }
        #endregion

        #region 10. DSA - Dijital İmza
        public static string DSAEncrypt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            try
            {
                using (var dsa = DSA.Create())
                {
                    if (_dsaParams == null) _dsaParams = dsa.ExportParameters(true);
                    dsa.ImportParameters(_dsaParams.Value);
                    byte[] sig = dsa.SignData(Encoding.UTF8.GetBytes(text), HashAlgorithmName.SHA256);
                    return Convert.ToBase64String(sig) + "|||" + text;
                }
            }
            catch (Exception ex) { return "DSA HATA: " + ex.Message; }
        }

        public static string DSADecrypt(string signedText)
        {
            if (string.IsNullOrWhiteSpace(signedText)) return signedText;
            try
            {
                var parts = signedText.Split(new[] { "|||" }, StringSplitOptions.None);
                if (parts.Length != 2) return "GEÇERSİZ DSA!";
                using (var dsa = DSA.Create())
                {
                    if (_dsaParams == null) return "ANAHTAR YOK!";
                    dsa.ImportParameters(_dsaParams.Value);
                    bool ok = dsa.VerifyData(Encoding.UTF8.GetBytes(parts[1]), Convert.FromBase64String(parts[0]), HashAlgorithmName.SHA256);
                    return ok ? "[DOĞRULANDI] " + parts[1] : "[GEÇERSİZ İMZA]";
                }
            }
            catch (Exception ex) { return "DSA HATA: " + ex.Message; }
        }
        #endregion

        #region 11. RSA - Asimetrik Şifreleme
        public static string RSAEncrypt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            try
            {
                using (var rsa = RSA.Create())
                {
                    if (_rsaParams.Modulus == null) _rsaParams = rsa.ExportParameters(true);
                    rsa.ImportParameters(_rsaParams);
                    byte[] enc = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256);
                    return Convert.ToBase64String(enc);
                }
            }
            catch (Exception ex) { return "RSA HATA: " + ex.Message; }
        }

        public static string RSADecrypt(string encText)
        {
            if (string.IsNullOrWhiteSpace(encText)) return encText;
            try
            {
                using (var rsa = RSA.Create())
                {
                    if (_rsaParams.Modulus == null) return "ANAHTAR YOK!";
                    rsa.ImportParameters(_rsaParams);
                    byte[] dec = rsa.Decrypt(Convert.FromBase64String(encText), RSAEncryptionPadding.OaepSHA256);
                    return Encoding.UTF8.GetString(dec);
                }
            }
            catch (Exception ex) { return "RSA HATA: " + ex.Message; }
        }
        #endregion
    }
}