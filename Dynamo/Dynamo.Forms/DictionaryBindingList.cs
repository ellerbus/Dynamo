using System.Collections;
using System.ComponentModel;

namespace Dynamo.Forms
{
    class DictionaryBindingList : BindingList<DictionaryBindingPair>
    {
        private readonly IDictionary _data;

        public DictionaryBindingList(IDictionary data)
        {
            _data = data;

            Reset();
        }

        public void Reset()
        {
            bool oldRaise = RaiseListChangedEvents;

            RaiseListChangedEvents = false;

            try
            {
                Clear();

                foreach (var key in _data.Keys)
                {
                    Add(new DictionaryBindingPair(_data, key.ToString()));
                }
            }
            finally
            {
                RaiseListChangedEvents = oldRaise;

                ResetBindings();
            }
        }

    }
}
