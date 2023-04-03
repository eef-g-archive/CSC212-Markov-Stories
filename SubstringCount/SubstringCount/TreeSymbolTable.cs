using System.Collections.Generic;
using System.Collections;
using System;

namespace Program
{
    public class BinarySearchTree<K, V> : IEnumerable<K>  where K : IComparable
    {
        // Beginning of Node Class -- Has special variation from Node class of Linked List
        public class Node<K, V>
        {
            public K key;
            public V value;
            public Node<K, V> L;
            public Node<K, V> R;

            public int count;

            public Node(K key, V value)
            {
                this.key = key;
                this.value = value;
                this.L = null;
                this.R = null;
            }
            public override string ToString()
            {
                string Lchild = "NULL";
                return Lchild;
            }

        }
        // End of Node Class

        public Node<K, V> root;
        int count = 0;
        public int Count => count;

        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }

        
        
        /// <summary>
        /// Adds a new node the the tree in order of value based off of the key. Adds 1 to the count of nodes in the tree.
        /// </summary>
        public void Add(K key, V value)
        {
            root = Add(key, value, root);
            count++;
        }

        /// <summary>
        /// Checks where the new Node needs to be added on the tree. Will throw an exception if a repeat key is being added.
        /// </summary>
        private Node<K, V> Add(K key, V value, Node<K, V> subroot)
        {
            if(subroot == null)
            {
                return new Node<K, V>(key ,value);
            }
            else
            {
                int compare = key.CompareTo(subroot.key);
                if (compare == -1)
                {
                    subroot.L = Add(key, value, subroot.L);
                }
                else if (compare == 1)
                {
                    subroot.R =  Add(key, value, subroot.R);
                }
                else
                {
                    throw new ArgumentException("ERROR: Cannot have two of the same key!");
                }
            }
            return subroot;
        }


        private Node<K, V> WalkToNode(K nodeKey, Node<K, V> subroot)
        {
            if (subroot == null)
            {
                return null;
            }

            int compare = nodeKey.CompareTo(subroot.key);
            if (compare == -1)
            {
                // Move to the left
                return WalkToNode(nodeKey, subroot.L);
            }
            else if (compare == 1)
            {
                //Move to the Right
                return WalkToNode(nodeKey, subroot.R);
            }
            else
            {
                return subroot;
            }
        }

        /// <summary>
        /// Returns the largest key (A.K.A the rightmost Node in the tree) 
        /// </summary>
        /// <returns></returns>
        public K Max()
        {
            return Max(root);   
        }

        /// <summary>
        /// Checks if the subroot node has a child that is larger than it. If it does it keeps checking 
        /// that child node until it reaches one that does not have a Right child and returns that node's key.
        /// </summary>
        private K Max(Node<K, V> subroot)
        {
            if (subroot.R != null)
            {
                return Max(subroot.R);
            }
            else
            {
                return subroot.key;
            }
        }

        public K Min()
        {
            return Min(root);
        }

        private K Min(Node<K, V> subroot)
        {
            if (subroot.L != null)
            {
                return Min(subroot.L);
            }
            else
            {
                return subroot.key;
            }
        }

        public K Predecessor(K fromKey)
        {
            Node<K, V> curr = WalkToNode(fromKey, root);
            if (curr == null)
            {
                throw new ArgumentException($"ERROR: '{fromKey}' does not exist in the tree!");
            }
            if (curr.L == null)
            {
                throw new InvalidOperationException($"ERROR: '{fromKey}' does not have a predecessor");
            }
            else
            {
                return Max(curr.L);
            }
        }

        public K Successor(K fromKey)
        {
            Node<K, V> curr = WalkToNode(fromKey, root);
            if (curr == null)
            {
                throw new ArgumentException($"ERROR: '{fromKey}' does not exist in the tree!");
            }
            if (curr.R == null)
            {
                throw new InvalidOperationException($"ERROR: '{fromKey}' does not have a successor");
            }
            else
            {
                return Min(curr.R);
            }
        }

        public void PrintKeysInOrder()
        {
            PrintKeysInOrder(root);
        }

        private void PrintKeysInOrder(Node<K, V> subroot)
        {
            if (subroot != null)
            {
                PrintKeysInOrder(subroot.L);
                Console.WriteLine(subroot.key);
                PrintKeysInOrder(subroot.R);
            }
        }



        /*
        *   ===============
        *     ENUMERATION
        *   ===============
        */

        // Need to make it so that way it doesn't just return the first thing in the tree
        private K GrabNodeData(Node<K, V> subroot)
        {
            K data = default;

            if (subroot != null)
            {
                GrabNodeData(subroot.L);
                GrabNodeData(subroot.R);
                data = subroot.key;
            }
            return data;
        }

        public IEnumerator<K> GetEnumerator()
        {
            yield return GrabNodeData(root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /*
        *   =================
        *   GETTERS & SETTERS
        *   =================
        */

        public V this[K key]
        {
            get
            {
                // Go to node w/ key of what's in brackets & return the value of it
                Node<K, V> node = WalkToNode(key, root);
                if (node == null)
                {
                    throw new ArgumentException($"ERROR: Key '{key}' does not exist in the tree!");
                }
                return node.value;
            }
            set
            {
                // Go to node w/ key of what's in brackets & change the value of it
                Node<K, V> node = WalkToNode(key, root);
                if (node == null)
                {
                    throw new ArgumentException($"ERROR: '{key}' does not exist in the tree!");
                }
                node.value = value;
            }
        }
    }
}