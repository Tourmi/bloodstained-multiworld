import sys

with open('key.dat', 'rb') as keyFile:
    key = keyFile.read()

def xor(el):
    idx, byte = el
    return byte ^ key[idx % len(key)]

with open(sys.argv[1], 'rb') as inFile:
    before = inFile.read()

with open(sys.argv[2], 'wb') as outFile:
    outFile.write(bytes(map(xor, enumerate(before))))
