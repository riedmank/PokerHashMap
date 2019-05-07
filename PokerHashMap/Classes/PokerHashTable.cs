using System;
using System.Collections.Generic;
using System.Text;
using LinkedList.Classes;

namespace PokerHashMap.Classes
{
    public class PokerHashTable
    {
        //Table of ranks
        LL[] Table = new LL[1024];
        int rank = 1; //How good a poker hand is

        /// <summary>
        /// Adds a value to the hashtable
        /// </summary>
        /// <param name="key">Poker Hand</param>
        /// <param name="value">Rank</param>
        public void Add(string key)
        {
            int index = GetHash(key);
            Node node = new Node(key, rank++);
            if (Table[index] == null)
                Table[index] = new LL(node);
            else
                Table[index].Append(node);
        }

        /// <summary>
        /// Finds rank of hand
        /// </summary>
        /// <param name="key">Poker hand</param>
        /// <returns>Rank of hand</returns>
        public int Find(string key)
        {
            int index = GetHash(key);
            Table[index].Current = Table[index].Head;
            while (Table[index].Current != null)
            {
                if ((string)Table[index].Current.Key == key)
                    return (int)Table[index].Current.Value;
                else
                    Table[index].Current = Table[index].Current.Next;
            }
            return 0;
        }

        /// <summary>
        /// Determine if key exists in Hashtable
        /// </summary>
        /// <param name="key">Poker hand</param>
        /// <returns>True or false</returns>
        public bool Contains(string key)
        {
            int index = GetHash(key);
            if (Table[index] == null)
                return false;
            Table[index].Current = Table[index].Head;
            while (Table[index].Current != null)
            {
                if ((string)Table[index].Current.Key == key)
                    return true;
                Table[index].Current = Table[index].Current.Next;
            }
            return false;
        }

        /// <summary>
        /// Gets location of where to add poker hand
        /// </summary>
        /// <param name="key">Poker Hand</param>
        /// <returns>Returns index of where to store rank of poker hand</returns>
        public int GetHash(string key)
        {
            int index = 0;
            foreach (char item in key)
                index += (int)item;
            index = (index * 743) % Table.Length;
            return index;
        }
    }
}
