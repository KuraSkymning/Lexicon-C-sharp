using System;

namespace Garage
{
    // En nyckel-värde-samling från strängar till mängder av strängar.
    // Klassen är ineffektiv eftersom kraven anger att man inte får använda System.Collections.
    public class StringDictionary
    {
        private StringDictionaryEntry[] _entries = [];

        // Hämta antalet nyckel-värde-par i samlingen.
        public int Count => _entries.Length;

        // Lägg till en nyckel och ett strängvärde.
        // Returnerar mängden av strängar som är kopplad till nyckeln.
        public StringSet Add(string key, string value)
        {
            StringSet stringset = Add(key);
            stringset.Add(value);
            return stringset;
        }

        // Lägg till en nyckel.
        // Returnerar mängden av strängar som är kopplad till nyckeln.
        public StringSet Add(string key)
        {
            StringSet? stringset = Get(key);
            if (stringset == null)
            {
                stringset = new StringSet();
                StringDictionaryEntry entry = new(key, stringset);
                Array.Resize(ref _entries, _entries.Length + 1);
                _entries[^1] = entry;
            }
            return stringset;
        }

        // Hämta mängden av strängar som är kopplad till nyckeln.
        public StringSet? Get(string key)
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].key == key)
                {
                    return _entries[i].value;
                }
            }
            return null;
        }

        // Hämta alla nycklar i samlingen.
        // Om det have varit tillåtet att använda System.Collections
        // hade det varit bättre att returnera en IEnumerable<string> istället.
        // Man hade också kunnat implementera en indexer istället,
        // men det är oklart huruvida det är tillåtet.
        public string[] Keys()
        {
            string[] keys = new string[_entries.Length];
            for (int i = 0; i < _entries.Length; i++)
            {
                keys[i] = _entries[i].key;
            }
            return keys;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _entries.Length; i++)
            {
                result += $"- {_entries[i].key}: {_entries[i].value}\n";
            }
            return result;
        }
    }

    // En post i StringDictionary.
    internal readonly struct StringDictionaryEntry(string key, StringSet value)
    {
        public readonly string key = key;
        public readonly StringSet value = value;
    }

    // En mängd av strängar.
    // Klassen är ineffektiv eftersom kraven anger att man inte får använda System.Collections.
    public class StringSet
    {
        private string[] _values = [];

        // Hämta antalet strängvärden i mängden.
        public int Count => _values.Length;

        // Lägg till ett strängvärde i mängden.
        public void Add(string value)
        {
            if (!Contains(value))
            {
                Array.Resize(ref _values, _values.Length + 1);
                _values[^1] = value;
            }
        }

        // Kontrollera om mängden innehåller ett visst strängvärde.
        public bool Contains(string value)
        {
            return Array.IndexOf(_values, value) >= 0;
        }

        // Hämta alla strängvärden i mängden.
        // Om det have varit tillåtet att använda System.Collections
        // hade det varit bättre att returnera en IEnumerable<string> istället.
        // Man hade också kunnat implementera en indexer istället,
        // men det är oklart huruvida det är tillåtet.
        public string[] Values()
        {
            string[] values = new string[_values.Length];
            Array.Copy(_values, values, _values.Length);
            return values;
        }

        public override string ToString()
        {
            return string.Join(", ", _values);
        }
    }
}
