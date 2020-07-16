using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Cno.Roca.Core.Cryptography
{
    public class Encryption
    {
        #region "Atributos Estaticos"

        /// <summary>Llave de encripcion DB</summary>
        private static readonly byte[] _encryptionKeyDB = new byte[32]
            {
                4, 2, 2, 9, 5, 5, 0, 0, 3, 6, 3, 9, // telefono PAB
                4, 2, 2, 9, 5, 5, 0, 0, 5, 4, 0, 1, // telefono NNF
                4, 8, 8, 8, 8, 8, 8, 8
            };

        /// <summary>Llave de encripcion Licencia</summary>
        private static readonly byte[] _encryptionKeyLicencia = new byte[32]
            {
                4, 2, 2, 9, 5, 5, 0, 0, 3, 6, 3, 7, 
                4, 2, 1, 9, 5, 5, 0, 0, 5, 4, 0, 1, 
                0, 8, 8, 8, 6, 8, 8, 8
            };

        /// <summary>Vector inicial de encripcion</summary>
        private static readonly byte[] _initialVector = new byte[16]
            {
                8, 8, 8, 8,
                4, 2, 2, 9, 5, 5, 0, 0, 5, 4, 0, 1, // telefono NNF
            };

        /// <summary>Path donde se guarda el password de la DB</summary>
        private static readonly string _pathPasswordDB = Path.Combine(ConfigurationManager.AppSettings["WMS.ConfigPath"], "OraclePasswordAccess.xml");

        #endregion

        #region "Propiedades Publicas"

        /// <summary>Devuelve el path hacia el archivo que contiene el password encriptado de la DB.</summary>
        public static string PathArchivoPasswordDB
        {
            get { return _pathPasswordDB; }
        }

        #endregion

        #region "Metodos publicos"

        /// <summary>
        /// Encripta un mensaje utilizando el algoritmo de Rijndael.
        /// </summary>
        /// <param name="message">El mensaje en claro, a encriptar.</param>
        /// <param name="key">key utilizada para la encripcion, debe ser de 32 bytes</param>
        /// <returns>Los bytes del mensaje encriptado.</returns>
        public static byte[] Encrypt(string message,byte[] key)
        {
            ASCIIEncoding textConverter = new ASCIIEncoding();
            RijndaelManaged myRijndael = new RijndaelManaged();
            byte[] messageBytes;

            //Get an encryptor.
            ICryptoTransform encryptor = myRijndael.CreateEncryptor(key, _initialVector);

            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            //Convert the data to a byte array.
            messageBytes = textConverter.GetBytes(message);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(messageBytes, 0, messageBytes.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            return msEncrypt.ToArray();
        }

        /// <summary>
        /// Encripta un mensaje utilizando el algoritmo de Rijndael.
        /// </summary>
        /// <param name="messageEncrypted">El mensaje encriptado.</param>
        /// <returns>El mensaje desencriptado.</returns>
        /// <param name="key">La clave con la cual se encripta.</param>
        public static string Decrypt(byte[] messageEncrypted, byte[] key)
        {
            ASCIIEncoding textConverter = new ASCIIEncoding();
            RijndaelManaged myRijndael = new RijndaelManaged();
            byte[] fromEncrypt;

            //Get a decryptor that uses the same key and IV as the encryptor.
            ICryptoTransform decryptor = myRijndael.CreateDecryptor(key, _initialVector);

            //Now decrypt the previously encrypted message using the decryptor
            // obtained in the above step.
            MemoryStream msDecrypt = new MemoryStream(messageEncrypted);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

            fromEncrypt = new byte[messageEncrypted.Length];

            //Read the data out of the crypto stream.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the byte array back into a string.
            return textConverter.GetString(fromEncrypt).Replace("\0","");
        }

        /// <summary>
        /// Configura la cantidad de licencias.
        /// </summary>
        /// <param name="cantidadLicencias">La cantidad de licencias a encriptar.</param>
        /// <param name="path">El path donde se guarda el xml encriptado.</param>
        public static void SetCantidadLicencias(int cantidadLicencias, string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            // guardo la entrada "nombreArchivo+CantidadLicencias" encriptados
            SaveEncryptedString(filename + cantidadLicencias, path, _encryptionKeyLicencia);
        }

        /// <summary>
        /// Obtiene la cantidad de licencias desde un archivo xml.
        /// </summary>
        /// <returns>
        /// La cantidad de licencias en claro.
        /// </returns>
        public static int GetCantidadLicencias(string path)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(path);
                // devuelve "nombreArchivo+CantidadLicencias" desencriptados
                string desencriptado = LoadEncrytedString(path, _encryptionKeyLicencia);

                if (filename.Length > desencriptado.Length)
                    throw new Exception("Archivo de licencia invalido: por longitud");

                // comparo que el nombre del archivo que estoy leyendo sea el mismo que esta encriptado
                for (int i = 0; i < filename.Length; i++)
                    if (filename[i].ToString().ToUpper() != desencriptado[i].ToString().ToUpper())
                        throw new Exception("Archivo de licencia invalido: por comparacion");

                return int.Parse(desencriptado.Substring(filename.Length, desencriptado.Length - filename.Length));
            }
            catch
            {
                throw new Exception("Licencia invalida");
            }
        }

        /// <summary>
        /// Configura el password del connection string de la base de datos.
        /// </summary>
        /// <param name="passwordDB">El password en claro.</param>
        public static void SetPasswordDB(string passwordDB)
        {
            SaveEncryptedString(passwordDB, _pathPasswordDB, _encryptionKeyDB);
        }

        /// <summary>
        /// Obtiene el password de la base de datos desde un archivo xml.
        /// </summary>
        /// <returns>
        /// El password desencriptado.
        /// </returns>
        public static string GetPasswordDB()
        {
            return LoadEncrytedString(_pathPasswordDB, _encryptionKeyDB);
        }

        #endregion

        #region "Metodos Privados"

        /// <summary>
        /// Guarda el string encryptado en un archivo
        /// </summary>
        /// <param name="clearString">string a encriptar</param>
        /// <param name="path">path donde se guaarda el string</param>
        /// <param name="encryptionKey">La llave con la cual se encripta (32 Bytes)</param>
        private static void SaveEncryptedString(string clearString, string path, byte[] encryptionKey)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(byte[]));
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                xmlSerializer.Serialize(streamWriter, Encrypt(clearString, encryptionKey));
            }
        }


        /// <summary>
        /// Carga el string encryptado en un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <param name="decryptionKey">La llave con la cual se desencripta (32 Bytes)</param>
        private static string LoadEncrytedString(string path, byte[] decryptionKey)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(byte[]));
            using (StreamReader streamReader = new StreamReader(path))
            {
                byte[] bytes = (byte[])xmlSerializer.Deserialize(streamReader);
                return Decrypt(bytes, decryptionKey);
            }
        }
     
        #endregion
    }
}