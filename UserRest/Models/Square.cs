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

        public Joueur Owner
        {
            get;
            set;
        }

        public Square()
        {
            
        }

        public Square(int id, Joueur j)
        {
            Id = id;
            Owner = j;
        }
    }
}
