using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    public class Inventory
    {
        private Dictionary<ItemType, List<Item>> _inventory;
        private IMonster _owner;

        public Inventory(IMonster owner)
        {
            _owner = owner;
            _inventory = new Dictionary<ItemType, List<Item>>();
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                _inventory.Add(type, new List<Item>());
        }

        public void GenerateNewItem()
        {
            var item = new Item(_owner.Level);
            ConsoleExtras.ColorLine($"Recieved new item!\n {item}", ConsoleColor.Blue);
            _inventory[item.Type].Add(item);
        }

        public void DisplayAll()
        {

        }

        public void DisplayItems(ItemType type)
        {

        }

        private void SwapEquipment(ItemType type, int index)
        {
            Item temp = _owner.Equipment[type];
            _owner.Equipment[type] = _inventory[type][index];
            _inventory[type][index] = temp;
        }
    }
}
