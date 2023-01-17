using System;
using System.Collections.Generic;
using System.Text;

namespace moorbot
{
   static class LevelManager
    {
        public static int maxLevel = 42;
       public static byte[] GetPhotoFromLevel(int lvl)
        {
            Console.WriteLine();
           var result = (byte[])moorbot.Main.Storage.Photos.ResourceManager.GetObject($"_result__{lvl-1}");
            return result;
        }
        public static int CalcLevel(int level, int happiness)
        {
            if(happiness>((level*7+level*7)+level*3))
            { return level + 1; }
            else
            {
                return level;
            }


        }

    }
}
