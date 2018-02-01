using System;
namespace UserRest.Models
{
    public class Joueur
    {
        public string Nom
        {
            get;
            set;
        }

        public int NombrePartie
        {
            get;
            set;
        }

        public int NombreVictoire
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        public Joueur()
        {
            
        }

        public Joueur(string nom, int nbParties, int nbVictoires, string image)
        {
            Nom = nom;
            NombrePartie = nbParties;
            NombreVictoire = nbVictoires;
            Image = image;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Joueur input = (Joueur)obj;

                return Nom == input.Nom && NombrePartie == input.NombrePartie && NombreVictoire == input.NombreVictoire && Image == input.Image;
            }
            else
            {
                return false;
            }
        }
    }
}
