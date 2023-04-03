using System.Collections.Generic;
using System.Collections;
using System;

namespace Program
{

    public class ListSymbolTable<K, V> : IEnumerable<K> where K: IComparable
    {

        private class Node<K, V> // Node class used within the linked list that allows for storage of data.
        {
            public K key;
            public V value;
            public Node<K, V> next;
            /// <summary>
            /// Creates a new node object with a key of type K and a value of type V
            /// </summary>
            /// <param name="key"></param>
            /// <param name="value"></param>
            public Node(K key, V value = default)
            {
                this.key = key;
                this.value = value;
                this.next = null;
            }


            public override string ToString()
            {
                return $"[Node with data ({key} : {value})]";
            }
        } // End of Node class
        
        // Beginning of Linked List
        private Node<K, V> head;
        private int count;
        public int Count => count;
        

        public ListSymbolTable() // Default Constructor
        {
            head = null;
            count = 0;
        }

        /*
            ====================
            |       LIST       |
            |    FUNCTIONS     |
            ====================
        */


        public void Add(K key, V value) 
        {
            Node<K,V> newNode = new Node<K, V>(key ,value);
            newNode.next = head;
            head = newNode;
            count++;
        }


        /// <summary>
        /// Walks to a specific index inside the list, returning the node object at that index
        /// </summary>
        /// <param name="index"></param>
        private Node<K, V> WalkToNode(int index)
        {
            Node<K, V> curr = head;
            if ( (index < 0) || (index > count))
            {
                throw new IndexOutOfRangeException($"Error: index {index} is out of range!");
            }
            for(int i = 0; i < index; i++)
            {
                curr = curr.next;
            }
            return curr;
        }

        /// <summary>
        /// Variation of WalkToIndex that takes a Node object's key and returns the node with that key
        /// </summary>
        /// <param name="key"></param>
        private Node<K, V> WalkToNode(K key)
        {
            Node<K, V> curr = head;
            while(curr != null)
            {
                if (key.Equals(curr.key))
                {
                    return curr;
                }
                curr = curr.next;
            }
            throw new KeyNotFoundException($"Error: key '{key}' is not in the table!");
        }

        public void Remove(K key)
        {
            Node<K, V> prev = head;
            
            // Special case -- Removing head of the list
            if (count > 0 && head.key.Equals(key))
            {
                Node<K, V> toRemove = head;
                head = head.next;
                toRemove.key = default;
                toRemove.value = default;
                toRemove.next =null;
                count--;
                return;
            }

            while (prev != null && prev.next != null)
            {
                // if the node in *Front* of me has the key
                if(prev.next.key.Equals(key))
                {
                    Node<K, V> toRemove = prev.next;
                    prev.next = toRemove.next;
                    toRemove.key = default;
                    toRemove.value = default;
                    toRemove.next = null;
                    count--;
                    break;
                }
            }
        }


        public bool Contains(K key)
        {
            try
            {
                Node<K,V> curr = WalkToNode(key);
                return true;
            }
            catch(KeyNotFoundException knfe)
            {
                return false;
            }
        }
        
        public void Clear()
        {
            head = null;   
            count = 0;
        }

        public K[] GetKeys()
        {
            K[] arr = new K[count];
            Node<K, V> node = head;
            for (int i = 0; i < count; i++)
            {
                node = WalkToNode(i);
                arr[i] = node.key;
            }
            return arr;
        }

        public V[] GetValues()
        {
            V[] arr = new V[count];
            Node<K, V> node = head;
            for (int i = 0; i < count; i++)
            {
                node = WalkToNode(i);
                arr[i] = node.value;
            }
            return arr;
        }


        /*
            =====================
            |    ENUMERATION    |
            |     METHODS       |
            =====================
        */
        public IEnumerator<K> GetEnumerator()
        {
            Node<K, V> curr = head;
            for(int i = 0; i < count; i++)
            {
                yield return curr.key;
                if(curr.next != null) { curr = curr.next; }
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public override string ToString()
        {
             return $"SymbolTable Object | Length: {count}";
        }
 
 
        public V this[K key]
        {
            get
            {
                Node<K, V> node = WalkToNode(key);
                return node.value;
            }
            set
            {
                Node<K, V> node = WalkToNode(key);
                node.value = value;
            }

        }

    }
}