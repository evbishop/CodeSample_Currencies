# CodeSample_Currencies
This is a sample of my code as of 2024.
It is a currencies system that's based on what I had to implement in one of my work projects.
The repo includes the system itself, a demo scene and a bunch of Unit Tests for the system.

System Specifications
There are 3 currencies: copper, silver and gold. They have values of 1, 3 and 5 respectively, meaning that silver is worth 3 times as much as copper and gold is worth 5 times as much as copper.
These values can be changed, but there will always be at least one currency with a value of 1.
We need to be able to randomly generate a wallet filled with different currencies by provided a total worth of the wallet to the system.
It is preffered to generate multiple different currency types where possible, meaning that it should be rare (but possible) to get 10 copper when asking for a wallet with up to 3 currencies types and a total worth of 10.
It must be possible to generate wallets with a maximum of 1, 2 or 3 currency types on demand.

So, if we want to get a wallet worth 10 value, containing up to 3 different currency types, we may get a wallet with:
1. 10 copper
2. 7 copper, 1 silver
3. 4 copper, 2 silver
4. 1 copper, 3 silver
5. 2 copper, 1 silver, 1 gold
6. 5 copper, 1 gold
7. 2 gold

If we want to get a wallet worth 10 value, containing up to 2 different currency types, we may get a wallet with:
1. 10 copper
2. 7 copper, 1 silver
3. 4 copper, 2 silver
4. 1 copper, 3 silver
5. 5 copper, 1 gold
6. 2 gold

If we want to generate a wallet with a single currency type, the we should provide to the system the amount of coins of that type, rather that a total worth.
Meaning that we would ask to get a wallet with 10 copper coins, or a wallet with 10 silver coins, or a wallet with 10 gold coins.

These wallets could be used to give the player a reward, adding the currencies in the wallet to the players wallet, or they could be used to generate prices in stores or prices for services.

When removing money from a wallet, we need to provided to system how many coins of each type (if any) we want to remove.

Demo scene:
![image](https://github.com/evbishop/CodeSample_Currencies/assets/16606035/dfc3f2e6-cfa2-47ec-8c0a-c8f9a360119c)

This is how you can run the tests:
![image](https://github.com/evbishop/CodeSample_Currencies/assets/16606035/e336b832-f23a-41a9-8cd5-d1802095968e)
