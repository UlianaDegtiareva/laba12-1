using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLaba10;

namespace laba12_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<Plants> list = new MyList<Plants>();
            int answer;
            do
            {
                Console.WriteLine("------ РАБОТА СО СПИСКОМ ------");
                Console.WriteLine("1. Создание списка c элементами базового класса Plant");
                Console.WriteLine("2. Создание списка со всеми элементами из коллекции");
                Console.WriteLine("3. Печать списка");
                Console.WriteLine("4. Добавление элементов в начало списка ");
                Console.WriteLine("5. Удаление улементов с заданным именем");
                Console.WriteLine("6. Клонирование списка");
                Console.WriteLine("7. Полное удаление списка");
                Console.WriteLine("8. Выход");
                Console.WriteLine();
                answer = InputAnswer();
                switch (answer)
                {
                    case 1:
                        list = СreatingListPlants();
                        break;
                    case 2:
                        {
                            list = СreatingList();
                            break;
                        }
                    case 3: list.PrintList(); break;
                    case 4:
                        Add(list);
                        break;
                    case 5:
                        Remove(list);
                        break;
                    case 6:
                        Clone(list);
                        break;
                    case 7:
                        list.Clear();
                        Console.WriteLine("Лист удален из памяти");
                        break;
                    case 8: { break; }
                    default:
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer != 8);
        }
        /// <summary>
        /// Ввод данных
        /// </summary>
        /// <returns></returns>
        static int InputAnswer()
        {
            int answer;
            bool Ok;
            do
            {
                string buf = Console.ReadLine();
                Ok = int.TryParse(buf, out answer);
                if (!Ok)
                {
                    Console.WriteLine("Неправильно выбран пункт меню. Повторите ввод");
                }
            } while (!Ok);
            return answer;
        }
        
        /// <summary>
        /// Создание списка с элементами базового класса
        /// </summary>
        /// <returns></returns>
        static MyList<Plants> СreatingListPlants()
        {
            int size;
            bool oK;
            do
            {
                Console.WriteLine("Введите размер списка:");
                string buf = Console.ReadLine();
                oK = int.TryParse(buf, out size);
                if ((!oK) || (size <= 0))
                {
                    Console.WriteLine("Неверный ввод количества элементов. Повторите ввод");
                }
            } while ((!oK) || (size < 0));
            MyList<Plants> list = new MyList<Plants>(size);
            Console.WriteLine("Список создан");
            return list;
        }
        
        /// <summary>
        /// Создание списка с элементами всех классов библиотеки
        /// </summary>
        /// <returns></returns>
        static MyList<Plants> СreatingList()
        {
            Plants plants = new Plants();
            plants.RandomInit();
            Flowers flowers = new Flowers();
            flowers.RandomInit();
            Trees trees = new Trees();
            trees.RandomInit();
            Rose rose = new Rose();
            rose.RandomInit();
            MyList<Plants> list = new MyList<Plants>(plants, flowers, trees, rose);
            Console.WriteLine("Список создан");
            return list;
        }
        
        /// <summary>
        /// Добавление элементов в начало списка
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static MyList<Plants> Add(MyList<Plants> list)
        {
            int add;
            bool oK;
            do
            {
                Console.WriteLine("Ведите количество элементов, которое необходимо добавить:");
                string buf = Console.ReadLine();
                oK = int.TryParse(buf, out add);
                if ((!oK) || (add < 0))
                {
                    Console.WriteLine("Неверный ввод количества элементов. Повторите ввод");
                }
            } while ((!oK) || (add < 0));
            list.AddToBegin(add);
            Console.WriteLine("Элементы добавлены.");
            return list;
        }

        /// <summary>
        /// Удаление элементов с заданным именем
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static MyList<Plants> Remove(MyList<Plants> list)
        {
            Console.WriteLine("Введите название растения. Элементы с таким же с названием будут удалены");
            string name = Console.ReadLine();
            list.RemoveAllItemsByName(name);
            Console.WriteLine("Все элементы из списка удалены или таких элементов нет");
            return list;
        }

        /// <summary>
        /// Клоинрование элементов
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static MyList<Plants> Clone(MyList<Plants> list)
        { 
            MyList<Plants> clone = new MyList<Plants>();
            clone = list.Clone();
            Console.WriteLine("Выполнено клонирование листа: ");
            clone.PrintList();
            Console.WriteLine();
            return clone;
        }
    }
}
