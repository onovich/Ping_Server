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

        public TSecond GetByFirst(TFirst first) {
            if (firstToSecond.TryGetValue(first, out TSecond second)) {
                return second;
            }
            throw new KeyNotFoundException("First value not found.");
        }

        public TFirst GetBySecond(TSecond second) {
            if (secondToFirst.TryGetValue(second, out TFirst first)) {
                return first;
            }
            throw new KeyNotFoundException("Second value not found.");
        }

        public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator() {
            return firstToSecond.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}