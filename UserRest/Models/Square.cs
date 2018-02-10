using System;
namespace UserRest.Models
{
    public class Square
    {
        public int Id
        {
            get;
            set;
        }

        public Pseudo Owner
        {
            get;
            set;
        }

        public Square()
        {
            
        }

        public Square(int id, Pseudo j)
        {
            Id = id;
            Owner = j;
        }
    }
}
