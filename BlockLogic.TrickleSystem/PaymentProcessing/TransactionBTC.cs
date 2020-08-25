using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using NBitcoin;
using Newtonsoft.Json.Linq;
using QBitNinja.Client;
using QBitNinja.Client.Models;


namespace BlockLogic.TrickleSystem.PaymentProcessing
{

    public class TransactionBTC
    {
        public TransactionBTC() { }
        

        public void SendMoney(decimal amount, string fromAddress, string toAddress)
        {
            Console.WriteLine($"Total Amount: {amount} \n From: {fromAddress} \n To: {toAddress}");
        }

        public static void SendTransaction(Decimal amountOfBtc, String toAddress)
        {
            Network network = Network.TestNet;
            BitcoinSecret frank = new BitcoinSecret("cSRGkyQ8Ny3QEMxHYp3BnDPAibQYReg4g1Nso7jfrAUgH79sjzsL");
            var PubKey = frank.PubKey;
            var address = PubKey.GetAddress(ScriptPubKeyType.Legacy, network);
            Console.WriteLine("Address: " + address);
            using (HttpClient client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12;
                var url = "https://api.blockcypher.com/v1/btc/test3/addrs/" + address;
                var response = client.GetAsync(url);
                var jsonVar = response.Result.Content.ReadAsStringAsync().Result;
                var Txs = JObject.Parse(jsonVar)["txrefs"];
                int numOfTxs = (int)JObject.Parse(jsonVar)["n_tx"];

                // loop through array to get spendable transaction hash
                String tx_hash = "";
                String prev_tx_hash = "";
                bool spendableTxFound = false;
                int maxTransactionEntries = numOfTxs;
                for (int i = 0; i < maxTransactionEntries; i++)
                {
                    tx_hash = Txs[i]["tx_hash"].ToString();
                    Console.WriteLine(tx_hash);

                    if (prev_tx_hash == tx_hash) //if the next tx_hash is the same 
                        maxTransactionEntries++;
                    try
                    {
                        if (Txs[i]["spent"].ToString() == "False" && (int)Txs[i]["confirmations"] > 5)
                        {
                            spendableTxFound = true;
                            break;
                        }
                    }
                    catch (NullReferenceException ex) { }
                    prev_tx_hash = tx_hash;
                } 
                // if spendable transaction is found
                if (spendableTxFound == true)
                {

                    var qclient = new QBitNinjaClient(network);
                    var transactionId = uint256.Parse(tx_hash);
                    var transactionResponse = qclient.GetTransaction(transactionId).Result;

                    Console.WriteLine("Address: " + address);
                    Console.WriteLine("Transaction ID: " + transactionResponse.TransactionId); 
                    Console.WriteLine("Amount of Confirmations: " + transactionResponse.Block.Confirmations);

                    var receivedCoins = transactionResponse.ReceivedCoins;
                    OutPoint outPointToSpend = null;
                    foreach (var coin in receivedCoins)
                    {
                        if (coin.TxOut.ScriptPubKey == address.ScriptPubKey)
                        {
                            outPointToSpend = coin.Outpoint;
                        }
                    }
                    if (outPointToSpend == null)
                        throw new Exception("TxOut doesn't contain our ScriptPubKey");
                    Transaction transaction = Transaction.Create(network);
                    transaction.Inputs.Add(new TxIn()
                    {
                        PrevOut = outPointToSpend
                    });

                    var recipientAddress = BitcoinAddress.Create(toAddress, network);
                    var amountToSend = new Money(amountOfBtc, MoneyUnit.BTC);
                    decimal numOfBytes = 1 * 180 + 2 * 34 + 10 + 1;
                    decimal satoshisInCoin = 100000000; // 1 hundred million
                    decimal recommendedFee = numOfBytes * 6 / satoshisInCoin;
                    // Set how to much to pay for fees below (Higher fees are mined quicker)
                    var minerFee = new Money(recommendedFee, MoneyUnit.BTC);
                    Console.WriteLine($"Your fees are: {minerFee}BTC");

                    // Change
                    var txInAmount = (Money)receivedCoins[(int)outPointToSpend.N].Amount;
                    var changeAmount = txInAmount - amountToSend - minerFee;

                    transaction.Outputs.Add(amountToSend, recipientAddress.ScriptPubKey);
                    // Send the change back
                    transaction.Outputs.Add(changeAmount, address.ScriptPubKey);
                    // Add a message for a total of 3 outputs
                    // String message = "I did it!";
                    //var bytes = Encoding.UTF8.GetBytes(message);
                    //transaction.Outputs.Add(Money.Zero, TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes));

                    // Add ScriptPubKey as input and sign with WIF private key
                    transaction.Inputs[0].ScriptSig = address.ScriptPubKey;
                    transaction.Sign(frank, receivedCoins.ToArray());

                    BroadcastResponse broadcastResponse = qclient.Broadcast(transaction).Result;

                    if (!broadcastResponse.Success)
                    {
                        Console.Error.WriteLine("ErrorCode: " + broadcastResponse.Error.ErrorCode);
                        Console.Error.WriteLine("Error message: " + broadcastResponse.Error.Reason);
                    }
                    else
                    {
                        Console.WriteLine("Success! You can check out the hash of the transaction in any block explorer:");
                        Console.WriteLine(transaction.GetHash());
                    }
                }
                else
                    Console.WriteLine("No spendable transaction (UTXO) found...");

            }

        }

