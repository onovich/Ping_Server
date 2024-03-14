using System;
using System.Collections;
using System.Collections.Generic;

namespace Ping.Protocol {

    public class BiDictionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>> {

        Dictionary<TFirst, TSecond> firstToSecond = new Dictionary<TFirst, TSecond>();
        Dictionary<TSecond, TFirst> secondToFirst = new Dictionary<TSecond, TFirst>();

        public void Add(TFirst first, TSecond second) {
            if (firstToSecond.ContainsKey(first) || secondToFirst.ContainsKey(second)) {
                throw new ArgumentException("Duplicate first or second value.");
            }
            firstToSecond[first] = second;
            secondToFirst[second] = first;
        }

        public bool TryGetByFirst(TFirst first, out TSecond second) {
            return firstToSecond.TryGetValue(first, out second);
        }

        public bool TryGetBySecond(TSecond second, out TFirst first) {
            return secondToFirst.TryGetValue(second, out first);
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator() {
            return firstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}