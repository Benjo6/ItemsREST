using System;

namespace Items
{
    public class Item
    {
        private int _nr;
        private string _desciption;
        private int _price;

        public Item()
        {

        }

        public Item(int nr, string desciption, int price)
        {

            _nr = nr;
            _desciption = desciption;
            _price = price;
        }

        public int Nr { get => _nr; set => _nr = value; }
        public string Desciption { get => _desciption; set => _desciption = value; }
        public int Price { get => _price; set => _price = value; }


        public override string ToString()
        {
            return Nr + Desciption + Price  ;
        }
    }
}
