using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;
using Helper = Neo.SmartContract.Framework.Helper;

namespace Neo.SmartContract
{
    public class SupplyChainNFTs : Framework.SmartContract
    {
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger, byte[], byte[]> TransferNotify;

        [DisplayName("MintToken")]
        public static event Action<byte[], byte[], byte[]> MintTokenNotify;
        
        private static StorageContext Context() => Storage.CurrentContext;

        private const byte Prefix_TotalSupply = 10;

        private static readonly byte[] Prefix_TokenOwnerBA = new byte[] { 11 };
        private static readonly byte[] Prefix_TokensOfBA = new byte[] { 14 };
        private static readonly byte[] Prefix_PropertiesBA = new byte[] { 13 };
        private static readonly byte[] Prefix_TokenBalanceBA = new byte[] { 12 };
            
        private const int TOKEN_DECIMALS = 8;
        private const int FACTOR = 100_000_000;
        private static readonly byte[] superAdmin = Helper.ToScriptHash("AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y");
        
        [DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transferred;


        public static Object Main(string operation, params object[] args)
        {
            if (operation == "mintNFT") return MintNFT((byte[])args[0],(byte[])args[1],(byte[])args[2]);
            if (operation == "transfer") return Transfer((byte[])args[0],(byte[])args[1],(BigInteger)args[2],(byte[])args[3],(byte[])args[4]);
            if (operation == "balanceOf") return  BalanceOf((byte[])args[0],(byte[])args[1]);
            
            return false;
        }

        
        public static bool MintNFT(byte[] tokenId, byte[] owner, byte[] historic)
        {
            if (!Runtime.CheckWitness(superAdmin)) return false;

            if (owner.Length != 20) throw new FormatException("The parameter 'owner' should be 20-byte address.");
            if (historic.Length > 2048) throw new FormatException("The length of 'properties' should be less than 2048.");

            byte[] key = Prefix_TokenOwnerBA.Concat(tokenId);
            StorageMap tokenOwnerMap = Storage.CurrentContext.CreateMap(key.AsString());
            if (tokenOwnerMap.Get(owner) != null) return false;
            
            byte[] key2 = Prefix_TokensOfBA.Concat(owner);
            StorageMap tokenOfMap = Storage.CurrentContext.CreateMap(key2.AsString());

            byte[] key3 = Prefix_PropertiesBA.Concat(tokenId);
            Storage.Put(Context(), key3, historic);
            tokenOwnerMap.Put(owner, owner);
            tokenOfMap.Put(tokenId, tokenId);
            
            byte[] Prefix_TotalSupplyBA = new byte[] { Prefix_TotalSupply };
            var totalSupply = Storage.Get(Context(), Prefix_TotalSupplyBA);
            if (totalSupply is null)
                Storage.Put(Context(), Prefix_TotalSupplyBA, FACTOR);
            else
                Storage.Put(Context(), Prefix_TotalSupplyBA, totalSupply.ToBigInteger() + FACTOR);
                
            byte[] key4 = Prefix_TokenBalanceBA.Concat(owner);    
            StorageMap tokenBalanceMap = Storage.CurrentContext.CreateMap(key4.AsString());
            tokenBalanceMap.Put(tokenId, FACTOR);
            
            MintTokenNotify(owner, tokenId, historic);
            
            return true;
        }

        public static bool Transfer(byte[] from, byte[] to, BigInteger amount, byte[] tokenId, byte[] reason)
        {
            if (from.Length != 20 || to.Length != 20) throw new FormatException("The parameters 'from' and 'to' should be 20-byte addresses.");
            if (amount < 0 || amount > FACTOR) throw new FormatException("The parameters 'amount' is out of range.");
            if (!Runtime.CheckWitness(from)) return false;
            
            if (from.Equals(to))
            {
                TransferNotify(from, to, amount, tokenId, reason);
                return true;
            }

            StorageMap fromTokenBalanceMap = Storage.CurrentContext.CreateMap(Prefix_TokenBalanceBA.Concat(from).AsString());
            StorageMap toTokenBalanceMap = Storage.CurrentContext.CreateMap(Prefix_TokenBalanceBA.Concat(to).AsString());
            StorageMap tokenOwnerMap = Storage.CurrentContext.CreateMap(Prefix_TokenOwnerBA.Concat(tokenId).AsString());
            StorageMap fromTokensOfMap = Storage.CurrentContext.CreateMap(Prefix_TokensOfBA.Concat(from).AsString());
            StorageMap toTokensOfMap = Storage.CurrentContext.CreateMap(Prefix_TokensOfBA.Concat(to).AsString());
            
            var fromTokenBalance = fromTokenBalanceMap.Get(tokenId);
            if (fromTokenBalance == null || fromTokenBalance.ToBigInteger() < amount) return false;
            var fromNewBalance = fromTokenBalance.ToBigInteger() - amount;
            if (fromNewBalance == 0)
            {
                tokenOwnerMap.Delete(from);
                fromTokensOfMap.Delete(tokenId);
            }
            fromTokenBalanceMap.Put(tokenId, fromNewBalance);

            var toTokenBalance = toTokenBalanceMap.Get(tokenId);
            if (toTokenBalance is null && amount > 0)
            {
                tokenOwnerMap.Put(to, to);
                toTokenBalanceMap.Put(tokenId, amount);
                toTokensOfMap.Put(tokenId, tokenId);
            }
            else
            {
                toTokenBalanceMap.Put(tokenId, toTokenBalance.ToBigInteger() + amount);
            }
            // Notify
            TransferNotify(from, to, amount, tokenId, reason);
            return true;
        }

        public static BigInteger BalanceOf(byte[] owner, byte[] tokenid)
        {
            if (owner.Length != 20) throw new FormatException("The parameter 'owner' should be 20-byte address.");
            byte[] key = Prefix_TokenBalanceBA.Concat(owner);
            if (tokenid is null)
            {
                var iterator = Storage.Find(Context(), key.AsString());
                BigInteger result = 0;
                while (iterator.Next())
                    result += iterator.Value.ToBigInteger();
                return result;
            }
            else
                return Storage.CurrentContext.CreateMap(key.AsString()).Get(tokenid).ToBigInteger();
        }
    }
}
