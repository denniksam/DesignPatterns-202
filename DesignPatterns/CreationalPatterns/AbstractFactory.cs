using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CreationalPatterns
{
    internal class AbstractFactoryDemo
    {
        public void Show()
        {
            ICrypterAbstractFactory factory = new DSTUFactory();

            ICryptoHasher hasher = factory.GetHasher();
            Console.WriteLine(hasher.Hash("test"));
            ICryptoCipher cipher = factory.GetCipher();
            Console.WriteLine(cipher.Cipher("test"));
        }
    }

    //////////////////////////// Abstraction ////////////////////////////////
    interface ICryptoHasher
    {
        string Hash(string input);
    }
    interface ICryptoCipher
    {
        string Cipher(string input);
    }
    interface ICryptoDecipher
    {
        string Decipher(string input);
    }

    interface ICrypterAbstractFactory
    {
        ICryptoHasher GetHasher();
        ICryptoCipher GetCipher();
        ICryptoDecipher GetDecipher();
    }

    ////////////////////////////// DSTU ////////////////////////////////////
    class DSTUHasher : ICryptoHasher
    {
        public string Hash(string input)
        {
            return $"Kupina hash of '{input}' ";
        }
    }
    class DSTUCipher : ICryptoCipher
    {
        public string Cipher(string input)
        {
            return $"Kalina cipher of '{input}' ";
        }
    }
    class DSTUDecipher : ICryptoDecipher
    {
        public string Decipher(string input)
        {
            return $"Kalina decipher of '{input}' ";
        }
    }

    class DSTUFactory : ICrypterAbstractFactory
    {
        public ICryptoCipher GetCipher()
        {
            return new DSTUCipher();  // Фабричная суть - создание объекта
        }  

        public ICryptoDecipher GetDecipher()
        {
            return new DSTUDecipher();
        }

        public ICryptoHasher GetHasher()
        {
            return new DSTUHasher();
        }
    }

    ////////////////////////////// AES ///////////////////////////////////
    class AESHasher { }
    class AESCipher { }
    class AESDecipher { }

    class AESFactory { }
    
}
/* Абстрактная фабрика - фабрика фабрик
Простая фабрика - создает конкретные объекты
Если конкретные объекты связаны, то переключение на новую связку - это использование
новой фабрики. Абстрактная фабрика - фабрика, создающая конкретную (простую) фабрику
Crypto{ Hash, Encipher, Decipher }
 AES { SHA, Cipher, Decipher }
 DSTU{ Kupina, KalinaC, KalinaD }

 */