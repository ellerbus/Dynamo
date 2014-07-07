using System.Collections;

namespace Dynamo.Forms
{
    sealed class DictionaryBindingPair
    {
        private IDictionary _data;

        public DictionaryBindingPair(IDictionary data, string key)
        {
            Key = key;

            _data = data;
        }
        
        public string Key { get; private set; }
        
        public object Value { get { return _data[Key]; } set { _data[Key] = value; } }
        
        public string Type { get { return Value.GetType().Name; } }
    }
}
