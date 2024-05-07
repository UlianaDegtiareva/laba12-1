using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLaba10;

namespace laba12_1
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? beg = null;
        public Point<T>? end = null;

        int count = 0;

        public int Count => count;

        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }
        
        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        /// <summary>
        /// Метод для добавления элемента в конец списка
        /// </summary>
        /// <param name="item"></param>
        public void AddToEnd(T item)
        { 
            T newData = (T)item.Clone(); //глубокое клонирование
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Pred = end;
                end = newItem;
            }
            else
            { 
                beg = newItem;
                end = beg;
            }
        }

        public MyList() { }

        /// <summary>
        /// Конструктор для создания списка с заданным размером
        /// </summary>
        /// <param name="size"></param>
        /// <exception cref="Exception"></exception>
        public MyList(int size)
        {
            try
            {
                if (size <= 0) throw new Exception("size less zero");
                beg = null;
                end = null;
                count = 0;
                // Если size больше 0, то создаем список с элементами
                if (size > 0)
                {
                    beg = MakeRandomData();
                    end = beg;
                    for (int i = 1; i < size; i++)
                    {
                        T newItem = MakeRandomItem();
                        AddToEnd(newItem);
                    }
                    count = size;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }




        /// <summary>
        /// Конструктор для создания списка через элементы
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="Exception"></exception>
        public MyList(params T[] collection)
        {
            try
            {
                if (collection == null)
                    throw new Exception("empty collection: null");
                if (collection.Length == 0) throw new Exception("empty collection");
                T newData = (T)collection[0].Clone();
                beg = new Point<T>(newData);
                end = beg;
                for (int i = 0; i < collection.Length; i++)
                {
                    AddToEnd(collection[i]);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод для печати листа
        /// </summary>
        public void PrintList()
        { 
            if (count == 0) Console.WriteLine("the list is empty");
            Point<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }

        /// <summary>
        /// Метод для добавления элемента в начало списка
        /// </summary>
        /// <param name="K"></param> количество добавляемых элементов
        public void AddToBegin(int K)
        {
            for (int i = 0; i < K; i++)
            {
                Plants plant = new Plants();
                plant.RandomInit();
                T newData = (T)plant.Clone(); //глубокое клонирование
                Point<T> newItem = new Point<T>(newData);
                count++;
                if (beg != null)
                {
                    beg.Pred = newItem;
                    newItem.Next = beg;
                    beg = newItem;
                }
                else
                {
                    beg = newItem;
                    end = beg;
                }
            }
        }

        /// <summary>
        /// Метод для поиска элемента в списке по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Point<T>? FindItem(string name)
        {
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data == null) throw new Exception("data is null");
                if (current.Data is Plants plant && plant.Name.Equals(name))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Метод для удаления всех элементов с заданным именем
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAllItemsByName(string name)
        {
            try
            {
                if (beg == null) throw new Exception("the empty list");
                bool removedAny = true;
                while (removedAny)
                {
                    Point<T>? pos = FindItem(name);
                    if (pos == null)
                    {
                        removedAny = false;
                    }
                    else
                    {
                        count--;
                        //один элемент
                        if (beg == end)
                        {
                            beg = end = null;
                        }
                        //первый элемент
                        else if (pos.Pred == null)
                        {
                            beg = beg?.Next;
                            beg.Pred = null;
                        }
                        //последний элемент
                        else if (pos.Next == null)
                        {
                            end = end.Pred;
                            end.Next = null;
                        }
                        else
                        {
                            Point<T> next = pos.Next;
                            Point<T>? pred = pos.Pred;
                            pos.Next.Pred = pred;
                            pos.Pred.Next = next;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод для удаления листа из памяти
        /// </summary>
        public void Clear()
        {
            Point<T>? current = beg;
            while (current != null)
            {
                Point<T>? next = current.Next;  // Сохранение ссылки на следующий элемент
                current.Next = null;  // Обнуление ссылки на следующий элемент текущего элемента

                if (next != null)
                {
                    next.Pred = null; // Обнуление ссылки на предыдущий элемент следующего элемента
                }

                current = next;  // Переход к следующему элементу
            }
            beg = null;
            end = null;
            count = 0;
        }

        /// <summary>
        /// Метод для глубокого клонирования списка
        /// </summary>
        /// <returns></returns>
        public MyList<T> Clone()
        {
            MyList<T> cloneList = new MyList<T>();
            Point<T>? current = beg;
            while (current != null)
            {
                cloneList.AddToEnd(current.Data);
                current = current.Next;
            }
            cloneList.count = count;
            return cloneList;
        }
    }
}