        public void TestTransactionInfo()
        {
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");

            GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;
            NBitcoin.Transaction transaction = transactionResponse.Transaction;

            Console.WriteLine(transactionResponse.TransactionId);
            Console.WriteLine(transaction.GetHash());
            Console.WriteLine();
            //Received Coins
            Console.WriteLine("Outputs:");
            var outputs = transaction.Outputs;
            foreach (TxOut output in outputs)
            {
                Money amount = output.Value;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC));
                var paymentScript = output.ScriptPubKey;
                Console.WriteLine("ScriptPubKey: " + paymentScript);  // It's the ScriptPubKey
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine("address: " + address);
                Console.WriteLine();
            }
            //Spent Coins
            Console.WriteLine("Inputs:");
            Money receivedAmount = Money.Zero;
            var inputs = transaction.Inputs;
            foreach (TxIn input in inputs)
            {
                OutPoint previousOutpoint = input.PrevOut;
                Console.WriteLine("Prev Outpoint Hash: " + previousOutpoint.Hash); // hash of prev tx
                Console.WriteLine("Prev Outpoint Index: " + previousOutpoint.N); // idx of out from prev tx, that has been spent in the current tx
                Console.WriteLine();
            }
            Console.WriteLine("Amount of inputs: " + transaction.Inputs.Count);

            var spentCoins = transactionResponse.SpentCoins;

            foreach (var spentCoin in spentCoins)
            {
                receivedAmount = (Money)spentCoin.Amount.Add(receivedAmount);
            }
            Console.WriteLine("Bitcoin Received: " + receivedAmount.ToDecimal(MoneyUnit.BTC));

            var fee = transaction.GetFee(spentCoins.ToArray());
            Console.Write("Fees (in Satoshis): ");
            Console.WriteLine(fee);
        }
        // This one just generates a random private key along with its info 
        public void Demo()
        {
            Key privateKey = new Key(); // a 256-bit number
            BitcoinSecret wifKey = privateKey.GetWif(Network.TestNet); // base58check of privateKey (on TestNet where coins are worth nothing)
            PubKey publicKey = privateKey.PubKey; // privateKey multiplied by base point (base point=point on Bitcoins elliptical curve)
            var publicKeyHash = publicKey.Hash; // another point on Bitcoins elliptical curve
            var testNetAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.TestNet); // used to receive/check funds
            var paymentScript = testNetAddress.ScriptPubKey; // has conditions on how to receive coins

            Console.WriteLine("Private Key: " + privateKey);
            Console.WriteLine("BitcoinSecret: " + wifKey);
            Console.WriteLine("Public Key: " + publicKey);
            Console.WriteLine("Public Key Hash: " + publicKeyHash);
            Console.WriteLine("Address for TestNet: " + testNetAddress);
            Console.WriteLine("ScriptPubKey: " + paymentScript);
            //var paymentScript = publicKey.ScriptPubKey;


        }

        // This uses my WIF private key and shows the address
        public void SecretToAddress()
        {
            BitcoinSecret frank = new BitcoinSecret("cSRGkyQ8Ny3QEMxHYp3BnDPAibQYReg4g1Nso7jfrAUgH79sjzsL");
            Key pk = frank.PrivateKey;
            PubKey publicKey = pk.PubKey;
            //var publicKeyHash = publicKey.Hash;
            var testNetAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.TestNet);

            Console.WriteLine("testNetAddress: " + testNetAddress); // n1T57TZPCLqFJy1JVHBvrs25druomRxwVS
        }
        // This shows how to change from privatekey to bitcoinsecret and vice-versa
        public void TestSecret()
        {
            Key privateKey = new Key();
            BitcoinSecret testNetPrivateKey = privateKey.GetWif(Network.TestNet); // base58check of privateKey (wif=wallet input format=bitcoinsecret)
            Console.WriteLine(testNetPrivateKey); // cSRGkyQ8Ny3QEMxHYp3BnDPAibQYReg4g1Nso7jfrAUgH79sjzsL

            Key samePrivateKey = testNetPrivateKey.PrivateKey; // changing a wif/bitcoinsecret back to privateKey
            bool PrivateKeysMatch = privateKey == samePrivateKey;
            Console.WriteLine("Is the key generated from WIF the same: " + PrivateKeysMatch);
            Console.WriteLine("Address: {0}", privateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.TestNet));
        }
        // some basic stuff when I was beginning
        public void TestBasics()
        {
            // make a random private key
            Key privateKey = new Key(); // make a random private key

            // make public key from private key
            PubKey publicKey = privateKey.PubKey;
            Console.WriteLine("Private Key: " + privateKey);
            Console.WriteLine("Public Key: " + publicKey); // 03b7faee7ebbadd6a42d24aaa5f02ea66f7d57fcdb2196180b611225307f0e244f

            var publicKeyHash = publicKey.Hash;
            Console.WriteLine("PubKeyHash: " + publicKeyHash);

            /* mainNetAddress below used for actual transactions */
            //var mainNetAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);  
            var testNetAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.TestNet);
            //Console.WriteLine("Public Key Address for Main: " + mainNetAddress);
            Console.WriteLine("Public Key Address for TestNet: " + testNetAddress);

            /*
                ScriptPubKeys are generated from bitcoin addresses,
                the following shows how to obtain addresses from ScriptPubKeys
            */
            var paymentScript = publicKeyHash.ScriptPubKey;
            var anotherTestNetAddress = paymentScript.GetDestinationAddress(Network.TestNet);

            Console.WriteLine("Test Net Address obtained from ScriptPubKey: " + anotherTestNetAddress);

            var samePublicKeyHash = (KeyId)paymentScript.GetDestination();
            Console.WriteLine("Public Key Has obtained from ScriptPubKey: " + samePublicKeyHash);
        }
    }

}