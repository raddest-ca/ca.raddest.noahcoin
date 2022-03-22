namespace NoahCoin;

public record Transaction(
    BigInteger Sender,
    BigInteger Receiver,
    BigInteger Amount
);

public record RewardTransaction(
    BigInteger Owner,
    BigInteger Amount
) : Transaction(Owner, Owner, Amount);