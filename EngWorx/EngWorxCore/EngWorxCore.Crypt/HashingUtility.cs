using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Crypt
{
    /// <summary>
    ///     Extension methods applied to the <see cref="object"/> type.
    /// </summary>
    public static class HashingUtility
    {
        /// <summary>
        ///     Gets a hash of the current instance.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the Cryptographic Service Provider to use.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetHash<T>(object instance) where T : HashAlgorithm, new()
        {
            T cryptoServiceProvider = new T();
            return computeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        ///     Gets a key based hash of the current instance.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the Cryptographic Service Provider to use.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <param name="key">
        ///     The key passed into the Cryptographic Service Provider algorithm.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetKeyedHash<T>(object instance, byte[] key) where T : KeyedHashAlgorithm, new()
        {
            T cryptoServiceProvider = new T { Key = key };
            return computeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        ///     Gets a MD5 hash of the current instance.
        /// </summary>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetMD5Hash(object instance)
        {
            return GetHash<MD5CryptoServiceProvider>(instance);
        }

        /// <summary>
        ///     Gets a SHA1 hash of the current instance.
        /// </summary>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetSHA1Hash(object instance)
        {
            return GetHash<SHA1CryptoServiceProvider>(instance);
        }

        private static string computeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
        {
            DataContractSerializer serializer = new DataContractSerializer(instance.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }
    }
}