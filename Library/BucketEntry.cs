using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class BucketEntry<Key, Value>
    {
        protected KeyValuePair<Key, Value> head;
        protected LinkedList<KeyValuePair<Key, Value>> items;

        public BucketEntry()
        {
            items = new();
        }

        public virtual KeyValuePair<Key, Value> Head
        {
            get
            {
                return head;
            }

            set
            {
                head = value;
            }
        }

        public virtual LinkedList<KeyValuePair<Key, Value>> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        public virtual void Add(KeyValuePair<Key, Value> item)
        {
            if(Head.Key == null && Head.Value == null)
            {
                Head = item;
            }
            else
            {
                Items.AddLast(item);
            }
        }
    }
}
