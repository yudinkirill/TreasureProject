using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// по идее этому классу место не в модели, не с данными, но щас лень плодить сборки, поэтому пусть будет тут
    /// </summary>
    public class NotesLoader
    {        //GetNotes не относится к этому классу, способ получения данных не должен быть описан в самих данных. Если я потом захочу получать не с сервера, а с другого места? или с нескольких? переписывать класс данных?
        public static string adress = "http://192.168.0.104/";
        public static string  Key = "XO";
        #region Data
        public static ObservableCollection<Note> GetNotes() //Коллекция заметок
        {
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(adress);
             var json = client.GetStringAsync($"api/notes");
             ObservableCollection<Note> notes = JsonConvert.DeserializeObject<ObservableCollection<Note>>(json.Result);

            Note note = new Note();
            //Расшифровываем заголовок
            var x = note.NoteHeader;
             Decrypt(x, Key);
            note.NoteHeader = x;
            //Расшифровываем текст
            var z = note.NoteText;
            Decrypt(z, Key);
            note.NoteHeader = z;

            return notes;
        }





        public async static Task<bool> AddItem(Note note)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(adress);
            
            //Шифруем Заголовок
            var z = note.NoteHeader;
            var x = Encrypt(z, Key);
            note.NoteHeader = x;

            //Шифруем Текст
            var c = note.NoteText;
            var v = Encrypt(c, Key);
            note.NoteText = x;
            
            //Преобразуем в JSON, отправляем
            var serializedItem = JsonConvert.SerializeObject(note);
            var response = await client.PostAsync($"api/notes", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async static Task<bool> EditItem(Note note)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(adress);
            var serializedItem = JsonConvert.SerializeObject(note);
            var response = await client.PutAsync($"api/notes/{note.NoteId}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Запрос на удаление ноты по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteItem(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(adress);
            var response = client.DeleteAsync($"api/notes/{id}").Result;

            return response.IsSuccessStatusCode;
        }
        #endregion
        #region Cryptography

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                                
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
            catch { return "U have 0 notes"; }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
        #endregion
    }
}
