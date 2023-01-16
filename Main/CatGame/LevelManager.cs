using System;
using System.Collections.Generic;
using System.Text;

namespace moorbot
{
   static class LevelManager
    {
       public static byte[] GetPhotoFromLevel(int lvl)
        {
           var result = (byte[])moorbot.Main.Storage.Photos.ResourceManager.GetObject($"_{lvl}_result");
            return result;
        }
        public static int CalcLevel(int level, int happiness)
        {
            if(happiness>level*7+level*7)
            { return level + 1; }
            else
            {
                return level;
            }


        }

    }
}
