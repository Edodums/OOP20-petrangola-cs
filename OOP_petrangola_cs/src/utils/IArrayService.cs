using System.Linq;

namespace OOP_petrangola_cs.utils
{
    /// <summary>
    /// Nice way to split an array using Linq https://stackoverflow.com/a/1841276/13455322
    /// </summary>
    public interface IArrayService
    {
        static void Split<T>(T[] array, int index, out T[] first, out T[] second)
        {
            first = array.Take(index).ToArray();
            second = array.Skip(index).ToArray();
        }

        static void SplitMidPoint<T>(T[] array, out T[] first, out T[] second)
        {
            Split(array, array.Length / 2, out first, out second);
        }
    }
}
