/*********************************************
 # Author: ITEpxlore (Tran Phuc Tho) - MMS45 #
 # Email: itexplore09@yahoo.com.vn           #
 # Copyright © 2011 - 2012                   #
 *********************************************/

using System;

namespace ConvertLetterAccent
{
    class Node
    {
        public Node Next;
        public Char Letter;
        public Char ConvertTo;

        public Node()
        {
            Next = null;
        }

        public Node(Char Letter, Char ConvertTo)
        {
            this.Letter = Letter;
            this.ConvertTo = ConvertTo;
        }

    }

    class AccentTable
    {
        Node START, LAST;

        public AccentTable()
        {
            START = null;
            LAST = null;
        }

        public void Insert(Char Letter, Char ConvertTo)
        {
            Node newnode = new Node(Letter, ConvertTo);

            if (START == null)
            {
                START = newnode;
                LAST = newnode;
                return;
            }

            LAST.Next = newnode;
            newnode.Next = null;
            LAST = newnode;
        }

        public Boolean Search(ref Char Find)
        {
            for (Node currentNode = START; currentNode != null; currentNode = currentNode.Next)
            {
                if (currentNode.Letter.Equals(Find))
                {
                    Find = currentNode.ConvertTo;
                    return true;
                }
            }
            return false;
        }
    }

    public class ConvertLetter
    {
        private AccentTable[] lstAccent;

        public ConvertLetter()
        {
            lstAccent = new AccentTable[7];
            CreateAccentTable();
        }

        private void CreateAccentTable()
        {
            int k = 0;
            do
            {
                lstAccent[k] = new AccentTable();
                k++;
            } while (k < lstAccent.Length);

            char[] CaseLetterA = { 'á', 'à', 'ả', 'ã', 'ạ', 'â', 'ấ', 'ầ', 'ẫ', 'ẩ', 'ậ', 'ă', 'ắ', 'ằ', 'ẵ', 'ẳ', 'ặ' };
            char[] CaseLetterD = { 'đ' };
            char[] CaseLetterE = { 'é', 'è', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ế', 'ề', 'ễ', 'ể', 'ệ' };
            char[] CaseLetterI = { 'í', 'ì', 'ĩ', 'ỉ', 'ị' };
            char[] CaseLetterO = { 'ô', 'ơ', 'ó', 'ò', 'ỏ', 'õ', 'ọ', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ', 'ớ', 'ờ', 'ở', 'ỡ', 'ợ' };
            char[] CaseLetterU = { 'ư', 'ú', 'ù', 'ũ', 'ủ', 'ụ', 'ứ', 'ừ', 'ữ', 'ử', 'ự' };
            char[] CaseLetterY = { 'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ' };

            foreach (char c in CaseLetterA)
                lstAccent[0].Insert(c, 'a');

            foreach (char c in CaseLetterD)
                lstAccent[1].Insert(c, 'd');

            foreach (char c in CaseLetterE)
                lstAccent[2].Insert(c, 'e');

            foreach (char c in CaseLetterI)
                lstAccent[3].Insert(c, 'i');

            foreach (char c in CaseLetterO)
                lstAccent[4].Insert(c, 'o');

            foreach (char c in CaseLetterU)
                lstAccent[5].Insert(c, 'u');

            foreach (char c in CaseLetterY)
                lstAccent[6].Insert(c, 'y');
        }

        /// <summary>
        /// Clear accent in a string
        /// </summary>
        /// <param name="ToBeCleared">String to be cleared accent</param>
        /// <returns>String</returns>
        public String ClearAccent(String ToBeCleared)
        {
            char[] tmp = new char[1];
            
            for (int i = 0; i < ToBeCleared.Length; i++)
            {
                tmp[0] = ToBeCleared[i];
                char oldValue = tmp[0];

                if (tmp[0].GetHashCode() < 8000000) //All letters which is not contain accent, has HashCode < 80000
                    continue;

                for (int j = 0; j < lstAccent.Length; j++)
                {
                    char newValue = tmp[0];
                    if (lstAccent[j].Search(ref newValue))
                        ToBeCleared = ToBeCleared.Replace(oldValue, newValue);
                }
            }
            
            return ToBeCleared;
        }
    }
}
