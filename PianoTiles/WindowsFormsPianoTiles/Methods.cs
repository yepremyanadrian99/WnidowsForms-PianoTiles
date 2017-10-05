using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsPianoTiles
{
    class Methods
    {
        Form1 fr;
        internal Player p;

        public Methods(Form1 fr)
        {
            this.fr = fr;
            p = new Player();
            p.Score = 0;
            p = Read();
        }

        internal Player Read()
        {
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream("Data/Highscore.dat", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    p = (Player)bf.Deserialize(fs);
                }
            }
            catch { }
            return p;
        }

        internal void Save(Player p)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream("Data/Highscore.dat", FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fs, p);
            }
        }
    }
}