# Basic-Encryption-System

The system incorporates 3 main mechinisms:
- a cipher mechinism 
- a cycling mechinism
- a salting mechinism

All the 3 mechinisms derive all their properties and variables from the key; there are not magic values inherent to the system.

The 3 mechanisms multiplicatively complement each other.

Cipher:
This mechanism implements a simple cipher, maping charecters to their corsponding value in the key.

Cycling:
This mechanism implements a shift to the key, for example 5G/c9P => P5G/c9. This example using a jumpConstant = 1, the key shifts by one position each time a value is mapped. The jumpConstant is derived out of the key its self. The mechanism makes it hard to reconstruct the key by brute-force.

Salting:
a map describing the position of the real values and the random values in the final string is generated by converting an amount of the first charecters in the key into a binary sequance(this amount depends on the number of real values needed, i.e the length of the actual message), these first values are ultimately random since the key its self is randomly generated. once a binary sequance is generated, it could be used as a blue print as to where the real values should be mapped and where the random values should be scattered. For instance 1's could be considered the position of real values, and 0's the position of random values.

If a malicious individual tries to crack the code, and somehow they manage to know how to decipher it, deciphering a single charecter which is a salt value will desynchronise the key by an encrement of one jumpConstant which means the rest of the deciphered values will be random nonsense.




The Key:
A key is generated by creating a list whose elements are the same as the list of all possible charecters referred to as "alpha", but each charecter has a completely random position.

an element in the key maps the charecter with its same index in alpha into the value of that element. for example:

alpha = "abcdefghi"
key = "bdecaghif"

the value "b" in the key has the index 0 in the key, which corsponds to "a" in alpha. Therefore key(a) => b, key(b) => d, key(e) => c.
