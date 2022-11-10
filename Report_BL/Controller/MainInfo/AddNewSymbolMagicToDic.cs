using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report_BL.Controller.MainInfo
{
    public static class AddNewSymbolMagicToDic
    {
        /// <summary>
        /// Добавляем если не сушествует в словаре новый символ и меджик
        /// </summary>
        /// <param name="dic">Словарь</param>
        /// <param name="newSymbol">новый символ</param>
        /// <param name="newMagic">номер меджика</param>
        /// <returns>Словарь</returns>
        public static Dictionary<string, List<int>> Add(Dictionary<string, List<int>> dic, string newSymbol, int newMagic)
        {
            // перебираем все символы в словаре
            foreach (string sym in dic.Keys)
            {
                // Если такой символ уже есть
                if (sym.Contains(newSymbol))
                {
                    // Перебираем меджики - проверяем нет ли уже такого
                    foreach (int magic in dic[sym])
                    {
                        // Если меджик и символ совпадает
                        // возвращаем исходный словарь
                        if (magic == newMagic)
                            return dic;
                    }
                    // Если такой символ есть а межика такого нет
                    // то добавляем новый меджик к этому символу
                    dic[sym].Add(newMagic);
                    return dic;
                }
                // Если такого символа нет в словаре - добавляем в словарь
            }
            dic.Add(newSymbol, new List<int> { newMagic });
            return dic;
        }
    }
}
