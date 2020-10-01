using System.Collections.Generic;

namespace Lektion9
{
    class Inventory {
        Stack<Item> items;

        public Inventory()
        {
            items = new Stack<Item>();
        }

        public Item Peek() {
            return items.Peek();
        }
        public Item Pop() {
            return items.Pop();
        }

        public void Push(Item item) {
            items.Push(item);
        }

        public bool IsEmpty() {
            return items.Count == 0;
        }
    }

}
