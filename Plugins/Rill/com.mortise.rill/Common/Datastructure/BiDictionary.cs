using System;
using System.Collections;
using System.Collections.Generic;

namespace MortiseFrame.Rill {

    public class BiDictionary<Key, Value> : IEnumerable<KeyValuePair<Key, Value>> {

        Dictionary<Key, Value> keyToValue = new Dictionary<Key, Value>();
        Dictionary<Value, Key> valueToKey = new Dictionary<Value, Key>();

        public void Add(Key key, Value value) {
            if (keyToValue.ContainsKey(key) || valueToKey.ContainsKey(value)) {
                throw new ArgumentException("Duplicate first or second value.");
            }
            keyToValue[key] = value;
            valueToKey[value] = key;
        }

        public bool ContainsKey(Key key) {
            return keyToValue.ContainsKey(key);
        }

        public bool ContainsValue(Value value) {
            return keyToValue.ContainsValue(value);
        }

        public bool TryGetByKey(Key key, out Value value) {
            return keyToValue.TryGetValue(key, out value);
        }

        public bool TryGetByValue(Value value, out Key key) {
            return valueToKey.TryGetValue(value, out key);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator() {
            return keyToValue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Clear() {
            keyToValue.Clear();
            valueToKey.Clear();
        }

    }

}