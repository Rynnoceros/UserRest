using System;
using System.Collections.Generic;

namespace UserRest.Models
{
    public class Pseudo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int NombrePartie { get; set; }
        public int Victoires { get; set; }

        public Pseudo()
        {
        }

        public override bool Equals(Object obj)
        {
            bool bRetour = false;
            if (obj != null && obj.GetType() == typeof(Pseudo))
            {
                Pseudo toCompare = (Pseudo)obj;

                bRetour = (this.Id == toCompare.Id) && 
                          (this.Name == toCompare.Name) && 
                          (this.Avatar == toCompare.Avatar) && 
                          (this.NombrePartie == toCompare.NombrePartie) && 
                          (this.Victoires == toCompare.Victoires);
            }

            return bRetour;
        }
    }


}
